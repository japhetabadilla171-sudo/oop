' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: DatabaseHelper
' OOP Concept: DATA ACCESS ABSTRACTION & ENCAPSULATION
' Manages the Microsoft Access (.accdb) database connection via the ACE OLEDB
' provider, table schema creation, parameterized query execution, and automated
' seeding on first run.
'
' ACCESS INTEGRATION NOTES (why this differs from other engines):
'   * The ACE provider parameterizes by POSITION, not by name: every "?" in the
'     SQL marks one parameter, and OleDbParameters MUST be added in the exact
'     order the "?" appear in the SQL text.
'   * Parameter types are declared explicitly (OleDbType) because type
'     inference mislabels Currency/Memo columns and causes "Data type
'     mismatch" errors.
'   * Access cannot create empty .accdb files from SQL, so a missing file is
'     generated through the ADOX Catalog COM object (late-bound, no extra
'     project reference required).
'   * Jet SQL supports one statement per command: INSERT and "SELECT
'     @@IDENTITY" are therefore two separate commands on the same open
'     connection (see InsertAndGetIdentity).
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms

Namespace Data
    Public Class DatabaseHelper
        Public Shared ReadOnly DbFileName As String = "grocery.accdb"
        Public Shared Property DbFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DbFileName)

        ' The ACE OLEDB provider version that successfully opens this database.
        ' Resolved lazily on first connection (12.0 is tried first, then 16.0).
        Private Shared _resolvedProvider As String = Nothing

        ''' <summary>
        ''' Picks the ACE provider installed on this machine.
        ''' Checked against the provider enumeration (no database file required).
        ''' </summary>
        Public Shared ReadOnly Property ResolvedProvider As String
            Get
                If String.IsNullOrEmpty(_resolvedProvider) Then
                    Dim installed As New List(Of String)()
                    Try
                        Using reader As OleDbDataReader = OleDbEnumerator.GetRootEnumerator()
                            While reader.Read()
                                Dim name As String = Convert.ToString(reader("SOURCES_NAME"))
                                If Not String.IsNullOrEmpty(name) Then
                                    installed.Add(name.Trim())
                                End If
                            End While
                        End Using
                    Catch ex As Exception
                        ' Enumeration unavailable -> fall back to the default provider
                    End Try

                    Dim candidates() As String = {
                        "Microsoft.ACE.OLEDB.12.0",
                        "Microsoft.ACE.OLEDB.16.0",
                        "Microsoft.ACE.OLEDB.16.0.7",
                        "Microsoft.ACE.OLEDB.16.0.14"
                    }

                    For Each candidate In candidates
                        If installed.Contains(candidate) Then
                            _resolvedProvider = candidate
                            Return _resolvedProvider
                        End If
                    Next

                    _resolvedProvider = candidates(0)
                End If
                Return _resolvedProvider
            End Get
        End Property

        Public Shared ReadOnly Property ConnectionString As String
            Get
                Return $"Provider={ResolvedProvider};Data Source={DbFilePath};Persist Security Info=False;"
            End Get
        End Property

        ''' <summary>
        ''' Gets an active, open Microsoft Access database connection.
        ''' </summary>
        Public Shared Function GetConnection() As OleDbConnection
            Dim conn As New OleDbConnection(ConnectionString)
            conn.Open()
            Return conn
        End Function

        ' ==========================================================================
        ' Typed, positional OleDb parameter helpers.
        '
        ' THE GOLDEN RULE: call these in the exact order the "?" placeholders
        ' appear in the SQL text (OleDb ignores parameter names and binds by
        ' position). Not doing so causes silent column swaps.
        ' ==========================================================================

        ''' <summary>Adds a variable text parameter (? = VarWChar).</summary>
        Public Shared Function AddText(cmd As OleDbCommand, value As String, size As Integer) As OleDbParameter
            Return AddNullableText(cmd, If(String.IsNullOrWhiteSpace(value), Nothing, value), size)
        End Function

        ''' <summary>Adds a text parameter that may be DBNull/Nothing.</summary>
        Public Shared Function AddNullableText(cmd As OleDbCommand, value As String, size As Integer) As OleDbParameter
            Dim p As OleDbParameter = cmd.Parameters.Add("?", OleDbType.VarWChar, size)
            p.Value = If(value Is Nothing, CObj(DBNull.Value), CObj(value))
            Return p
        End Function

        ''' <summary>Adds a 4-byte integer parameter (? = Long).</summary>
        Public Shared Function AddLong(cmd As OleDbCommand, value As Long) As OleDbParameter
            Dim p As OleDbParameter = cmd.Parameters.Add("?", OleDbType.Integer)
            p.Value = value
            Return p
        End Function

        ''' <summary>Adds an integer parameter that may be DBNull.</summary>
        Public Shared Function AddNullableLong(cmd As OleDbCommand, value As Nullable(Of Integer)) As OleDbParameter
            Dim p As OleDbParameter = cmd.Parameters.Add("?", OleDbType.Integer)
            p.Value = If(value.HasValue, CObj(value.Value), CObj(DBNull.Value))
            Return p
        End Function

        ''' <summary>Adds a money parameter (? = Currency).</summary>
        Public Shared Function AddCurrency(cmd As OleDbCommand, value As Decimal) As OleDbParameter
            Dim p As OleDbParameter = cmd.Parameters.Add("?", OleDbType.Currency)
            p.Value = value
            Return p
        End Function

        ''' <summary>Adds a date/time parameter (? = DATETIME).</summary>
        Public Shared Function AddDate(cmd As OleDbCommand, value As DateTime) As OleDbParameter
            Dim p As OleDbParameter = cmd.Parameters.Add("?", OleDbType.Date)
            p.Value = value
            Return p
        End Function

        ''' <summary>Adds a date parameter that may be DBNull.</summary>
        Public Shared Function AddNullableDate(cmd As OleDbCommand, value As Nullable(Of DateTime)) As OleDbParameter
            Dim p As OleDbParameter = cmd.Parameters.Add("?", OleDbType.Date)
            p.Value = If(value.HasValue, CObj(value.Value), CObj(DBNull.Value))
            Return p
        End Function

        ''' <summary>
        ''' Executes a single-statement INSERT and returns the new Autonumber
        ''' identity via SELECT @@IDENTITY on the same connection.
        ''' </summary>
        Public Shared Function InsertAndGetIdentity(cmd As OleDbCommand) As Integer
            cmd.ExecuteNonQuery()
            Using idCmd As OleDbCommand = cmd.Connection.CreateCommand()
                ' OleDb requires every command on a connection that has a pending
                ' local transaction to be assigned to that transaction too.
                If cmd.Transaction IsNot Nothing Then
                    idCmd.Transaction = cmd.Transaction
                End If
                idCmd.CommandText = "SELECT @@IDENTITY"
                Dim identity As Object = idCmd.ExecuteScalar()
                If identity Is Nothing OrElse identity Is DBNull.Value Then
                    Throw New InvalidOperationException("Access did not return a new record identity.")
                End If
                Return Convert.ToInt32(identity)
            End Using
        End Function

        ' ==========================================================================
        ' Safe value readers (Access returns .NET-native types; these helpers keep
        ' DBNULL and legacy string dates under control).
        ' ==========================================================================

        ''' <summary>Reads a DATETIME column directly (or parses legacy text).</summary>
        Public Shared Function ReadDateTime(reader As OleDbDataReader, column As String) As DateTime
            Dim raw As Object = reader(column)
            If raw Is Nothing OrElse raw Is DBNull.Value Then
                Return DateTime.MinValue
            End If
            If TypeOf raw Is DateTime Then
                Return CType(raw, DateTime)
            End If
            Dim parsed As DateTime
            If DateTime.TryParse(Convert.ToString(raw), parsed) Then
                Return parsed
            End If
            Return DateTime.MinValue
        End Function

        ''' <summary>Reads a nullable DATETIME column.</summary>
        Public Shared Function ReadNullableDateTime(reader As OleDbDataReader, column As String) As Nullable(Of DateTime)
            Dim raw As Object = reader(column)
            If raw Is Nothing OrElse raw Is DBNull.Value Then
                Return Nothing
            End If
            If TypeOf raw Is DateTime Then
                Return CType(raw, DateTime)
            End If
            Dim parsed As DateTime
            If DateTime.TryParse(Convert.ToString(raw), parsed) Then
                Return parsed
            End If
            Return Nothing
        End Function

        ''' <summary>Initializes database tables and seed data if not yet created.</summary>
        Public Shared Sub InitializeDatabase()
            Try
                ' Access cannot create an empty database via SQL — use ADOX first.
                If Not File.Exists(DbFilePath) Then
                    CreateEmptyAccessDatabase(DbFilePath)
                End If

                Using conn As OleDbConnection = GetConnection()
                    EnsureTables(conn)
                    SeedInitialDataIfEmpty(conn)
                End Using
            Catch ex As Exception
                Dim message As String = $"Failed to initialize Microsoft Access database.{vbCrLf}{ex.Message}"

                If TypeOf ex Is InvalidOperationException AndAlso
                   ex.Message IsNot Nothing AndAlso
                   ex.Message.StartsWith("Microsoft Access") Then
                    ' Keep the actionable message as-is
                    message = ex.Message
                End If

                If Application.MessageLoop Then
                    MessageBox.Show(message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Creates an empty .accdb file through the ADOX Catalog COM object.
        ''' Late-bound via reflection so no COM project reference is needed.
        ''' </summary>
        Private Shared Sub CreateEmptyAccessDatabase(filePath As String)
            Dim catalogType As Type = Type.GetTypeFromProgID("ADOX.Catalog")
            If catalogType Is Nothing Then
                Throw New InvalidOperationException(
                    "Microsoft Access is required on this computer. " &
                    "Install Microsoft Access (the Access Database Engine/ADOX components) and run the program again.")
            End If

            Dim lastError As Exception = Nothing
            For Each provider In {"Microsoft.ACE.OLEDB.12.0", "Microsoft.ACE.OLEDB.16.0"}
                Dim catalog As Object = Nothing
                Try
                    catalog = Activator.CreateInstance(catalogType)
                    Dim connectArgs() As Object = { $"Provider={provider};Data Source={filePath};" }
                    catalogType.InvokeMember("Create", System.Reflection.BindingFlags.InvokeMethod, Nothing, catalog, connectArgs)
                    Return ' success
                Catch ex As Exception
                    lastError = If(ex.InnerException, ex)
                Finally
                    If catalog IsNot Nothing Then
                        Try
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(catalog)
                        Catch
                            ' release failure is not fatal
                        End Try
                    End If
                End Try
            Next

            Throw New InvalidOperationException(
                $"Microsoft Access Database Engine (ACE OLEDB) provider is not usable on this computer. " &
                $"Install 32-bit Microsoft Access or the 32-bit Access Database Engine, then run again. ({lastError?.Message})")
        End Sub

        ''' <summary>
        ''' Creates the missing tables (Access DDL has no IF NOT EXISTS).
        ''' Any table that already exists is left untouched.
        ''' </summary>
        Private Shared Sub EnsureTables(conn As OleDbConnection)
            Dim existing As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Using schema As DataTable = conn.GetSchema("Tables")
                For Each row As DataRow In schema.Rows
                    If Convert.ToString(row("TABLE_TYPE")) = "TABLE" Then
                        existing.Add(Convert.ToString(row("TABLE_NAME")).Trim())
                    End If
                Next
            End Using

            ' NOTE: each block runs only when that table is missing. FK constraints
            ' reference earlier tables, so the order below must never change.
            If Not existing.Contains("Users") Then
                ExecuteDdl(conn, "
                    CREATE TABLE Users (
                        UserId COUNTER(1,1),
                        Username VARCHAR(60) NOT NULL,
                        PasswordHash VARCHAR(255) NOT NULL,
                        Salt VARCHAR(64) NOT NULL,
                        FullName VARCHAR(120) NOT NULL,
                        Role VARCHAR(30) NOT NULL,
                        IsActive LONG NOT NULL,
                        CreatedAt DATETIME NOT NULL,
                        CONSTRAINT PK_Users PRIMARY KEY (UserId),
                        CONSTRAINT UQ_Users_Username UNIQUE (Username)
                    );")
            End If

            If Not existing.Contains("Categories") Then
                ExecuteDdl(conn, "
                    CREATE TABLE Categories (
                        CategoryId COUNTER(1,1),
                        Name VARCHAR(90) NOT NULL,
                        CONSTRAINT PK_Categories PRIMARY KEY (CategoryId),
                        CONSTRAINT UQ_Categories_Name UNIQUE (Name)
                    );")
            End If

            If Not existing.Contains("Suppliers") Then
                ExecuteDdl(conn, "
                    CREATE TABLE Suppliers (
                        SupplierId COUNTER(1,1),
                        Name VARCHAR(150) NOT NULL,
                        ContactNumber VARCHAR(30),
                        Address VARCHAR(255),
                        CONSTRAINT PK_Suppliers PRIMARY KEY (SupplierId)
                    );")
            End If

            If Not existing.Contains("Products") Then
                ExecuteDdl(conn, "
                    CREATE TABLE Products (
                        ProductId COUNTER(1,1),
                        ProductCode VARCHAR(30) NOT NULL,
                        Name VARCHAR(150) NOT NULL,
                        CategoryId LONG NOT NULL,
                        SupplierId LONG NOT NULL,
                        Price CURRENCY NOT NULL,
                        CostPrice CURRENCY NOT NULL,
                        StockQty LONG NOT NULL,
                        ReorderLevel LONG NOT NULL,
                        ExpiryDate DATETIME,
                        IsActive LONG NOT NULL,
                        CONSTRAINT PK_Products PRIMARY KEY (ProductId),
                        CONSTRAINT UQ_Products_Code UNIQUE (ProductCode),
                        CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories (CategoryId),
                        CONSTRAINT FK_Products_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers (SupplierId)
                    );")
            End If

            If Not existing.Contains("Sales") Then
                ExecuteDdl(conn, "
                    CREATE TABLE Sales (
                        SaleId COUNTER(1,1),
                        ReceiptNo VARCHAR(40) NOT NULL,
                        SaleDate DATETIME NOT NULL,
                        CashierId LONG NOT NULL,
                        CustomerId LONG,
                        Subtotal CURRENCY NOT NULL,
                        DiscountType VARCHAR(40) NOT NULL,
                        DiscountAmount CURRENCY NOT NULL,
                        Total CURRENCY NOT NULL,
                        CashReceived CURRENCY NOT NULL,
                        ChangeAmount CURRENCY NOT NULL,
                        CONSTRAINT PK_Sales PRIMARY KEY (SaleId),
                        CONSTRAINT UQ_Sales_ReceiptNo UNIQUE (ReceiptNo),
                        CONSTRAINT FK_Sales_Cashiers FOREIGN KEY (CashierId) REFERENCES Users (UserId),
                        CONSTRAINT FK_Sales_Customers FOREIGN KEY (CustomerId) REFERENCES Users (UserId)
                    );")
            End If

            If Not existing.Contains("SaleItems") Then
                ExecuteDdl(conn, "
                    CREATE TABLE SaleItems (
                        SaleItemId COUNTER(1,1),
                        SaleId LONG NOT NULL,
                        ProductId LONG NOT NULL,
                        Quantity LONG NOT NULL,
                        UnitPrice CURRENCY NOT NULL,
                        LineTotal CURRENCY NOT NULL,
                        CONSTRAINT PK_SaleItems PRIMARY KEY (SaleItemId),
                        CONSTRAINT FK_SaleItems_Sales FOREIGN KEY (SaleId) REFERENCES Sales (SaleId),
                        CONSTRAINT FK_SaleItems_Products FOREIGN KEY (ProductId) REFERENCES Products (ProductId)
                    );")
            End If

            If Not existing.Contains("StockMovements") Then
                ExecuteDdl(conn, "
                    CREATE TABLE StockMovements (
                        MovementId COUNTER(1,1),
                        ProductId LONG NOT NULL,
                        MovementType VARCHAR(20) NOT NULL,
                        QuantityChange LONG NOT NULL,
                        Reason VARCHAR(255),
                        SupplierId LONG,
                        UserId LONG NOT NULL,
                        MovementDate DATETIME NOT NULL,
                        CONSTRAINT PK_StockMovements PRIMARY KEY (MovementId),
                        CONSTRAINT FK_StockMovements_Products FOREIGN KEY (ProductId) REFERENCES Products (ProductId),
                        CONSTRAINT FK_StockMovements_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers (SupplierId),
                        CONSTRAINT FK_StockMovements_Users FOREIGN KEY (UserId) REFERENCES Users (UserId)
                    );")
            End If
        End Sub

        Private Shared Sub ExecuteDdl(conn As OleDbConnection, sql As String)
            Using cmd As New OleDbCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>
        ''' Populates initial seed data (Users, Categories, Suppliers, 30 Products) inside a transaction.
        ''' </summary>
        Private Shared Sub SeedInitialDataIfEmpty(conn As OleDbConnection)
            Using checkCmd As New OleDbCommand("SELECT COUNT(*) FROM Users;", conn)
                Dim userCount As Long = Convert.ToInt64(checkCmd.ExecuteScalar())
                If userCount > 0 Then
                    Return ' Database already populated
                End If
            End Using

            Using trans As OleDbTransaction = conn.BeginTransaction()
                Try
                    ' 1. Seed Users (1 Owner, 1 Cashier, 1 Clerk, 1 Customer)
                    ' QA A5/B7/C6: the role suffix used to be baked into FullName
                    ' ("Maria Santos (Owner)"), so headers/combos showed the role
                    ' twice. Seeds now store the plain name; User.GetDisplayName()
                    ' still strips any legacy suffix found in existing databases.
                    Dim usersToSeed As New List(Of SeedUser) From {
                        New SeedUser("owner", "owner123", "Maria Santos", "StoreOwner"),
                        New SeedUser("cashier", "cashier123", "Juan Dela Cruz", "Cashier"),
                        New SeedUser("clerk", "clerk123", "Pedro Penduko", "InventoryClerk"),
                        New SeedUser("customer", "customer123", "Ana Batumbakal", "Customer")
                    }

                    For Each u In usersToSeed
                        Dim salt As String = Services.AuthService.GenerateSalt()
                        Dim hash As String = Services.AuthService.HashPassword(u.P, salt)

                        Using cmd As New OleDbCommand("
                            INSERT INTO Users (Username, PasswordHash, Salt, FullName, Role, IsActive, CreatedAt)
                            VALUES (?, ?, ?, ?, ?, ?, ?);", conn, trans)
                            AddText(cmd, u.U, 60)
                            AddText(cmd, hash, 255)
                            AddText(cmd, salt, 64)
                            AddText(cmd, u.N, 120)
                            AddText(cmd, u.R, 30)
                            AddLong(cmd, 1)
                            AddDate(cmd, DateTime.Now)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    ' 2. Seed 6 Categories
                    Dim categories As String() = {"Rice & Grains", "Canned Goods", "Noodles & Pasta", "Beverages & Drinks", "Snacks & Biscuits", "Personal Care & Toiletries"}
                    For Each cat In categories
                        Using cmd As New OleDbCommand("INSERT INTO Categories (Name) VALUES (?);", conn, trans)
                            AddText(cmd, cat, 90)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    ' 3. Seed 3 Suppliers
                    Dim suppliers As New List(Of SeedSupplier) From {
                        New SeedSupplier("San Miguel Distribution", "0917-123-4567", "EDSA, Mandaluyong City"),
                        New SeedSupplier("Universal Robina Logistics", "0918-234-5678", "Bagong Ilog, Pasig City"),
                        New SeedSupplier("Mega Global Supply Co.", "0922-345-6789", "Valenzuela City, Metro Manila")
                    }
                    For Each sup In suppliers
                        Using cmd As New OleDbCommand("
                            INSERT INTO Suppliers (Name, ContactNumber, Address)
                            VALUES (?, ?, ?);", conn, trans)
                            AddText(cmd, sup.Name, 150)
                            AddText(cmd, sup.Contact, 30)
                            AddText(cmd, sup.Address, 255)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    ' 4. Seed 30 Realistic Grocery Products with Philippine Peso prices
                    ' Category IDs: 1=Rice, 2=Canned, 3=Noodles, 4=Beverages, 5=Snacks, 6=Personal Care
                    ' Supplier IDs: 1=San Miguel, 2=Universal Robina, 3=Mega Global
                    Dim products As List(Of SeedProduct) = GetSeedProducts()

                    For Each p In products
                        Dim productId As Integer
                        Using cmd As New OleDbCommand("
                            INSERT INTO Products (ProductCode, Name, CategoryId, SupplierId, Price, CostPrice, StockQty, ReorderLevel, ExpiryDate, IsActive)
                            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?);", conn, trans)
                            AddText(cmd, p.Code, 30)
                            AddText(cmd, p.Name, 150)
                            AddLong(cmd, p.Cat)
                            AddLong(cmd, p.Sup)
                            AddCurrency(cmd, p.Price)
                            AddCurrency(cmd, p.Cost)
                            AddLong(cmd, p.Qty)
                            AddLong(cmd, p.Reorder)
                            AddDate(cmd, p.Exp)
                            AddLong(cmd, 1)
                            productId = InsertAndGetIdentity(cmd)
                        End Using

                        ' Record initial stock movement audit entry
                        Using moveCmd As New OleDbCommand("
                            INSERT INTO StockMovements (ProductId, MovementType, QuantityChange, Reason, SupplierId, UserId, MovementDate)
                            VALUES (?, ?, ?, ?, ?, ?, ?);", conn, trans)
                            AddLong(moveCmd, productId)
                            AddText(moveCmd, "StockIn", 20)
                            AddLong(moveCmd, p.Qty)
                            AddText(moveCmd, "Initial Store Inventory Stock-In", 255)
                            AddLong(moveCmd, p.Sup)
                            AddLong(moveCmd, 1)
                            AddDate(moveCmd, DateTime.Now)
                            moveCmd.ExecuteNonQuery()
                        End Using
                    Next

                    trans.Commit()
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Private Shared Function GetSeedProducts() As List(Of SeedProduct)
            Dim list As New List(Of SeedProduct)()
            list.Add(New SeedProduct("PRD001", "Dinorado Special Rice 5kg", 1, 1, 280.0D, 240.0D, 45, 10, DateTime.Today.AddMonths(12)))
            list.Add(New SeedProduct("PRD002", "Sinandomeng Rice 5kg", 1, 1, 250.0D, 215.0D, 8, 10, DateTime.Today.AddMonths(10)))
            list.Add(New SeedProduct("PRD003", "Jasmine Fragrant Rice 1kg", 1, 1, 62.0D, 52.0D, 30, 15, DateTime.Today.AddMonths(8)))
            list.Add(New SeedProduct("PRD004", "Brown Rice Healthy 1kg", 1, 1, 75.0D, 60.0D, 5, 10, DateTime.Today.AddDays(15)))
            list.Add(New SeedProduct("PRD005", "Mega Sardines in Tomato Sauce 155g", 2, 3, 24.5D, 20.0D, 120, 25, DateTime.Today.AddYears(2)))
            list.Add(New SeedProduct("PRD006", "Mega Sardines with Chili 155g", 2, 3, 25.0D, 20.5D, 90, 20, DateTime.Today.AddYears(2)))
            list.Add(New SeedProduct("PRD007", "Purefoods Corned Beef 150g", 2, 1, 68.0D, 58.0D, 50, 15, DateTime.Today.AddMonths(18)))
            list.Add(New SeedProduct("PRD008", "Argentina Meat Loaf 150g", 2, 1, 28.0D, 23.0D, 4, 15, DateTime.Today.AddMonths(6)))
            list.Add(New SeedProduct("PRD009", "San Marino Corned Tuna 180g", 2, 3, 42.0D, 35.0D, 65, 20, DateTime.Today.AddMonths(14)))
            list.Add(New SeedProduct("PRD010", "Century Tuna Flakes in Oil 155g", 2, 3, 38.5D, 32.0D, 12, 20, DateTime.Today.AddDays(20)))
            list.Add(New SeedProduct("PRD011", "Lucky Me! Instant Pancit Canton Kalamansi 80g", 3, 2, 16.0D, 13.0D, 150, 30, DateTime.Today.AddMonths(8)))
            list.Add(New SeedProduct("PRD012", "Lucky Me! Pancit Canton Chilimansi 80g", 3, 2, 16.0D, 13.0D, 140, 30, DateTime.Today.AddMonths(8)))
            list.Add(New SeedProduct("PRD013", "Lucky Me! Beef Mami Noodle Soup 55g", 3, 2, 14.5D, 11.5D, 80, 25, DateTime.Today.AddMonths(7)))
            list.Add(New SeedProduct("PRD014", "Payless Extra Big Pancit Canton 130g", 3, 2, 21.0D, 17.0D, 6, 15, DateTime.Today.AddMonths(5)))
            list.Add(New SeedProduct("PRD015", "Royal Spaghetti Pasta 900g", 3, 2, 95.0D, 78.0D, 25, 10, DateTime.Today.AddMonths(12)))
            list.Add(New SeedProduct("PRD016", "Coca-Cola Original Taste 1.5L", 4, 1, 75.0D, 62.0D, 40, 15, DateTime.Today.AddMonths(6)))
            list.Add(New SeedProduct("PRD017", "Royal Tru-Orange 1.5L", 4, 1, 72.0D, 60.0D, 35, 15, DateTime.Today.AddMonths(6)))
            list.Add(New SeedProduct("PRD018", "Sprite Lemon-Lime 1.5L", 4, 1, 72.0D, 60.0D, 3, 10, DateTime.Today.AddMonths(5)))
            list.Add(New SeedProduct("PRD019", "C2 Green Tea Apple 500ml", 4, 2, 26.0D, 20.0D, 55, 20, DateTime.Today.AddDays(10)))
            list.Add(New SeedProduct("PRD020", "San Miguel Pale Pilsen Can 330ml", 4, 1, 55.0D, 45.0D, 48, 12, DateTime.Today.AddMonths(9)))
            list.Add(New SeedProduct("PRD021", "Piattos Cheese Potato Crisps 85g", 5, 2, 38.0D, 31.0D, 60, 20, DateTime.Today.AddMonths(6)))
            list.Add(New SeedProduct("PRD022", "Nova Country Cheddar Multigrain 78g", 5, 2, 37.0D, 30.0D, 50, 15, DateTime.Today.AddMonths(6)))
            list.Add(New SeedProduct("PRD023", "Chippy Barbecue Corn Chips 110g", 5, 2, 27.0D, 22.0D, 7, 20, DateTime.Today.AddMonths(4)))
            list.Add(New SeedProduct("PRD024", "SkyFlakes Crackers Box 24 Packs", 5, 2, 72.0D, 58.0D, 30, 10, DateTime.Today.AddMonths(10)))
            list.Add(New SeedProduct("PRD025", "Rebisco Sandwich Choco Crackers 10 Packs", 5, 2, 65.0D, 52.0D, 28, 10, DateTime.Today.AddDays(-5)))
            list.Add(New SeedProduct("PRD026", "Safeguard Pure White Bar Soap 130g", 6, 1, 48.0D, 38.0D, 70, 20, DateTime.Today.AddYears(3)))
            list.Add(New SeedProduct("PRD027", "Palmolive Naturals Shampoo 180ml", 6, 1, 115.0D, 95.0D, 25, 10, DateTime.Today.AddYears(2)))
            list.Add(New SeedProduct("PRD028", "Colgate Maximum Cavity Toothpaste 145g", 6, 1, 98.0D, 80.0D, 35, 15, DateTime.Today.AddYears(2)))
            list.Add(New SeedProduct("PRD029", "Sunsilk Smooth & Manageable Sachet 12s", 6, 1, 72.0D, 60.0D, 5, 15, DateTime.Today.AddMonths(12)))
            list.Add(New SeedProduct("PRD030", "Head & Shoulders Cool Menthol Sachet 12s", 6, 1, 84.0D, 70.0D, 2, 15, DateTime.Today.AddDays(25)))
            Return list
        End Function

        Private Structure SeedUser
            Public U As String
            Public P As String
            Public N As String
            Public R As String
            Public Sub New(username As String, password As String, fullName As String, role As String)
                U = username : P = password : N = fullName : R = role
            End Sub
        End Structure

        Private Structure SeedSupplier
            Public Name As String
            Public Contact As String
            Public Address As String
            Public Sub New(n As String, c As String, a As String)
                Name = n : Contact = c : Address = a
            End Sub
        End Structure

        Private Structure SeedProduct
            Public Code As String
            Public Name As String
            Public Cat As Integer
            Public Sup As Integer
            Public Price As Decimal
            Public Cost As Decimal
            Public Qty As Integer
            Public Reorder As Integer
            Public Exp As DateTime
            Public Sub New(c As String, n As String, catId As Integer, supId As Integer, pr As Decimal, co As Decimal, q As Integer, re As Integer, ex As DateTime)
                Code = c : Name = n : Cat = catId : Sup = supId : Price = pr : Cost = co : Qty = q : Reorder = re : Exp = ex
            End Sub
        End Structure
    End Class
End Namespace
