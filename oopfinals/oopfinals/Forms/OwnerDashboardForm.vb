' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: OwnerDashboardForm (Code-Behind)
' OOP Concept: FULL SYSTEM CONTROL, ABSTRACTION, REPORTING & SECURITY
' Provides total administrative access: KPI dashboard cards, staff management,
' complete product/pricing/supplier CRUD, historical sales auditing, and
' business reporting with CSV export and printing.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports oopfinals.Data
Imports oopfinals.Models
Imports oopfinals.Services
Imports oopfinals.Controls

Namespace Forms
    Public Class OwnerDashboardForm
        Private ReadOnly _owner As StoreOwner
        Private ReadOnly _userRepo As New UserRepository()
        Private ReadOnly _productRepo As New ProductRepository()
        Private ReadOnly _categoryRepo As New CategoryRepository()
        Private ReadOnly _supplierRepo As New SupplierRepository()
        Private ReadOnly _saleRepo As New SaleRepository()

        Private _selectedStaffId As Integer = 0
        Private _selectedProductId As Integer = 0
        Private _selectedCategoryId As Integer = 0
        Private _btnCatNew As Button ' runtime-added "New" for the category editor (designer has none)
        Private _selectedSupplierId As Integer = 0
        Private _currentReportData As DataTable = Nothing

        Public Sub New(owner As StoreOwner)
            InitializeComponent()
            _owner = owner
            Me.DoubleBuffered = True
            LayoutKpiCards()
            InitializeUiRestyle()
        End Sub

        Private Sub OwnerDashboardForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            Me.Refresh()
        End Sub

        Private Sub pnlCards_Resize(sender As Object, e As EventArgs) Handles pnlCards.Resize
            LayoutKpiCards()
        End Sub

        Private Sub LayoutKpiCards()
            Dim cards() As Panel = {pnlCard1, pnlCard2, pnlCard3, pnlCard4}
            Dim lblTitles() As Label = {lblCard1Title, lblCard2Title, lblCard3Title, lblCard4Title}
            Dim lblVals() As Label = {lblCard1Val, lblCard2Val, lblCard3Val, lblCard4Val}

            Dim panelWidth As Integer = pnlCards.ClientSize.Width
            Const margin As Integer = 15
            Const cardGap As Integer = 15
            Dim n As Integer = cards.Length
            Dim avail As Integer = panelWidth - (margin * 2)

            Dim cardW As Integer = (avail - (cardGap * (n - 1))) \ n
            If cardW < 200 Then
                cardW = 200
            End If

            Dim gap As Integer = (avail - (cardW * n)) \ (n - 1)
            If gap < 6 Then gap = 6
            Dim spacing As Integer = cardW + gap

            For i As Integer = 0 To n - 1
                cards(i).Size = New Size(cardW, cards(i).Height)
                cards(i).Left = margin + (spacing * i)
                lblTitles(i).Size = New Size(cardW - 20, lblTitles(i).Height)
                lblVals(i).Size = New Size(cardW - 20, lblVals(i).Height)
            Next
        End Sub

        Private Sub OwnerDashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' SECURITY: verify role on form load
            If _owner.Role <> "StoreOwner" Then
                MessageBox.Show("Access denied: this dashboard is restricted to Store Owner accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.BeginInvoke(New MethodInvoker(Sub() Me.Close()))
                Return
            End If

            ' Step 7 (Figma): "Role: Store Owner · <Full name> (@username)" — role
            ' rendered exactly once (GetDisplayName strips legacy suffixes).
            lblOwnerInfo.Text = $"Role: {_owner.GetRoleName()} · {_owner.GetDisplayName()} (@{_owner.Username})"
            AppHeader.Apply(pnlHeader, lblAppTitle, lblOwnerInfo, btnLogout) ' RESTYLE: reposition after label text set here

            ' Set initial date ranges
            dtpTxStart.Value = DateTime.Today.AddDays(-7)
            dtpTxEnd.Value = DateTime.Today
            dtpReportDate.Value = DateTime.Today

            ' Populate drop-down lists
            PopulateStaffRoles()
            PopulateCategoriesAndSuppliers()
            PopulateReportTypes()

            ' Load initial data views
            LoadOverview()
            LoadStaffList()
            LoadProductsList()
            LoadCategoriesList()
            LoadSuppliersList()
            LoadTransactions()
        End Sub

        ' =====================================================================
        ' TAB 1: OVERVIEW & KPI CARDS
        ' =====================================================================
        Private Sub LoadOverview()
            Dim todaySales As List(Of Sale) = _saleRepo.GetSalesByDate(DateTime.Today)
            Dim totalRev As Decimal = 0D
            For Each s In todaySales
                totalRev += s.Total
            Next

            Dim lowStockCount As Integer = _productRepo.GetLowStockProducts().Count
            Dim expSoonCount As Integer = _productRepo.GetExpiringProducts(30).Count

            lblCard1Val.Text = $"PHP {totalRev:N2}"
            lblCard2Val.Text = todaySales.Count.ToString()
            lblCard3Val.Text = $"{lowStockCount} items"
            lblCard4Val.Text = $"{expSoonCount} items"

            dgvOverviewRecent.Rows.Clear()
            For Each s In todaySales
                dgvOverviewRecent.Rows.Add(s.ReceiptNo, s.SaleDate.ToString("hh:mm tt"), s.CashierName, s.Total.ToString("N2"))
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateOverviewFooter(todaySales.Count)
            ApplyKpiCardLayout() ' re-fit amounts after the text changed
        End Sub

        ' =====================================================================
        ' TAB 2: STAFF MANAGEMENT
        ' =====================================================================
        Private Sub PopulateStaffRoles()
            cboStaffRole.Items.Clear()
            cboStaffRole.Items.Add("Cashier")
            cboStaffRole.Items.Add("InventoryClerk")
            cboStaffRole.SelectedIndex = 0
        End Sub

        Private Sub LoadStaffList()
            dgvStaff.Rows.Clear()
            Dim staffList As List(Of User) = _userRepo.GetAllStaff()
            For Each u In staffList
                Dim statusStr As String = If(u.IsActive, "Active", "Inactive")
                Dim rowIdx As Integer = dgvStaff.Rows.Add(u.UserId, u.Username, u.FullName, u.Role, statusStr, u.CreatedAt.ToString("yyyy-MM-dd"))
                If Not u.IsActive Then
                    dgvStaff.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.Red
                End If
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateStaffFooter(staffList.Count)
        End Sub

        Private Sub dgvStaff_SelectionChanged(sender As Object, e As EventArgs) Handles dgvStaff.SelectionChanged
            If dgvStaff.SelectedRows.Count = 0 Then Return
            Dim row As DataGridViewRow = dgvStaff.SelectedRows(0)
            _selectedStaffId = Convert.ToInt32(row.Cells(0).Value)
            txtStaffUsername.Text = Convert.ToString(row.Cells(1).Value)
            txtStaffName.Text = Convert.ToString(row.Cells(2).Value)

            Dim role As String = Convert.ToString(row.Cells(3).Value)
            If cboStaffRole.Items.Contains(role) Then
                cboStaffRole.SelectedItem = role
            End If

            txtStaffUsername.ReadOnly = True ' Do not change existing username
            txtStaffPassword.Clear()
            lblStaffMsg.Text = $"Editing staff ID #{_selectedStaffId}"
            lblStaffMsg.ForeColor = Color.Black
        End Sub

        Private Sub btnStaffNew_Click(sender As Object, e As EventArgs) Handles btnStaffNew.Click
            _selectedStaffId = 0
            txtStaffUsername.Clear()
            txtStaffUsername.ReadOnly = False
            txtStaffName.Clear()
            txtStaffPassword.Clear()
            cboStaffRole.SelectedIndex = 0
            lblStaffMsg.Text = "Creating new staff account."
            lblStaffMsg.ForeColor = Color.Black
            txtStaffUsername.Focus()
        End Sub

        Private Sub btnStaffSave_Click(sender As Object, e As EventArgs) Handles btnStaffSave.Click
            lblStaffMsg.Text = String.Empty
            Dim username As String = txtStaffUsername.Text.Trim().ToLowerInvariant()
            Dim fullName As String = txtStaffName.Text.Trim()
            Dim role As String = Convert.ToString(cboStaffRole.SelectedItem)
            Dim pass As String = txtStaffPassword.Text

            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(fullName) Then
                lblStaffMsg.Text = "Username and Full Name are required."
                lblStaffMsg.ForeColor = Color.Red
                Return
            End If

            If Not ValidationRules.IsValidUsername(username) Then
                lblStaffMsg.Text = "Username must be 3-20 characters; letters, numbers, dots and underscores only (no spaces)."
                lblStaffMsg.ForeColor = Color.Red
                Return
            End If

            If Not ValidationRules.IsValidFullName(fullName) Then
                lblStaffMsg.Text = "Full name must contain only letters, spaces, hyphens, apostrophes, dots and commas."
                lblStaffMsg.ForeColor = Color.Red
                Return
            End If

            ' Fixed role list validation: only Cashier or InventoryClerk may be assigned
            If role <> "Cashier" AndAlso role <> "InventoryClerk" Then
                lblStaffMsg.Text = "Role must be either 'Cashier' or 'InventoryClerk'."
                lblStaffMsg.ForeColor = Color.Red
                Return
            End If

            Try
                If _selectedStaffId = 0 Then
                    ' Creating new staff
                    If Not ValidationRules.IsValidPassword(pass) Then
                        lblStaffMsg.Text = "Initial password must be at least 6 characters and contain no spaces."
                        lblStaffMsg.ForeColor = Color.Red
                        Return
                    End If

                    If _userRepo.UsernameExists(username) Then
                        lblStaffMsg.Text = "Username already exists."
                        lblStaffMsg.ForeColor = Color.Red
                        Return
                    End If

                    Dim salt As String = AuthService.GenerateSalt()
                    Dim hash As String = AuthService.HashPassword(pass, salt)

                    Dim newStaff As User
                    If role = "InventoryClerk" Then
                        newStaff = New InventoryClerk()
                    Else
                        newStaff = New Cashier()
                    End If

                    newStaff.Username = username
                    newStaff.FullName = fullName
                    newStaff.Role = role
                    newStaff.PasswordHash = hash
                    newStaff.Salt = salt
                    newStaff.IsActive = True
                    newStaff.CreatedAt = DateTime.Now

                    _userRepo.CreateUser(newStaff)
                    lblStaffMsg.Text = "Staff account created successfully!"
                    lblStaffMsg.ForeColor = Color.DarkGreen
                Else
                    ' Updating existing staff
                    Dim staff As User = _userRepo.GetById(_selectedStaffId)
                    If staff IsNot Nothing Then
                        staff.FullName = fullName
                        staff.Role = role
                        _userRepo.UpdateUser(staff)
                        lblStaffMsg.Text = "Staff profile updated successfully!"
                        lblStaffMsg.ForeColor = Color.DarkGreen
                    End If
                End If

                LoadStaffList()
            Catch ex As Exception
                lblStaffMsg.Text = ex.Message
                lblStaffMsg.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub btnStaffResetPass_Click(sender As Object, e As EventArgs) Handles btnStaffResetPass.Click
            If _selectedStaffId <= 0 Then
                MessageBox.Show("Please select a staff member to reset password.", "Select Staff", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim newPass As String = txtStaffPassword.Text
            If Not ValidationRules.IsValidPassword(newPass) Then
                MessageBox.Show("New password must be at least 6 characters and contain no spaces.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtStaffPassword.Focus()
                Return
            End If

            Try
                Dim newSalt As String = AuthService.GenerateSalt()
                Dim newHash As String = AuthService.HashPassword(newPass, newSalt)
                If _userRepo.UpdatePassword(_selectedStaffId, newHash, newSalt) Then
                    lblStaffMsg.Text = "Password reset successfully!"
                    lblStaffMsg.ForeColor = Color.DarkGreen
                    txtStaffPassword.Clear()
                End If
            Catch ex As Exception
                lblStaffMsg.Text = ex.Message
                lblStaffMsg.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub btnStaffToggleActive_Click(sender As Object, e As EventArgs) Handles btnStaffToggleActive.Click
            If _selectedStaffId <= 0 Then Return
            Dim staff As User = _userRepo.GetById(_selectedStaffId)
            If staff Is Nothing Then Return

            If staff.UserId = _owner.UserId Then
                MessageBox.Show("Cannot deactivate the primary Store Owner account.", "Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim newStatus As Boolean = Not staff.IsActive
            Dim actionName As String = If(newStatus, "activate", "deactivate")

            If MessageBox.Show($"Are you sure you want to {actionName} account '{staff.Username}'?", "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                _userRepo.SetActiveStatus(staff.UserId, newStatus)
                LoadStaffList()
                lblStaffMsg.Text = $"Account '{staff.Username}' has been {actionName}d."
                lblStaffMsg.ForeColor = Color.DarkGreen
            End If
        End Sub

        ' =====================================================================
        ' TAB 3: PRODUCT MANAGEMENT (CRUD)
        ' =====================================================================
        Private Sub PopulateCategoriesAndSuppliers()
            cboProdCategory.Items.Clear()
            cboProdCatFilter.Items.Clear()
            cboProdCatFilter.Items.Add("All Categories")

            Dim cats As List(Of Category) = _categoryRepo.GetAll()
            For Each c In cats
                cboProdCategory.Items.Add(c)
                cboProdCatFilter.Items.Add(c)
            Next
            If cboProdCategory.Items.Count > 0 Then cboProdCategory.SelectedIndex = 0
            cboProdCatFilter.SelectedIndex = 0

            cboProdSupplier.Items.Clear()
            Dim sups As List(Of Supplier) = _supplierRepo.GetAll()
            For Each s In sups
                cboProdSupplier.Items.Add(s)
            Next
            If cboProdSupplier.Items.Count > 0 Then cboProdSupplier.SelectedIndex = 0
        End Sub

        Private Sub LoadProductsList()
            dgvProducts.Rows.Clear()
            Dim kw As String = txtProdSearch.Text.Trim()
            Dim catId As Nullable(Of Integer) = Nothing

            If cboProdCatFilter.SelectedIndex > 0 Then
                Dim c = TryCast(cboProdCatFilter.SelectedItem, Category)
                If c IsNot Nothing Then catId = c.CategoryId
            End If

            Dim prods As List(Of Product) = _productRepo.Search(kw, catId, activeOnly:=False)
            For Each p In prods
                Dim statusStr As String = If(p.IsActive, "Yes", "Inactive")
                Dim rowIdx As Integer = dgvProducts.Rows.Add(p.ProductCode, p.Name, p.CategoryName, p.Price.ToString("N2"), p.CostPrice.ToString("N2"), p.StockQty, statusStr)
                If Not p.IsActive Then
                    dgvProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.Gray
                ElseIf p.IsLowStock() Then
                    dgvProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.DarkOrange
                End If
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateProductsFooter(prods.Count)
        End Sub

        Private Sub txtProdSearch_TextChanged(sender As Object, e As EventArgs) Handles txtProdSearch.TextChanged
            LoadProductsList()
        End Sub

        Private Sub cboProdCatFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProdCatFilter.SelectedIndexChanged
            LoadProductsList()
        End Sub

        Private Sub dgvProducts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvProducts.SelectionChanged
            If dgvProducts.SelectedRows.Count = 0 Then Return
            Dim code As String = Convert.ToString(dgvProducts.SelectedRows(0).Cells(0).Value)
            Dim prod As Product = _productRepo.GetByCode(code)
            If prod Is Nothing Then Return

            _selectedProductId = prod.ProductId
            txtProdCode.Text = prod.ProductCode
            txtProdName.Text = prod.Name
            txtProdPrice.Text = prod.Price.ToString("F2")
            txtProdCost.Text = prod.CostPrice.ToString("F2")
            numProdStock.Value = Math.Max(0, prod.StockQty)
            numProdReorder.Value = Math.Max(0, prod.ReorderLevel)
            chkProdActive.Checked = prod.IsActive

            If prod.ExpiryDate.HasValue Then
                dtpProdExpiry.Checked = True
                dtpProdExpiry.Value = prod.ExpiryDate.Value
            Else
                dtpProdExpiry.Checked = False
            End If

            ' Match Category
            For i As Integer = 0 To cboProdCategory.Items.Count - 1
                Dim c = TryCast(cboProdCategory.Items(i), Category)
                If c IsNot Nothing AndAlso c.CategoryId = prod.CategoryId Then
                    cboProdCategory.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Match Supplier
            For i As Integer = 0 To cboProdSupplier.Items.Count - 1
                Dim s = TryCast(cboProdSupplier.Items(i), Supplier)
                If s IsNot Nothing AndAlso s.SupplierId = prod.SupplierId Then
                    cboProdSupplier.SelectedIndex = i
                    Exit For
                End If
            Next

            lblProdMsg.Text = $"Editing product #{prod.ProductCode}"
            lblProdMsg.ForeColor = Color.Black
        End Sub

        Private Sub btnProdNew_Click(sender As Object, e As EventArgs) Handles btnProdNew.Click
            _selectedProductId = 0
            txtProdCode.Clear()
            txtProdName.Clear()
            txtProdPrice.Clear()
            txtProdCost.Clear()
            numProdStock.Value = 10
            numProdReorder.Value = 5
            dtpProdExpiry.Checked = False
            chkProdActive.Checked = True
            lblProdMsg.Text = "Ready to add new product."
            lblProdMsg.ForeColor = Color.Black
            txtProdCode.Focus()
        End Sub

        Private Sub btnProdSave_Click(sender As Object, e As EventArgs) Handles btnProdSave.Click
            lblProdMsg.Text = String.Empty

            Dim code As String = txtProdCode.Text.Trim().ToUpperInvariant()
            Dim name As String = txtProdName.Text.Trim()
            Dim price As Decimal = 0D
            Dim cost As Decimal = 0D

            If String.IsNullOrWhiteSpace(code) OrElse String.IsNullOrWhiteSpace(name) Then
                lblProdMsg.Text = "Product code and name are required."
                lblProdMsg.ForeColor = Color.Red
                Return
            End If

            ' BUSINESS RULE: product code charset limited to letters, digits and dash
            If Not System.Text.RegularExpressions.Regex.IsMatch(code, "^[A-Z0-9-]+$") Then
                lblProdMsg.Text = "Product code may only contain letters, numbers and dashes."
                lblProdMsg.ForeColor = Color.Red
                Return
            End If

            If Not Decimal.TryParse(txtProdPrice.Text.Trim(), price) OrElse Not ValidationRules.IsValidMoney(price) OrElse price <= 0D Then
                lblProdMsg.Text = $"Retail price must be a positive amount (max PHP {ValidationRules.MaxMoney:N2}, up to 2 decimals)."
                lblProdMsg.ForeColor = Color.Red
                Return
            End If

            If Not Decimal.TryParse(txtProdCost.Text.Trim(), cost) OrElse Not ValidationRules.IsValidMoney(cost) OrElse cost <= 0D Then
                lblProdMsg.Text = $"Cost price must be a positive amount (max PHP {ValidationRules.MaxMoney:N2}, up to 2 decimals)."
                lblProdMsg.ForeColor = Color.Red
                Return
            End If

            Dim expDate As Nullable(Of DateTime) = If(dtpProdExpiry.Checked, dtpProdExpiry.Value.Date, CType(Nothing, Nullable(Of DateTime)))

            ' BUSINESS RULE: expiry date cannot be in the past
            If expDate.HasValue AndAlso expDate.Value.Date <= DateTime.Today Then
                lblProdMsg.Text = "Expiry date must be in the future."
                lblProdMsg.ForeColor = Color.Red
                Return
            End If

            Dim selectedCat = TryCast(cboProdCategory.SelectedItem, Category)
            Dim selectedSup = TryCast(cboProdSupplier.SelectedItem, Supplier)

            If selectedCat Is Nothing OrElse selectedSup Is Nothing Then
                lblProdMsg.Text = "Please select valid Category and Supplier."
                lblProdMsg.ForeColor = Color.Red
                Return
            End If

            ' BUSINESS RULE: warn if selling price is below the cost price
            If price < cost Then
                Dim warnRes As DialogResult = MessageBox.Show(
                    $"WARNING: Retail price (PHP {price:N2}) is BELOW cost price (PHP {cost:N2})." & vbCrLf &
                    "Saving this product will cause a loss on every sale. Save anyway?",
                    "Price Below Cost", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If warnRes <> DialogResult.Yes Then
                    Return
                End If
            End If

            Try
                Dim p As New Product() With {
                    .ProductId = _selectedProductId,
                    .ProductCode = code,
                    .Name = name,
                    .CategoryId = selectedCat.CategoryId,
                    .SupplierId = selectedSup.SupplierId,
                    .Price = price,
                    .CostPrice = cost,
                    .StockQty = Convert.ToInt32(numProdStock.Value),
                    .ReorderLevel = Convert.ToInt32(numProdReorder.Value),
                    .ExpiryDate = expDate,
                    .IsActive = chkProdActive.Checked
                }

                If _selectedProductId = 0 Then
                    Dim existing = _productRepo.GetByCode(code)
                    If existing IsNot Nothing Then
                        lblProdMsg.Text = "Product code already exists."
                        lblProdMsg.ForeColor = Color.Red
                        Return
                    End If

                    ' BUSINESS RULE: duplicate product name (case-insensitive) rejected
                    Dim dupeName = _productRepo.GetAll(activeOnly:=False).FirstOrDefault(
                        Function(x) x.Name.Trim().Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                    If dupeName IsNot Nothing Then
                        lblProdMsg.Text = $"Product name already exists (code {dupeName.ProductCode})."
                        lblProdMsg.ForeColor = Color.Red
                        Return
                    End If

                    _productRepo.Add(p)
                    lblProdMsg.Text = "Product added successfully!"
                    lblProdMsg.ForeColor = Color.DarkGreen
                Else
                    ' Duplicate guard on update (exclude the product being edited)
                    Dim dupeNameUpd = _productRepo.GetAll(activeOnly:=False).FirstOrDefault(
                        Function(x) x.ProductId <> _selectedProductId AndAlso
                                    x.Name.Trim().Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                    If dupeNameUpd IsNot Nothing Then
                        lblProdMsg.Text = $"Product name already exists (code {dupeNameUpd.ProductCode})."
                        lblProdMsg.ForeColor = Color.Red
                        Return
                    End If

                    _productRepo.Update(p)
                    lblProdMsg.Text = "Product updated successfully!"
                    lblProdMsg.ForeColor = Color.DarkGreen
                End If

                LoadProductsList()
                LoadOverview()
            Catch ex As Exception
                lblProdMsg.Text = ex.Message
                lblProdMsg.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub btnProdDelete_Click(sender As Object, e As EventArgs) Handles btnProdDelete.Click
            If _selectedProductId <= 0 Then Return

            If MessageBox.Show("Are you sure you want to deactivate this product?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                _productRepo.DeleteOrDeactivate(_selectedProductId)
                lblProdMsg.Text = "Product has been deactivated."
                lblProdMsg.ForeColor = Color.DarkGreen
                LoadProductsList()
                LoadOverview()
            End If
        End Sub

        ' =====================================================================
        ' TAB 4: CATEGORIES & SUPPLIERS CRUD
        ' =====================================================================
        Private Sub LoadCategoriesList()
            dgvCategories.Rows.Clear()
            Dim cats As List(Of Category) = _categoryRepo.GetAll()
            For Each c In cats
                dgvCategories.Rows.Add(c.CategoryId, c.Name)
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateCategoriesFooter(cats.Count)
        End Sub

        Private Sub dgvCategories_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCategories.SelectionChanged
            If dgvCategories.SelectedRows.Count = 0 Then Return
            _selectedCategoryId = Convert.ToInt32(dgvCategories.SelectedRows(0).Cells(0).Value)
            txtCatName.Text = Convert.ToString(dgvCategories.SelectedRows(0).Cells(1).Value)
        End Sub

        Private Sub btnCatSave_Click(sender As Object, e As EventArgs) Handles btnCatSave.Click
            Dim name As String = txtCatName.Text.Trim()
            If String.IsNullOrWhiteSpace(name) Then
                MessageBox.Show("Category name cannot be blank.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' BUSINESS RULE: duplicate category name (case-insensitive) rejected
            Dim dupe As Category = _categoryRepo.GetAll().FirstOrDefault(
                Function(c) c.CategoryId <> _selectedCategoryId AndAlso
                            c.Name.Trim().Equals(name, StringComparison.OrdinalIgnoreCase))
            If dupe IsNot Nothing Then
                MessageBox.Show($"Category '{name}' already exists.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                If _selectedCategoryId = 0 Then
                    _categoryRepo.Add(name)
                Else
                    _categoryRepo.Update(_selectedCategoryId, name)
                End If
                txtCatName.Clear()
                _selectedCategoryId = 0
                LoadCategoriesList()
                PopulateCategoriesAndSuppliers()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnCatNew_Click(sender As Object, e As EventArgs)
            _selectedCategoryId = 0
            txtCatName.Clear()
            txtCatName.Focus()
        End Sub

        Private Sub btnCatDelete_Click(sender As Object, e As EventArgs) Handles btnCatDelete.Click
            If _selectedCategoryId <= 0 Then Return
            If MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    _categoryRepo.Delete(_selectedCategoryId)
                    txtCatName.Clear()
                    _selectedCategoryId = 0
                    LoadCategoriesList()
                    PopulateCategoriesAndSuppliers()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        End Sub

        Private Sub LoadSuppliersList()
            dgvSuppliers.Rows.Clear()
            Dim sups As List(Of Supplier) = _supplierRepo.GetAll()
            For Each s In sups
                dgvSuppliers.Rows.Add(s.SupplierId, s.Name, s.ContactNumber, s.Address)
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateSuppliersFooter(sups.Count)
        End Sub

        Private Sub dgvSuppliers_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSuppliers.SelectionChanged
            If dgvSuppliers.SelectedRows.Count = 0 Then Return
            _selectedSupplierId = Convert.ToInt32(dgvSuppliers.SelectedRows(0).Cells(0).Value)
            txtSupName.Text = Convert.ToString(dgvSuppliers.SelectedRows(0).Cells(1).Value)
            txtSupContact.Text = Convert.ToString(dgvSuppliers.SelectedRows(0).Cells(2).Value)
            txtSupAddr.Text = Convert.ToString(dgvSuppliers.SelectedRows(0).Cells(3).Value)
        End Sub

        Private Sub btnSupNew_Click(sender As Object, e As EventArgs) Handles btnSupNew.Click
            _selectedSupplierId = 0
            txtSupName.Clear()
            txtSupContact.Clear()
            txtSupAddr.Clear()
            txtSupName.Focus()
        End Sub

        Private Sub btnSupSave_Click(sender As Object, e As EventArgs) Handles btnSupSave.Click
            Dim name As String = txtSupName.Text.Trim()
            If String.IsNullOrWhiteSpace(name) Then
                MessageBox.Show("Supplier name cannot be blank.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If Not ValidationRules.IsValidContactNumber(txtSupContact.Text.Trim()) Then
                MessageBox.Show("Contact number must be 7-15 digits (optionally starting with +).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtSupContact.Focus()
                Return
            End If

            ' BUSINESS RULE: duplicate supplier name (case-insensitive) rejected
            Dim dupe As Supplier = _supplierRepo.GetAll().FirstOrDefault(
                Function(s) s.SupplierId <> _selectedSupplierId AndAlso
                            s.Name.Trim().Equals(name, StringComparison.OrdinalIgnoreCase))
            If dupe IsNot Nothing Then
                MessageBox.Show($"Supplier '{name}' already exists.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sup As New Supplier(_selectedSupplierId, name, txtSupContact.Text.Trim(), txtSupAddr.Text.Trim())

            Try
                If _selectedSupplierId = 0 Then
                    _supplierRepo.Add(sup)
                Else
                    _supplierRepo.Update(sup)
                End If
                btnSupNew.PerformClick()
                LoadSuppliersList()
                PopulateCategoriesAndSuppliers()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnSupDelete_Click(sender As Object, e As EventArgs) Handles btnSupDelete.Click
            If _selectedSupplierId <= 0 Then Return
            If MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    _supplierRepo.Delete(_selectedSupplierId)
                    btnSupNew.PerformClick()
                    LoadSuppliersList()
                    PopulateCategoriesAndSuppliers()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        End Sub

        ' =====================================================================
        ' TAB 5: TRANSACTION HISTORY
        ' =====================================================================
        Private Sub LoadTransactions()
            dgvTransactions.Rows.Clear()
            Dim sDate As DateTime = dtpTxStart.Value.Date
            Dim eDate As DateTime = dtpTxEnd.Value.Date.AddDays(1).AddSeconds(-1)

            Dim sales As List(Of Sale) = _saleRepo.GetSalesByDateRange(sDate, eDate)
            For Each s In sales
                dgvTransactions.Rows.Add(s.ReceiptNo, s.SaleDate.ToString("yyyy-MM-dd hh:mm tt"), s.CashierName, s.CustomerName, s.Subtotal.ToString("N2"), s.DiscountAmount.ToString("N2"), s.Total.ToString("N2"))
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateTransactionsFooter(sales.Count)
        End Sub

        Private Sub TxDateFilter_Changed(sender As Object, e As EventArgs) Handles dtpTxStart.ValueChanged, dtpTxEnd.ValueChanged
            LoadTransactions()
        End Sub

        Private Sub btnTxViewDetails_Click(sender As Object, e As EventArgs) Handles btnTxViewDetails.Click
            If dgvTransactions.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a transaction from the list.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim receiptNo As String = Convert.ToString(dgvTransactions.SelectedRows(0).Cells(0).Value)
            Dim sale As Sale = _saleRepo.GetByReceiptNo(receiptNo)
            If sale IsNot Nothing Then
                Using dlg As New PastReceiptForm(sale)
                    dlg.ShowDialog(Me)
                End Using
            End If
        End Sub

        ' =====================================================================
        ' TAB 6: REPORTS & ANALYTICS
        ' =====================================================================
        Private Sub PopulateReportTypes()
            cboReportType.Items.Clear()
            cboReportType.Items.Add("Daily Sales Report")
            cboReportType.Items.Add("Inventory Valuation Report")
            cboReportType.Items.Add("Low-Stock & Expiry Alert Report")
            cboReportType.SelectedIndex = 0
        End Sub

        Private Sub cboReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReportType.SelectedIndexChanged
            dtpReportDate.Enabled = (cboReportType.SelectedIndex = 0)
        End Sub

        Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
            Dim stats As Dictionary(Of String, Object) = Nothing
            Dim selectedIdx As Integer = cboReportType.SelectedIndex

            Select Case selectedIdx
                Case 0
                    _currentReportData = ReportService.GenerateDailySalesReport(dtpReportDate.Value.Date, stats)
                    lblReportSummary.Text = $"Daily Sales for {dtpReportDate.Value:yyyy-MM-dd}: {stats("TransactionCount")} Transactions | Gross: PHP {CDec(stats("TotalGross")):N2} | Discounts: PHP {CDec(stats("TotalDiscount")):N2} | Net Total: PHP {CDec(stats("TotalNet")):N2}"
                Case 1
                    _currentReportData = ReportService.GenerateInventoryReport(stats)
                    lblReportSummary.Text = $"Inventory Valuation: {stats("TotalProducts")} products ({stats("TotalUnits")} units) | Cost Valuation: PHP {CDec(stats("TotalCostValue")):N2} | Retail Valuation: PHP {CDec(stats("TotalRetailValue")):N2} | Potential Gross Margin: PHP {CDec(stats("PotentialProfit")):N2}"
                Case 2
                    _currentReportData = ReportService.GenerateLowStockAndExpiryReport()
                    lblReportSummary.Text = $"Alert Summary: {_currentReportData.Rows.Count} critical item(s) flagged for low-stock or expiration."
            End Select

            dgvReport.DataSource = _currentReportData
        End Sub

        Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
            If _currentReportData Is Nothing OrElse _currentReportData.Rows.Count = 0 Then
                MessageBox.Show("Please generate a report with data before exporting to CSV.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv"
                sfd.FileName = $"{cboReportType.SelectedItem.ToString().Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                If sfd.ShowDialog(Me) = DialogResult.OK Then
                    Try
                        ReportService.ExportDataTableToCsv(_currentReportData, sfd.FileName)
                        MessageBox.Show($"Report successfully exported to:{vbCrLf}{sfd.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show($"Export error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub

        Private Sub btnPrintReport_Click(sender As Object, e As EventArgs) Handles btnPrintReport.Click
            If _currentReportData Is Nothing OrElse _currentReportData.Rows.Count = 0 Then
                MessageBox.Show("Please generate a report before printing.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Try
                ReportService.PrintReport(cboReportType.SelectedItem.ToString(), _currentReportData, Me)
            Catch ex As Exception
                MessageBox.Show($"Print preview error: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
            If MessageBox.Show("Are you sure you want to log out of the owner administration?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Close()
            End If
        End Sub

        ' =====================================================================
        ' TYPING-LEVEL INPUT VALIDATION
        ' =====================================================================
        Private Sub BlockSpaceTyping(sender As Object, e As KeyPressEventArgs) _
            Handles txtStaffUsername.KeyPress, txtStaffPassword.KeyPress
            If Char.IsWhiteSpace(e.KeyChar) Then
                e.Handled = True
                lblStaffMsg.Text = "Spaces are not allowed in this field."
                lblStaffMsg.ForeColor = Color.Red
            End If
        End Sub

        Private Sub BlockDigitsInNames(sender As Object, e As KeyPressEventArgs) _
            Handles txtStaffName.KeyPress
            Dim allowedPunct As String = " '-.,"
            If Not (Char.IsControl(e.KeyChar) OrElse Char.IsLetter(e.KeyChar) OrElse
                    Char.IsWhiteSpace(e.KeyChar) OrElse allowedPunct.Contains(e.KeyChar.ToString())) Then
                e.Handled = True
                lblStaffMsg.Text = "Numbers and symbols are not allowed in this field."
                lblStaffMsg.ForeColor = Color.Red
            End If
        End Sub

#Region "Figma UI restyle (visual only - no business behavior changes)"

        ' ================= UI-only state =================
        Private _lblOverviewFooter As Label
        Private _lblOverviewEmpty As Label
        Private _lblStaffFooter As Label
        Private _lblStaffEmpty As Label
        Private _lblProductsFooter As Label
        Private _lblProductsEmpty As Label
        Private _lblCatFooter As Label
        Private _lblCatEmpty As Label
        Private _lblSupFooter As Label
        Private _lblSupEmpty As Label
        Private _lblTxFooter As Label
        Private _lblTxEmpty As Label
        Private _lblReportEmpty As Label

        ' Nutrition catalog (new, placeholder-backed)
        Private _tabNutrition As TabPage
        Private _nutritionTable As NutritionTableControl
        Private _nutritionRail As ProgressRail
        Private _nutritionStateLabel As Label
        Private _nutritionProcessedLabel As Label
        Private _nutritionValidLabel As Label
        Private _nutritionReviewLabel As Label
        Private _nutritionNotice As InlineNotice
        Private _btnNutritionImport As Button
        Private _btnNutritionDetails As Button

        ''' <summary>Applies the design tokens + layout. Visual only.</summary>
        Private Sub InitializeUiRestyle()
            _tabNutrition = New TabPage()
            _tabNutrition.Name = "tabNutrition"
            _tabNutrition.Text = "Nutrition catalog"
            tabOwner.Controls.Add(_tabNutrition)

            ' QA E: run-time restore/minimum sizes matched with the other portals
            ' (the designer still said 1084x711), then open maximized.
            Me.ClientSize = New Size(1280, 800)
            Me.MinimumSize = New Size(1024, 700)
            Me.WindowState = FormWindowState.Maximized

            AppHeader.Apply(pnlHeader, lblAppTitle, lblOwnerInfo, btnLogout)
            ShellTabs.Apply(tabOwner,
                New ShellTabSpec(tabOverview, "Dashboard", "dashboard"),
                New ShellTabSpec(tabStaff, "Staff", "users"),
                New ShellTabSpec(tabProducts, "Products", "package"),
                New ShellTabSpec(tabMasterData, "Categories & Suppliers", "tags"),
                New ShellTabSpec(tabTransactions, "Transactions", "receipt"),
                New ShellTabSpec(tabReports, "Reports", "chart"),
                New ShellTabSpec(_tabNutrition, "Nutrition Catalog", "leaf"))

            RestyleOverview()
            RestyleStaff()
            RestyleProducts()
            RestyleMasterData()
            RestyleTransactions()
            RestyleReports()
            BuildNutritionTab()

            _nutritionTable.LoadOwnerCatalog(_productRepo.GetAll(activeOnly:=True))
            RefreshNutritionImportCard()

            Theme.RefreshLayout(Me)
        End Sub

#Region "RestyleHelpers"
        Private Function FormLabelO(lbl As Label, text As String) As Label
            lbl.Text = text
            lbl.Font = Theme.FontBody12
            lbl.ForeColor = Theme.Muted
            lbl.AutoSize = True
            lbl.BackColor = Color.Transparent
            lbl.Margin = New Padding(0, 12, 0, 0)
            Return lbl
        End Function

        Private Function WrapTBO(tb As TextBox, Optional cue As String = Nothing) As InputPanel
            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.Fill
            wrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleInput(tb)
            If Not String.IsNullOrEmpty(cue) Then
                Theme.SetCue(tb, cue)
            End If
            wrap.Controls.Add(tb)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    tb.Location = New Point(12, Math.Max(0, (wrap.ClientSize.Height - tb.Height) \ 2))
                    tb.Width = wrap.ClientSize.Width - 24
                End Sub
            Return wrap
        End Function

        Private Function WrapComboO(cbo As ComboBox) As InputPanel
            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.Fill
            wrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleCombo(cbo)
            wrap.Controls.Add(cbo)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    cbo.Location = New Point(10, Math.Max(0, (wrap.ClientSize.Height - cbo.Height) \ 2))
                    cbo.Width = Math.Max(60, wrap.ClientSize.Width - 20)
                End Sub
            Return wrap
        End Function

        Private Function WrapNumO(n As NumericUpDown, width As Integer) As InputPanel
            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.Fill
            wrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleNum(n)
            wrap.Controls.Add(n)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    n.Location = New Point(12, Math.Max(0, (wrap.ClientSize.Height - n.Height) \ 2))
                    n.Width = width
                End Sub
            Return wrap
        End Function

        Private Function MsgRowO(btn As Button, msg As Label) As Panel
            msg.Font = Theme.FontBody12
            msg.AutoSize = False
            msg.Dock = DockStyle.Fill
            msg.TextAlign = ContentAlignment.MiddleLeft
            msg.BackColor = Color.Transparent

            Dim row As New Panel()
            row.Dock = DockStyle.Fill
            row.Height = 40
            row.Margin = New Padding(0, 14, 0, 0)
            row.BackColor = Color.Transparent
            row.Controls.Add(msg)
            row.Controls.Add(btn)
            AddHandler row.Resize,
                Sub(s As Object, e As EventArgs)
                    btn.Location = New Point(0, 0)
                    msg.Location = New Point(btn.Width + 12, (row.ClientSize.Height - Math.Min(20, msg.Height)) \ 2)
                    msg.Size = New Size(Math.Max(60, row.ClientSize.Width - btn.Width - 16), row.ClientSize.Height)
                End Sub
            Return row
        End Function
#End Region

        ' ================= Dashboard / Overview =================
        Private Sub RestyleOverview()
            tabOverview.BackColor = Theme.PageBg

            pnlOverviewRecentTitle.Visible = False
            tabOverview.Controls.Remove(pnlOverviewRecentTitle)

            ' KPI card chrome (positions remain handled by LayoutKpiCards)
            pnlCards.Height = 128
            For Each kpi As Panel In New Panel() {pnlCard1, pnlCard2, pnlCard3, pnlCard4}
                kpi.BackColor = Theme.White
                kpi.Height = 108
                Theme.RoundControl(kpi, Theme.RadiusCard)
                AddHandler kpi.Paint,
                    Sub(s As Object, e As PaintEventArgs)
                        Using path As System.Drawing.Drawing2D.GraphicsPath = Theme.RoundedPath(
                            New Rectangle(0, 0, kpi.ClientSize.Width - 1, kpi.ClientSize.Height - 1), Theme.RadiusCard)
                            Using p As New Pen(Theme.BorderLine, 1.0F)
                                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
                                e.Graphics.DrawPath(p, path)
                            End Using
                        End Using
                    End Sub
            Next

            Dim titles() As Label = {lblCard1Title, lblCard2Title, lblCard3Title, lblCard4Title}
            Dim values() As Label = {lblCard1Val, lblCard2Val, lblCard3Val, lblCard4Val}
            Dim kpis() As Panel = {pnlCard1, pnlCard2, pnlCard3, pnlCard4}
            For i As Integer = 0 To 3
                titles(i).Font = Theme.FontSmall
                titles(i).ForeColor = Theme.Muted
                titles(i).BackColor = Color.Transparent
                values(i).Font = Theme.F(23.0F, ThemeWeight.Semibold)
                values(i).ForeColor = Theme.Ink
                values(i).BackColor = Color.Transparent
                ' KPI internals: title pinned top-left, value pinned bottom-left,
                ' both sized to the card at every resize (equal widths come from
                ' LayoutKpiCards). Values step down one font size when narrow so
                ' amounts like "PHP 646.00" are never clipped.
                titles(i).AutoSize = False
                titles(i).TextAlign = ContentAlignment.MiddleLeft
                titles(i).Anchor = AnchorStyles.Top Or AnchorStyles.Left
                values(i).AutoSize = False
                values(i).TextAlign = ContentAlignment.MiddleLeft
                values(i).Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            Next

            For i As Integer = 0 To 3
                AddHandler kpis(i).Resize, Sub(s As Object, e As EventArgs) ApplyKpiCardLayout()
            Next
            ApplyKpiCardLayout()

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvOverviewRecent)
            GridStyler.ApplyWeights(dgvOverviewRecent, 1.4, 1.0, 2.0, 1.4)
            dgvOverviewRecent.Dock = DockStyle.Fill ' fills the white body (was a fixed strip)
            body.Controls.Add(dgvOverviewRecent)
            _lblOverviewEmpty = GridStyler.AttachEmptyOverlay(dgvOverviewRecent, "No transactions today yet.")

            _lblOverviewFooter = GridStyler.NewFooter()
            _lblOverviewFooter.Height = 24
            _lblOverviewFooter.Dock = DockStyle.Bottom
            _lblOverviewFooter.BackColor = Theme.White

            Dim header As New Panel()
            header.Dock = DockStyle.Top
            header.Height = 30
            header.BackColor = Theme.White
            header.Margin = New Padding(0)

            lblRecentTitle.TextAlign = ContentAlignment.MiddleLeft
            lblRecentTitle.Font = Theme.FontSection
            lblRecentTitle.ForeColor = Theme.Ink
            lblRecentTitle.Dock = DockStyle.Fill
            lblRecentTitle.BackColor = Theme.White
            lblRecentTitle.Margin = New Padding(0)
            header.Controls.Add(lblRecentTitle)

            card.Controls.Add(body)
            card.Controls.Add(_lblOverviewFooter)
            card.Controls.Add(header)

            tabOverview.Controls.Add(card)

            Dim heading As PageHeading = PageHeading.Build(tabOverview, "Dashboard", _
                "Today's performance and inventory health at a glance.")
            heading.AddChip(New StatusChip("Live now", ThemeTone.Success))
        End Sub

        ''' <summary>Pins each KPI title top-left and its value bottom-left inside the
        ''' card, stepping the value font down when the card is narrow so amounts
        ''' like "PHP 646.00" and titles never clip. (Equal widths remain the
        ''' job of LayoutKpiCards.)</summary>
        Private Sub ApplyKpiCardLayout()
            Dim kpis() As Panel = {pnlCard1, pnlCard2, pnlCard3, pnlCard4}
            Dim titles() As Label = {lblCard1Title, lblCard2Title, lblCard3Title, lblCard4Title}
            Dim values() As Label = {lblCard1Val, lblCard2Val, lblCard3Val, lblCard4Val}
            Dim vBig As Font = Theme.F(23.0F, ThemeWeight.Semibold)
            Dim vCompact As Font = Theme.F(17.0F, ThemeWeight.Semibold)
            Dim vTiny As Font = Theme.F(14.0F, ThemeWeight.Semibold)
            For i As Integer = 0 To 3
                If kpis(i) Is Nothing OrElse titles(i) Is Nothing OrElse values(i) Is Nothing Then Continue For
                Dim cardW As Integer = Math.Max(80, kpis(i).ClientSize.Width - 28)
                Dim cardH As Integer = kpis(i).ClientSize.Height
                titles(i).SetBounds(14, 14, cardW, 18)
                Dim v As Font = vBig
                Dim t As String = values(i).Text
                If t.Length = 0 OrElse TextRenderer.MeasureText(t, v, Size.Empty,
                        TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix).Width > cardW Then
                    v = vCompact
                    If t.Length > 0 AndAlso TextRenderer.MeasureText(t, v, Size.Empty,
                            TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix).Width > cardW Then
                        v = vTiny
                    End If
                    values(i).Font = v
                End If
                values(i).SetBounds(14, Math.Max(30, cardH - v.Height - 12), cardW, v.Height + 4)
            Next
        End Sub

        Private Sub UpdateOverviewFooter(total As Integer)
            If _lblOverviewFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblOverviewFooter, total, total, If(total = 1, "sale", "sales"))
            If _lblOverviewEmpty IsNot Nothing Then _lblOverviewEmpty.Visible = (dgvOverviewRecent.RowCount = 0)
        End Sub

        ' ================= Staff =================
        Private Sub RestyleStaff()
            tabStaff.BackColor = Theme.PageBg

            grpStaffEdit.Visible = False
            tabStaff.Controls.Remove(grpStaffEdit)

            Dim tableCard As New CardPanel()
            tableCard.Dock = DockStyle.Fill
            tableCard.Margin = New Padding(0)
            tableCard.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvStaff)
            GridStyler.ApplyWeights(dgvStaff, 0.7, 1.2, 2.0, 1.2, 1.0, 1.1)
            body.Controls.Add(dgvStaff)
            _lblStaffEmpty = GridStyler.AttachEmptyOverlay(dgvStaff, "No staff accounts found.")

            _lblStaffFooter = GridStyler.NewFooter()
            _lblStaffFooter.Height = 24
            _lblStaffFooter.Dock = DockStyle.Bottom
            _lblStaffFooter.BackColor = Theme.White

            tableCard.Controls.Add(body)
            tableCard.Controls.Add(_lblStaffFooter)

            Dim editCard As New CardPanel()
            editCard.Dock = DockStyle.Bottom
            editCard.Height = 320 ' was 236 - password field / role combo / message got clipped
            editCard.Margin = New Padding(0, 12, 0, 0)
            editCard.Padding = New Padding(16, 12, 16, 12)

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Fill
            tlp.BackColor = Theme.White
            tlp.ColumnCount = 2
            tlp.RowCount = 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 46F))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 54F))
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            Dim fields As New TableLayoutPanel()
            fields.Dock = DockStyle.Fill
            fields.BackColor = Theme.White
            fields.ColumnCount = 1
            fields.RowCount = 9
            fields.Margin = New Padding(0)
            For r As Integer = 0 To 7
                fields.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            fields.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            Dim addF As Action(Of Control) = Sub(c As Control)
                                                 fields.Controls.Add(c, 0, fields.Controls.Count)
                                             End Sub

            Dim lblTitle As New Label()
            lblTitle.Text = "Staff account details"
            lblTitle.Font = Theme.FontCardTitle
            lblTitle.ForeColor = Theme.Ink
            lblTitle.AutoSize = True
            lblTitle.Margin = New Padding(0, 0, 0, 4)
            addF(lblTitle)

            addF(FormLabelO(lblStaffUsername, "Username"))
            addF(WrapTBO(txtStaffUsername))
            addF(FormLabelO(lblStaffFullName, "Full name"))
            addF(WrapTBO(txtStaffName))
            addF(FormLabelO(lblStaffRole, "Role"))
            addF(WrapComboO(cboStaffRole))
            addF(FormLabelO(lblStaffPass, "Initial / reset password"))
            addF(WrapTBO(txtStaffPassword))

            Dim fieldsFiller As New Panel()
            fieldsFiller.Dock = DockStyle.Fill
            fieldsFiller.BackColor = Theme.White
            fieldsFiller.Margin = New Padding(0)
            addF(fieldsFiller)

            Dim actions As New TableLayoutPanel()
            actions.Dock = DockStyle.Fill
            actions.BackColor = Theme.White
            actions.ColumnCount = 1
            actions.RowCount = 6
            actions.Margin = New Padding(12, 0, 0, 0)
            For r As Integer = 0 To 4
                actions.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            actions.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            Dim addA As Action(Of Control) = Sub(c As Control)
                                                 actions.Controls.Add(c, 0, actions.Controls.Count)
                                             End Sub

            Dim lblActionsTitle As New Label()
            lblActionsTitle.Text = "Actions"
            lblActionsTitle.Font = Theme.FontCardTitle
            lblActionsTitle.ForeColor = Theme.Ink
            lblActionsTitle.AutoSize = True
            lblActionsTitle.Margin = New Padding(0, 0, 0, 4)
            addA(lblActionsTitle)

            btnStaffNew.Text = "Clear / New"
            Theme.StyleButton(btnStaffNew, ThemeButtonKind.Ghost)
            btnStaffNew.Dock = DockStyle.Fill
            btnStaffNew.Margin = New Padding(0, 0, 0, 8)
            btnStaffNew.Height = Theme.HeightButton
            addA(btnStaffNew)

            btnStaffSave.Text = "Save staff"
            Theme.StyleButton(btnStaffSave, ThemeButtonKind.Primary)
            btnStaffSave.Dock = DockStyle.Fill
            btnStaffSave.Margin = New Padding(0, 0, 0, 8)
            btnStaffSave.Height = Theme.HeightButton
            addA(btnStaffSave)

            btnStaffResetPass.Text = "Reset password"
            Theme.StyleButton(btnStaffResetPass, ThemeButtonKind.Secondary)
            btnStaffResetPass.Dock = DockStyle.Fill
            btnStaffResetPass.Margin = New Padding(0, 0, 0, 8)
            btnStaffResetPass.Height = Theme.HeightButton
            addA(btnStaffResetPass)

            btnStaffToggleActive.Text = "Deactivate / activate"
            Theme.StyleButton(btnStaffToggleActive, ThemeButtonKind.Destructive)
            btnStaffToggleActive.Dock = DockStyle.Fill
            btnStaffToggleActive.Margin = New Padding(0)
            btnStaffToggleActive.Height = Theme.HeightButton
            addA(btnStaffToggleActive)

            lblStaffMsg.Font = Theme.FontBody12
            lblStaffMsg.AutoSize = False
            lblStaffMsg.Dock = DockStyle.Fill
            lblStaffMsg.TextAlign = ContentAlignment.MiddleLeft
            lblStaffMsg.BackColor = Color.Transparent
            addA(lblStaffMsg)

            tlp.Controls.Add(fields, 0, 0)
            tlp.Controls.Add(actions, 1, 0)
            editCard.Controls.Add(tlp)

            tabStaff.Controls.Add(tableCard)
            tabStaff.Controls.Add(editCard)

            Dim heading As PageHeading = PageHeading.Build(tabStaff, "Staff accounts", _
                "Create logins for colleagues, adjust roles, and reset passwords.")
            heading.AddChip(New StatusChip("Access control", ThemeTone.Neutral))
        End Sub

        Private Sub UpdateStaffFooter(total As Integer)
            If _lblStaffFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblStaffFooter, total, total, If(total = 1, "account", "accounts"))
            If _lblStaffEmpty IsNot Nothing Then _lblStaffEmpty.Visible = (dgvStaff.RowCount = 0)
        End Sub

        ' ================= Products =================
        Private Sub RestyleProducts()
            tabProducts.BackColor = Theme.PageBg

            splitProducts.Visible = False
            tabProducts.Controls.Remove(splitProducts)
            pnlProdSearch.Visible = False
            splitProducts.Panel1.Controls.Remove(pnlProdSearch)

            Dim tableCard As New CardPanel()
            tableCard.Dock = DockStyle.Fill
            tableCard.Margin = New Padding(0, 0, 12, 0)
            tableCard.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvProducts)
            GridStyler.ApplyWeights(dgvProducts, 1.0, 2.2, 1.4, 1.0, 1.0, 0.7, 1.0)
            body.Controls.Add(dgvProducts)
            _lblProductsEmpty = GridStyler.AttachEmptyOverlay(dgvProducts, "No products match your search.")

            _lblProductsFooter = GridStyler.NewFooter()
            _lblProductsFooter.Height = 24
            _lblProductsFooter.Dock = DockStyle.Bottom
            _lblProductsFooter.BackColor = Theme.White

            Dim header As New Panel()
            header.Dock = DockStyle.Top
            header.Height = 30
            header.BackColor = Theme.White
            header.Margin = New Padding(0)

            lblProdSearch.Text = "Products"
            lblProdSearch.Font = Theme.FontSection
            lblProdSearch.ForeColor = Theme.Ink
            lblProdSearch.AutoSize = True
            lblProdSearch.BackColor = Theme.White
            lblProdSearch.Location = New Point(2, 4)
            header.Controls.Add(lblProdSearch)

            ' Category filter combo with an inside-left caption (combos have no
            ' native cue banner, so the caption labels what the filter does).
            Dim catCombo As New InputPanel()
            catCombo.Size = New Size(232, 26)
            catCombo.Margin = New Padding(0)
            catCombo.BackColor = Theme.White
            Theme.StyleCombo(cboProdCatFilter)
            Dim catCaption As New Label()
            catCaption.Text = "Category"
            catCaption.Font = Theme.FontSmall
            catCaption.ForeColor = Theme.Muted
            catCaption.AutoSize = True
            catCaption.BackColor = Theme.White
            catCaption.Margin = New Padding(0)
            catCombo.Controls.Add(catCaption)
            catCombo.Controls.Add(cboProdCatFilter)
            AddHandler catCombo.Resize,
                Sub(s As Object, e As EventArgs)
                    Dim comboX As Integer = 10 + catCaption.Width + 8
                    cboProdCatFilter.Location = New Point(comboX,
                                                          Math.Max(0, (catCombo.ClientSize.Height - cboProdCatFilter.Height) \ 2))
                    cboProdCatFilter.Width = Math.Max(40, catCombo.ClientSize.Width - comboX - 10)
                End Sub
            header.Controls.Add(catCombo)

            Dim searchWrap As New InputPanel()
            searchWrap.Size = New Size(240, 26)
            searchWrap.Margin = New Padding(0)
            searchWrap.BackColor = Theme.White
            Theme.StyleInput(txtProdSearch)
            Theme.SetCue(txtProdSearch, "Search products")
            searchWrap.Controls.Add(txtProdSearch)
            AddHandler searchWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    txtProdSearch.Location = New Point(10, Math.Max(0, (searchWrap.ClientSize.Height - txtProdSearch.Height) \ 2))
                    txtProdSearch.Width = searchWrap.ClientSize.Width - 20
                End Sub
            header.Controls.Add(searchWrap)

            ' Right-align the two pickers and keep search left of the combo
            ' (no overlap with the "Products" title at the smallest window).
            AddHandler header.Resize,
                Sub(s As Object, e As EventArgs)
                    Dim w As Integer = header.ClientSize.Width
                    catCombo.Location = New Point(w - catCombo.Width, 2)
                    Dim minX As Integer = lblProdSearch.Width + 48
                    Dim searchX As Integer = Math.Max(minX, catCombo.Left - 12 - 240)
                    searchWrap.Location = New Point(searchX, 2)
                    searchWrap.Width = Math.Min(240, Math.Max(120, catCombo.Left - 12 - searchX))
                End Sub

            Dim legend As New Label()
            legend.Dock = DockStyle.Bottom
            legend.Height = 22
            legend.Font = Theme.FontSmall
            legend.ForeColor = Theme.Warn
            legend.BackColor = Theme.White
            legend.TextAlign = ContentAlignment.MiddleLeft
            legend.Padding = New Padding(4, 0, 0, 0)
            legend.Text = "Orange rows = low stock (at or below the reorder level)."

            tableCard.Controls.Add(body)
            tableCard.Controls.Add(_lblProductsFooter)
            tableCard.Controls.Add(header)
            tableCard.Controls.Add(legend)

            Dim editorCard As New CardPanel()
            editorCard.Dock = DockStyle.Fill
            editorCard.Margin = New Padding(0)
            editorCard.Padding = New Padding(16, 12, 16, 12)
            editorCard.AutoScroll = True ' editor rows scroll when the window is short

            Dim tlp As New TableLayoutPanel()
            ' Dock=Top + AutoSize: the editor grows to its natural height inside
            ' the scrollable card instead of clipping bottom controls.
            tlp.Dock = DockStyle.Top
            tlp.AutoSize = True
            tlp.AutoSizeMode = AutoSizeMode.GrowOnly
            tlp.BackColor = Theme.White
            tlp.ColumnCount = 1
            tlp.RowCount = 17
            tlp.Margin = New Padding(0)
            tlp.Padding = New Padding(0)
            For r As Integer = 0 To 15
                tlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            Dim addRow As Action(Of Control) = Sub(c As Control)
                                                   tlp.Controls.Add(c, 0, tlp.Controls.Count)
                                               End Sub

            Dim lblTitle As New Label()
            lblTitle.Text = "Product editor"
            lblTitle.Font = Theme.FontCardTitle
            lblTitle.ForeColor = Theme.Ink
            lblTitle.AutoSize = True
            lblTitle.Margin = New Padding(0, 0, 0, 4)
            addRow(lblTitle)

            addRow(FormLabelO(lblProdCode, "Product code"))
            addRow(WrapTBO(txtProdCode, "e.g. R-ICE-001"))
            addRow(FormLabelO(lblProdName, "Product name"))
            addRow(WrapTBO(txtProdName))
            addRow(FormLabelO(lblProdCat, "Category"))
            addRow(WrapComboO(cboProdCategory))
            addRow(FormLabelO(lblProdSupplier, "Supplier"))
            addRow(WrapComboO(cboProdSupplier))

            Dim pcRow As New TableLayoutPanel()
            pcRow.Dock = DockStyle.Fill
            pcRow.Height = 52
            pcRow.Margin = New Padding(0)
            pcRow.BackColor = Theme.White
            pcRow.ColumnCount = 2
            pcRow.RowCount = 2
            pcRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
            pcRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
            pcRow.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            pcRow.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            lblProdPrice.Margin = New Padding(0, 10, 8, 0)
            lblProdCost.Margin = New Padding(8, 10, 0, 0)
            pcRow.Controls.Add(FormLabelO(lblProdPrice, lblProdPrice.Text), 0, 0)
            pcRow.Controls.Add(FormLabelO(lblProdCost, lblProdCost.Text), 1, 0)
            Dim pw As New InputPanel()
            pw.Dock = DockStyle.Fill
            pw.Margin = New Padding(0, 6, 8, 0)
            Theme.StyleInput(txtProdPrice)
            Theme.SetCue(txtProdPrice, "Selling price (PHP)")
            pw.Controls.Add(txtProdPrice)
            AddHandler pw.Resize,
                Sub(s As Object, e As EventArgs)
                    txtProdPrice.Location = New Point(12, Math.Max(0, (pw.ClientSize.Height - txtProdPrice.Height) \ 2))
                    txtProdPrice.Width = pw.ClientSize.Width - 24
                End Sub
            Dim cw As New InputPanel()
            cw.Dock = DockStyle.Fill
            cw.Margin = New Padding(8, 6, 0, 0)
            Theme.StyleInput(txtProdCost)
            Theme.SetCue(txtProdCost, "Cost (PHP)")
            cw.Controls.Add(txtProdCost)
            AddHandler cw.Resize,
                Sub(s As Object, e As EventArgs)
                    txtProdCost.Location = New Point(12, Math.Max(0, (cw.ClientSize.Height - txtProdCost.Height) \ 2))
                    txtProdCost.Width = cw.ClientSize.Width - 24
                End Sub
            pcRow.Controls.Add(pw, 0, 1)
            pcRow.Controls.Add(cw, 1, 1)
            addRow(pcRow)

            Dim sreRow As New TableLayoutPanel()
            sreRow.Dock = DockStyle.Fill
            sreRow.Height = 64
            sreRow.Margin = New Padding(0)
            sreRow.BackColor = Theme.White
            sreRow.ColumnCount = 3
            sreRow.RowCount = 2
            sreRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 34F))
            sreRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33F))
            sreRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33F))
            sreRow.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            sreRow.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            lblProdStock.Margin = New Padding(0, 10, 8, 0)
            lblProdReorder.Margin = New Padding(0, 10, 8, 0)
            lblProdExpiry.Margin = New Padding(0, 10, 0, 0)
            sreRow.Controls.Add(FormLabelO(lblProdStock, lblProdStock.Text), 0, 0)
            sreRow.Controls.Add(FormLabelO(lblProdReorder, lblProdReorder.Text), 1, 0)
            sreRow.Controls.Add(FormLabelO(lblProdExpiry, lblProdExpiry.Text), 2, 0)
            sreRow.Controls.Add(WrapNumO(numProdStock, 999), 0, 1)
            sreRow.Controls.Add(WrapNumO(numProdReorder, 999), 1, 1)
            dtpProdExpiry.Font = Theme.FontBody12
            sreRow.Controls.Add(dtpProdExpiry, 2, 1)
            addRow(sreRow)

            chkProdActive.Font = Theme.FontBody12
            chkProdActive.ForeColor = Theme.Ink
            chkProdActive.AutoSize = True
            chkProdActive.Margin = New Padding(0, 6, 0, 0)
            chkProdActive.BackColor = Theme.White
            addRow(chkProdActive)

            Dim editTitle As New Label()
            editTitle.Text = "Actions"
            editTitle.Font = Theme.FontCardTitle
            editTitle.ForeColor = Theme.Ink
            editTitle.AutoSize = True
            editTitle.Margin = New Padding(0, 8, 0, 0)
            addRow(editTitle)

            btnProdNew.Text = "Clear / New"
            Theme.StyleButton(btnProdNew, ThemeButtonKind.Ghost)
            btnProdNew.Dock = DockStyle.Fill
            btnProdNew.Margin = New Padding(0, 4, 0, 8)
            btnProdNew.Height = Theme.HeightButton
            addRow(btnProdNew)

            btnProdSave.Text = "Save product"
            Theme.StyleButton(btnProdSave, ThemeButtonKind.Primary)
            btnProdSave.Dock = DockStyle.Fill
            btnProdSave.Margin = New Padding(0, 0, 0, 8)
            btnProdSave.Height = Theme.HeightButton
            addRow(btnProdSave)

            btnProdDelete.Text = "Deactivate product"
            Theme.StyleButton(btnProdDelete, ThemeButtonKind.Destructive)
            btnProdDelete.Dock = DockStyle.Fill
            btnProdDelete.Height = Theme.HeightButton
            addRow(btnProdDelete)

            lblProdMsg.Font = Theme.FontBody12
            lblProdMsg.AutoSize = False
            lblProdMsg.Dock = DockStyle.Fill
            lblProdMsg.TextAlign = ContentAlignment.MiddleLeft
            lblProdMsg.BackColor = Color.Transparent
            addRow(lblProdMsg)

            Dim filler As New Panel()
            filler.Dock = DockStyle.Fill
            filler.BackColor = Theme.White
            filler.Margin = New Padding(0)
            addRow(filler)

            ' The partnered label+input rows get fixed heights so both lines
            ' inside them always show fully (price/cost pair and stock trio).
            tlp.RowStyles(tlp.GetRow(pcRow)) = New RowStyle(SizeType.Absolute, 82F)
            tlp.RowStyles(tlp.GetRow(sreRow)) = New RowStyle(SizeType.Absolute, 92F)
            tlp.RowStyles(tlp.GetRow(chkProdActive)) = New RowStyle(SizeType.Absolute, 36F)

            editorCard.Controls.Add(tlp)

            Dim columns As New TableLayoutPanel()
            columns.Dock = DockStyle.Fill
            columns.BackColor = Theme.PageBg
            columns.Margin = New Padding(0)
            columns.Padding = New Padding(0)
            columns.ColumnCount = 2
            columns.RowCount = 1
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55F))
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45F))
            columns.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            columns.Controls.Add(tableCard, 0, 0)
            columns.Controls.Add(editorCard, 1, 0)

            tabProducts.Controls.Add(columns)

            Dim heading As PageHeading = PageHeading.Build(tabProducts, "Products", _
                "Full catalog CRUD with pricing, stock and expiry controls.")
            heading.AddChip(New StatusChip("Live catalog", ThemeTone.Success))
        End Sub

        Private Sub UpdateProductsFooter(total As Integer)
            If _lblProductsFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblProductsFooter, total, total, If(total = 1, "product", "products"))
            If _lblProductsEmpty IsNot Nothing Then _lblProductsEmpty.Visible = (dgvProducts.RowCount = 0)
        End Sub

        ' ================= Categories & Suppliers =================
        Private Sub RestyleMasterData()
            tabMasterData.BackColor = Theme.PageBg

            splitMaster.Visible = False
            tabMasterData.Controls.Remove(splitMaster)
            pnlCatActions.Visible = False
            pnlSupActions.Visible = False
            grpCategories.Controls.Remove(pnlCatActions)
            grpSuppliers.Controls.Remove(pnlSupActions)
            grpCategories.Visible = False
            grpSuppliers.Visible = False
            tabMasterData.Controls.Remove(grpCategories)
            tabMasterData.Controls.Remove(grpSuppliers)

            Dim catCard As New CardPanel()
            catCard.Dock = DockStyle.Fill
            catCard.Margin = New Padding(0, 0, 12, 0)
            catCard.Padding = New Padding(14, 10, 14, 8)

            Dim catBody As New Panel()
            catBody.Dock = DockStyle.Fill
            catBody.BackColor = Theme.White
            catBody.Margin = New Padding(0)
            catBody.Padding = New Padding(0)

            GridStyler.Apply(dgvCategories)
            GridStyler.ApplyWeights(dgvCategories, 0.5, 3.0)
            catBody.Controls.Add(dgvCategories)
            _lblCatEmpty = GridStyler.AttachEmptyOverlay(dgvCategories, "No categories yet.")

            _lblCatFooter = GridStyler.NewFooter()
            _lblCatFooter.Height = 24
            _lblCatFooter.Dock = DockStyle.Bottom
            _lblCatFooter.BackColor = Theme.White

            Dim catHeader As New Panel()
            catHeader.Dock = DockStyle.Top
            catHeader.Height = 30
            catHeader.BackColor = Theme.White
            catHeader.Margin = New Padding(0)

            Dim lblCatTitle As New Label()
            lblCatTitle.Text = "Categories"
            lblCatTitle.Font = Theme.FontSection
            lblCatTitle.ForeColor = Theme.Ink
            lblCatTitle.AutoSize = True
            lblCatTitle.BackColor = Theme.White
            lblCatTitle.Location = New Point(2, 4)
            catHeader.Controls.Add(lblCatTitle)

            ' Category editor: caption above a full-width name box, New/Save/Delete
            ' beneath. btnCatDelete is the designer's own button (its Handles wiring
            ' keeps working); btnCatNew is runtime-added and just clears the form.
            btnCatSave.Text = "Save"
            Theme.StyleButton(btnCatSave, ThemeButtonKind.Primary)
            btnCatSave.AutoSize = False
            btnCatSave.Size = New Size(96, Theme.HeightButton)
            btnCatSave.Margin = New Padding(0, 0, 8, 0)

            btnCatDelete.Text = "Delete"
            Theme.StyleButton(btnCatDelete, ThemeButtonKind.Destructive)
            btnCatDelete.AutoSize = False
            btnCatDelete.Size = New Size(110, Theme.HeightButton)
            btnCatDelete.Margin = New Padding(0, 0, 0, 0)

            _btnCatNew = New Button()
            _btnCatNew.Name = "btnCatNew"
            _btnCatNew.Text = "New"
            _btnCatNew.AutoSize = False
            _btnCatNew.Size = New Size(92, Theme.HeightButton)
            _btnCatNew.Margin = New Padding(0, 0, 8, 0)
            Theme.StyleButton(_btnCatNew, ThemeButtonKind.Ghost)
            AddHandler _btnCatNew.Click, AddressOf btnCatNew_Click

            Dim catEdit As New TableLayoutPanel()
            catEdit.Dock = DockStyle.Bottom
            catEdit.Height = 116
            catEdit.Margin = New Padding(0, 10, 0, 0)
            catEdit.BackColor = Theme.White
            catEdit.ColumnCount = 2
            catEdit.RowCount = 3
            catEdit.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            catEdit.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            catEdit.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            catEdit.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            catEdit.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            lblCatName.Font = Theme.FontBody12
            lblCatName.ForeColor = Theme.Muted
            lblCatName.AutoSize = True
            lblCatName.Margin = New Padding(0, 0, 0, 6)
            lblCatName.BackColor = Theme.White

            txtCatName.Dock = DockStyle.Fill
            txtCatName.Margin = New Padding(0)

            Dim catButtons As New FlowLayoutPanel()
            catButtons.Dock = DockStyle.Fill
            catButtons.BackColor = Theme.White
            catButtons.Margin = New Padding(0, 6, 0, 0)
            catButtons.WrapContents = False
            catButtons.FlowDirection = FlowDirection.LeftToRight
            catButtons.Controls.Add(_btnCatNew)
            catButtons.Controls.Add(btnCatSave)
            catButtons.Controls.Add(btnCatDelete)

            catEdit.Controls.Add(lblCatName, 0, 0)
            catEdit.Controls.Add(txtCatName, 0, 1)
            catEdit.SetColumnSpan(txtCatName, 2)
            catEdit.Controls.Add(catButtons, 0, 2)
            catEdit.SetColumnSpan(catButtons, 2)

            catCard.Controls.Add(catBody)
            catCard.Controls.Add(_lblCatFooter)
            catCard.Controls.Add(catHeader)
            catCard.Controls.Add(catEdit)

            Dim supCard As New CardPanel()
            supCard.Dock = DockStyle.Fill
            supCard.Margin = New Padding(0)
            supCard.Padding = New Padding(14, 10, 14, 8)

            Dim supBody As New Panel()
            supBody.Dock = DockStyle.Fill
            supBody.BackColor = Theme.White
            supBody.Margin = New Padding(0)
            supBody.Padding = New Padding(0)

            GridStyler.Apply(dgvSuppliers)
            GridStyler.ApplyWeights(dgvSuppliers, 0.5, 2.0, 1.5, 2.4)
            supBody.Controls.Add(dgvSuppliers)
            _lblSupEmpty = GridStyler.AttachEmptyOverlay(dgvSuppliers, "No suppliers yet.")

            _lblSupFooter = GridStyler.NewFooter()
            _lblSupFooter.Height = 24
            _lblSupFooter.Dock = DockStyle.Bottom
            _lblSupFooter.BackColor = Theme.White

            Dim supHeader As New Panel()
            supHeader.Dock = DockStyle.Top
            supHeader.Height = 30
            supHeader.BackColor = Theme.White
            supHeader.Margin = New Padding(0)

            Dim lblSupTitle As New Label()
            lblSupTitle.Text = "Suppliers"
            lblSupTitle.Font = Theme.FontSection
            lblSupTitle.ForeColor = Theme.Ink
            lblSupTitle.AutoSize = True
            lblSupTitle.BackColor = Theme.White
            lblSupTitle.Location = New Point(2, 4)
            supHeader.Controls.Add(lblSupTitle)

            txtSupName.Dock = DockStyle.Fill
            txtSupName.Margin = New Padding(0)
            txtSupContact.Dock = DockStyle.Fill
            txtSupContact.Margin = New Padding(0)
            txtSupAddr.Dock = DockStyle.Fill
            txtSupAddr.Margin = New Padding(0)

            btnSupNew.Text = "New"
            Theme.StyleButton(btnSupNew, ThemeButtonKind.Ghost)
            btnSupNew.AutoSize = False
            btnSupNew.Size = New Size(92, Theme.HeightButton)
            btnSupNew.Dock = DockStyle.Fill
            btnSupNew.Margin = New Padding(8, 0, 0, 0)
            btnSupSave.Text = "Save"
            Theme.StyleButton(btnSupSave, ThemeButtonKind.Primary)
            btnSupSave.AutoSize = False
            btnSupSave.Size = New Size(92, Theme.HeightButton)
            btnSupSave.Dock = DockStyle.Fill
            btnSupSave.Margin = New Padding(8, 4, 0, 0)
            btnSupDelete.Text = "Delete"
            Theme.StyleButton(btnSupDelete, ThemeButtonKind.Destructive)
            btnSupDelete.AutoSize = False
            btnSupDelete.Size = New Size(92, Theme.HeightButton)
            btnSupDelete.Dock = DockStyle.Fill
            btnSupDelete.Margin = New Padding(8, 4, 0, 0)

            Dim supEdit As New TableLayoutPanel()
            supEdit.Dock = DockStyle.Bottom
            supEdit.Height = 118
            supEdit.Margin = New Padding(0)
            supEdit.BackColor = Theme.White
            supEdit.ColumnCount = 3
            supEdit.RowCount = 3
            supEdit.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            supEdit.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            supEdit.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 104F))
            For rr As Integer = 0 To 2
                supEdit.RowStyles.Add(New RowStyle(SizeType.Percent, 33.33F))
            Next
            lblSupName.Font = Theme.FontBody12
            lblSupName.ForeColor = Theme.Muted
            lblSupName.AutoSize = True
            lblSupName.Margin = New Padding(0, 0, 8, 0)
            lblSupName.BackColor = Theme.White
            lblSupContact.Font = Theme.FontBody12
            lblSupContact.ForeColor = Theme.Muted
            lblSupContact.AutoSize = True
            lblSupContact.Margin = New Padding(0, 0, 8, 0)
            lblSupContact.BackColor = Theme.White
            lblSupAddr.Font = Theme.FontBody12
            lblSupAddr.ForeColor = Theme.Muted
            lblSupAddr.AutoSize = True
            lblSupAddr.Margin = New Padding(0, 0, 8, 0)
            lblSupAddr.BackColor = Theme.White
            supEdit.Controls.Add(lblSupName, 0, 0)
            supEdit.Controls.Add(txtSupName, 1, 0)
            supEdit.Controls.Add(FormLabelO(lblSupContact, lblSupContact.Text), 0, 1)
            supEdit.Controls.Add(txtSupContact, 1, 1)
            supEdit.Controls.Add(FormLabelO(lblSupAddr, lblSupAddr.Text), 0, 2)
            supEdit.Controls.Add(txtSupAddr, 1, 2)
            supEdit.Controls.Add(btnSupNew, 2, 0)
            supEdit.Controls.Add(btnSupSave, 2, 1)
            supEdit.Controls.Add(btnSupDelete, 2, 2)

            supCard.Controls.Add(supBody)
            supCard.Controls.Add(_lblSupFooter)
            supCard.Controls.Add(supHeader)
            supCard.Controls.Add(supEdit)

            Dim columns As New TableLayoutPanel()
            columns.Dock = DockStyle.Fill
            columns.BackColor = Theme.PageBg
            columns.Margin = New Padding(0)
            columns.Padding = New Padding(0)
            columns.ColumnCount = 2
            columns.RowCount = 1
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55F))
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45F))
            columns.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            columns.Controls.Add(catCard, 0, 0)
            columns.Controls.Add(supCard, 1, 0)

            tabMasterData.Controls.Add(columns)

            Dim heading As PageHeading = PageHeading.Build(tabMasterData, "Categories & suppliers", _
                "Master data feeding the catalog and stock-in flows.")
            heading.AddChip(New StatusChip("Reference data", ThemeTone.Neutral))
        End Sub

        Private Sub UpdateCategoriesFooter(total As Integer)
            If _lblCatFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblCatFooter, total, total, If(total = 1, "category", "categories"))
            If _lblCatEmpty IsNot Nothing Then _lblCatEmpty.Visible = (dgvCategories.RowCount = 0)
        End Sub

        Private Sub UpdateSuppliersFooter(total As Integer)
            If _lblSupFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblSupFooter, total, total, If(total = 1, "supplier", "suppliers"))
            If _lblSupEmpty IsNot Nothing Then _lblSupEmpty.Visible = (dgvSuppliers.RowCount = 0)
        End Sub

        ' ================= Transactions =================
        Private Sub RestyleTransactions()
            tabTransactions.BackColor = Theme.PageBg

            pnlTxFilter.Visible = False
            tabTransactions.Controls.Remove(pnlTxFilter)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvTransactions)
            GridStyler.ApplyWeights(dgvTransactions, 1.2, 1.4, 1.1, 1.3, 1.0, 1.0, 1.1)
            body.Controls.Add(dgvTransactions)
            _lblTxEmpty = GridStyler.AttachEmptyOverlay(dgvTransactions, "No transactions in this date range.")

            _lblTxFooter = GridStyler.NewFooter()
            _lblTxFooter.Height = 24
            _lblTxFooter.Dock = DockStyle.Bottom
            _lblTxFooter.BackColor = Theme.White

            card.Controls.Add(body)
            card.Controls.Add(_lblTxFooter)

            Dim strip As New Panel()
            strip.Dock = DockStyle.Top
            strip.Height = 46
            strip.BackColor = Theme.PageBg
            strip.Margin = New Padding(0)

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Fill
            tlp.BackColor = Theme.PageBg
            tlp.Margin = New Padding(0)
            tlp.ColumnCount = 4
            tlp.RowCount = 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 190F))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize)) ' date pickers keep natural size
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            lblTxStart.Font = Theme.FontSmall
            lblTxStart.ForeColor = Theme.Muted
            lblTxStart.AutoSize = True
            lblTxStart.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblTxStart, 0, 0)

            dtpTxStart.Font = Theme.FontBody12
            dtpTxStart.Margin = New Padding(0, 6, 12, 6)
            tlp.Controls.Add(dtpTxStart, 1, 0)

            lblTxEnd.Font = Theme.FontSmall
            lblTxEnd.ForeColor = Theme.Muted
            lblTxEnd.AutoSize = True
            lblTxEnd.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblTxEnd, 2, 0)

            dtpTxEnd.Font = Theme.FontBody12
            dtpTxEnd.Margin = New Padding(0, 6, 0, 6)
            tlp.Controls.Add(dtpTxEnd, 3, 0)
            strip.Controls.Add(tlp)

            tabTransactions.Controls.Add(card)
            tabTransactions.Controls.Add(strip)

            Dim heading As PageHeading = PageHeading.Build(tabTransactions, "Transaction history", _
                "Auditable receipts across the selected date window.")
            heading.AddChip(New StatusChip("Records", ThemeTone.Neutral))

            btnTxViewDetails.Text = "View receipt"
            Theme.StyleButton(btnTxViewDetails, ThemeButtonKind.Secondary)
            btnTxViewDetails.AutoSize = True
            heading.AddAction(btnTxViewDetails)
        End Sub

        Private Sub UpdateTransactionsFooter(total As Integer)
            If _lblTxFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblTxFooter, total, total, If(total = 1, "transaction", "transactions"))
            If _lblTxEmpty IsNot Nothing Then _lblTxEmpty.Visible = (dgvTransactions.RowCount = 0)
        End Sub

        ' ================= Reports =================
        Private Sub RestyleReports()
            tabReports.BackColor = Theme.PageBg

            pnlReportHeader.Visible = False
            pnlReportSummary.Visible = False
            tabReports.Controls.Remove(pnlReportHeader)
            tabReports.Controls.Remove(pnlReportSummary)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvReport)
            dgvReport.Dock = DockStyle.Fill ' preview renders in the whole card
            body.Controls.Add(dgvReport)
            _lblReportEmpty = GridStyler.AttachEmptyOverlay(dgvReport, "Generate a report to see results here.")

            card.Controls.Add(body)

            lblReportSummary.Text = "Report ready."
            lblReportSummary.Font = Theme.FontBody12
            lblReportSummary.ForeColor = Theme.Info
            lblReportSummary.BackColor = Theme.InfoTint
            lblReportSummary.AutoSize = False
            lblReportSummary.Height = 34
            lblReportSummary.Dock = DockStyle.Top
            lblReportSummary.TextAlign = ContentAlignment.MiddleLeft
            lblReportSummary.Padding = New Padding(16, 0, 0, 0)
            lblReportSummary.Margin = New Padding(0, 0, 0, 12)

            Dim strip As New Panel()
            strip.Dock = DockStyle.Top
            strip.Height = 46
            strip.BackColor = Theme.PageBg
            strip.Margin = New Padding(0)

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Fill
            tlp.BackColor = Theme.PageBg
            tlp.Margin = New Padding(0)
            tlp.ColumnCount = 4
            tlp.RowCount = 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 52F))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 48F))
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            lblReportType.Font = Theme.FontSmall
            lblReportType.ForeColor = Theme.Muted
            lblReportType.AutoSize = True
            lblReportType.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblReportType, 0, 0)

            Dim typeWrap As New InputPanel()
            typeWrap.Dock = DockStyle.Fill
            typeWrap.Margin = New Padding(0, 6, 12, 6)
            Theme.StyleCombo(cboReportType)
            typeWrap.Controls.Add(cboReportType)
            AddHandler typeWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    cboReportType.Location = New Point(10, Math.Max(0, (typeWrap.ClientSize.Height - cboReportType.Height) \ 2))
                    cboReportType.Width = Math.Max(60, typeWrap.ClientSize.Width - 20)
                End Sub
            tlp.Controls.Add(typeWrap, 1, 0)

            lblReportDate.Font = Theme.FontSmall
            lblReportDate.ForeColor = Theme.Muted
            lblReportDate.AutoSize = True
            lblReportDate.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblReportDate, 2, 0)

            Dim datePanel As New Panel()
            datePanel.Dock = DockStyle.Fill
            datePanel.BackColor = Theme.PageBg
            datePanel.Margin = New Padding(0)
            dtpReportDate.Font = Theme.FontBody12
            dtpReportDate.Width = 190
            dtpReportDate.Location = New Point(0, 6)
            datePanel.Controls.Add(dtpReportDate)
            tlp.Controls.Add(datePanel, 3, 0)
            strip.Controls.Add(tlp)

            tabReports.Controls.Add(card)
            tabReports.Controls.Add(lblReportSummary)
            tabReports.Controls.Add(strip)

            Dim heading As PageHeading = PageHeading.Build(tabReports, "Reports & analytics", _
                "Generate, export and print operational reports.")
            heading.AddChip(New StatusChip("Business data", ThemeTone.Info))

            btnGenerateReport.Text = "Generate"
            Theme.StyleButton(btnGenerateReport, ThemeButtonKind.Primary)
            btnGenerateReport.AutoSize = True
            heading.AddAction(btnGenerateReport)

            btnExportCsv.Text = "Export CSV"
            Theme.StyleButton(btnExportCsv, ThemeButtonKind.Secondary)
            btnExportCsv.AutoSize = True
            heading.AddAction(btnExportCsv)

            btnPrintReport.Text = "Print"
            Theme.StyleButton(btnPrintReport, ThemeButtonKind.Secondary)
            btnPrintReport.AutoSize = True
            heading.AddAction(btnPrintReport)

            AddHandler dgvReport.DataBindingComplete,
                Sub(s As Object, e As DataGridViewBindingCompleteEventArgs)
                    GridStyler.ApplyWeights(dgvReport, 1.6, 2.0, 1.6, 2.2, 2.0, 2.0)
                    If _lblReportEmpty IsNot Nothing Then
                        _lblReportEmpty.Visible = (dgvReport.RowCount = 0)
                        If _lblReportEmpty.Visible Then
                            _lblReportEmpty.Left = Math.Max(0, (dgvReport.ClientSize.Width - _lblReportEmpty.Width) \ 2)
                            _lblReportEmpty.Top = Math.Max(0, (dgvReport.ClientSize.Height - _lblReportEmpty.Height) \ 2)
                            _lblReportEmpty.BringToFront()
                        End If
                    End If
                End Sub
        End Sub

        ' ================= Nutrition catalog tab (new, placeholder-backed) =================
        Private Sub BuildNutritionTab()
            _tabNutrition.BackColor = Theme.PageBg

            Dim importCard As New CardPanel()
            importCard.Dock = DockStyle.Top
            importCard.Height = 148
            importCard.Margin = New Padding(0, 0, 0, 12)
            importCard.Padding = New Padding(16, 12, 16, 12)

            Dim itlp As New TableLayoutPanel()
            itlp.Dock = DockStyle.Fill
            itlp.BackColor = Theme.White
            itlp.ColumnCount = 1
            itlp.RowCount = 5
            itlp.Margin = New Padding(0)
            For r As Integer = 0 To 3
                itlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            itlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            Dim addI As Action(Of Control) = Sub(c As Control)
                                                 itlp.Controls.Add(c, 0, itlp.Controls.Count)
                                             End Sub

            Dim lblImpTitle As New Label()
            lblImpTitle.Text = "Import status · Open Food Facts"
            lblImpTitle.Font = Theme.FontCardTitle
            lblImpTitle.ForeColor = Theme.Ink
            lblImpTitle.AutoSize = True
            lblImpTitle.Margin = New Padding(0, 0, 0, 8)
            lblImpTitle.BackColor = Color.Transparent
            addI(lblImpTitle)

            _nutritionStateLabel = New Label()
            _nutritionStateLabel.Text = "Not linked"
            _nutritionStateLabel.Font = Theme.FontBody12
            _nutritionStateLabel.ForeColor = Theme.Muted
            _nutritionStateLabel.AutoSize = True
            _nutritionStateLabel.Margin = New Padding(0, 0, 0, 8)
            _nutritionStateLabel.BackColor = Color.Transparent
            addI(_nutritionStateLabel)

            _nutritionRail = New ProgressRail()
            _nutritionRail.Dock = DockStyle.Fill
            _nutritionRail.Height = 7
            _nutritionRail.Margin = New Padding(0, 0, 0, 8)
            addI(_nutritionRail)

            Dim counters As New FlowLayoutPanel()
            counters.Dock = DockStyle.Fill
            counters.FlowDirection = FlowDirection.LeftToRight
            counters.WrapContents = False
            counters.BackColor = Theme.White
            counters.Margin = New Padding(0)

            _nutritionProcessedLabel = New Label()
            _nutritionProcessedLabel.Text = "0 records processed"
            _nutritionProcessedLabel.Font = Theme.FontBody12
            _nutritionProcessedLabel.ForeColor = Theme.Muted
            _nutritionProcessedLabel.AutoSize = True
            _nutritionProcessedLabel.Margin = New Padding(0, 6, 24, 0)

            _nutritionValidLabel = New Label()
            _nutritionValidLabel.Text = "0 verified"
            _nutritionValidLabel.Font = Theme.FontBody12
            _nutritionValidLabel.ForeColor = Theme.Green
            _nutritionValidLabel.AutoSize = True
            _nutritionValidLabel.Margin = New Padding(0, 6, 24, 0)

            _nutritionReviewLabel = New Label()
            _nutritionReviewLabel.Text = "0 need review"
            _nutritionReviewLabel.Font = Theme.FontBody12
            _nutritionReviewLabel.ForeColor = Theme.Warn
            _nutritionReviewLabel.AutoSize = True
            _nutritionReviewLabel.Margin = New Padding(0, 6, 0, 0)

            counters.Controls.Add(_nutritionProcessedLabel)
            counters.Controls.Add(_nutritionValidLabel)
            counters.Controls.Add(_nutritionReviewLabel)
            addI(counters)

            Dim filler As New Panel()
            filler.Dock = DockStyle.Fill
            filler.BackColor = Theme.White
            filler.Margin = New Padding(0)
            addI(filler)

            importCard.Controls.Add(itlp)

            Dim tableCard As New CardPanel()
            tableCard.Dock = DockStyle.Fill
            tableCard.Margin = New Padding(0)
            tableCard.Padding = New Padding(14, 10, 14, 8)

            _nutritionNotice = New InlineNotice()
            _nutritionNotice.Dock = DockStyle.Top
            _nutritionNotice.Height = 34
            _nutritionNotice.Margin = New Padding(0, 0, 0, 10)
            _nutritionNotice.SetNotice("No nutrition data yet — run the import to fill this catalog.", ThemeTone.Info)

            _nutritionTable = New NutritionTableControl()
            _nutritionTable.Dock = DockStyle.Fill
            _nutritionTable.Margin = New Padding(0)

            tableCard.Controls.Add(_nutritionTable)
            tableCard.Controls.Add(_nutritionNotice)

            _tabNutrition.Controls.Add(tableCard)
            _tabNutrition.Controls.Add(importCard)

            Dim heading As PageHeading = PageHeading.Build(_tabNutrition, "Nutrition catalog", _
                "Running Open Food Facts sample-data imports to power in-app nutrition values.")
            heading.AddChip(New StatusChip("Open Food Facts", ThemeTone.Info))

            _btnNutritionDetails = New Button()
            _btnNutritionDetails.Text = "View details"
            Theme.StyleButton(_btnNutritionDetails, ThemeButtonKind.Secondary)
            _btnNutritionDetails.AutoSize = True
            AddHandler _btnNutritionDetails.Click, AddressOf OnNutritionDetailsClick
            heading.AddAction(_btnNutritionDetails)

            _btnNutritionImport = New Button()
            ' QA C4: remove developer-facing text ("Run import (demo)").
            _btnNutritionImport.Text = "Import sample data"
            Theme.StyleButton(_btnNutritionImport, ThemeButtonKind.Secondary)
            _btnNutritionImport.AutoSize = True
            AddHandler _btnNutritionImport.Click, AddressOf OnNutritionImportClick
            heading.AddAction(_btnNutritionImport)

            AddHandler _nutritionTable.DetailsRequested, Sub(sender As Object, e As EventArgs)
                                                             OnNutritionDetailsClick(Me, EventArgs.Empty)
                                                         End Sub
        End Sub

        ''' <summary>
        ''' QA C1: the status card, banner, rail and counters all read
        ''' NutritionStore.GetStatus() — the same source of truth the grid and the
        ''' Cashier preview use. ROOT CAUSE of the self-contradicting screen: this
        ''' card counted OffImportState progress (a per-session accumulator that
        ''' stays zero until an import runs) while the grid silently seeded the
        ''' store, so it reported "0 records" next to a 25-row grid.
        ''' </summary>
        Private Sub RefreshNutritionImportCard()
            If _nutritionStateLabel Is Nothing Then Return
            Dim st As NutritionStatus = NutritionStore.GetStatus()
            Dim p As OffImportProgress = OffImportState.Current.Progress

            If st.IsRunning Then
                _nutritionStateLabel.Text = "Import running…"
                _nutritionStateLabel.ForeColor = Theme.Muted
                _nutritionRail.Value = If(p.RecordsTotal = 0, 8.0,
                                          Math.Min(96.0, p.RecordsProcessed * 100.0 / p.RecordsTotal))
                _nutritionNotice.Visible = False
                _nutritionProcessedLabel.Text = $"{p.RecordsProcessed} records processed"
                _nutritionValidLabel.Text = $"{p.RecordsValid} verified"
                _nutritionReviewLabel.Text = $"{p.RecordsNeedReview} need review"
            ElseIf st.IsImported Then
                ' Real counts from the store; the "no data" banner hides (QA C1).
                _nutritionStateLabel.ForeColor = Theme.Green
                _nutritionStateLabel.Text = If(st.LastImportAt > DateTime.MinValue,
                                               $"Sample data ready · imported {st.LastImportAt:HH:mm}",
                                               "Sample data ready")
                _nutritionRail.Value = 100.0
                _nutritionNotice.Visible = False
                _nutritionProcessedLabel.Text = $"{st.RecordsVerified + st.RecordsNeedReview} records processed"
                _nutritionValidLabel.Text = $"{st.RecordsVerified} verified"
                _nutritionReviewLabel.Text = $"{st.RecordsNeedReview} need review"
            Else
                _nutritionStateLabel.ForeColor = Theme.Muted
                _nutritionStateLabel.Text = "Not imported yet"
                _nutritionRail.Value = 0.0
                _nutritionNotice.Visible = True
                _nutritionProcessedLabel.Text = "0 records processed"
                _nutritionValidLabel.Text = "0 verified"
                _nutritionReviewLabel.Text = "0 need review"
            End If
        End Sub

        Private Sub OnNutritionImportClick(sender As Object, e As EventArgs)
            If OffImportState.Current.IsRunning Then Return
            Dim progress As New Progress(Of OffImportProgress)(
                Sub(p)
                    RefreshNutritionImportCard()
                    If p.HasFinished OrElse p.HasFailed Then
                        _nutritionTable.LoadOwnerCatalog(_productRepo.GetAll(activeOnly:=True))
                        _nutritionTable.PerformLayout()
                    End If
                End Sub)
            Services.NutritionStore.RunImportAsync(progress, System.Threading.CancellationToken.None)
            RefreshNutritionImportCard()
        End Sub

        Private Sub OnNutritionDetailsClick(sender As Object, e As EventArgs)
            Dim code As String = _nutritionTable.SelectedCode
            If String.IsNullOrEmpty(code) Then
                MessageBox.Show("Select a nutrition record first (double-click a row or use this button).", "Nutrition catalog",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Dim rec As NutritionRecord = Services.NutritionStore.TryGetForCode(code)
            If rec Is Nothing Then
                MessageBox.Show("No nutrition data is linked to this product yet. The catalog fills once an Open Food Facts import is connected.",
                                "Nutrition catalog", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Using dlg As New NutritionDetailView(rec)
                dlg.ShowDialog(Me)
            End Using
        End Sub

#End Region
    End Class
End Namespace
