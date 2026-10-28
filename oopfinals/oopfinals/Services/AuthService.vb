' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: AuthService
' OOP Concept: ENCAPSULATION & SECURITY
' Encapsulates user authentication, cryptographic PBKDF2 password hashing,
' salting, and registration workflows.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Security.Cryptography
Imports oopfinals.Data
Imports oopfinals.Models

Namespace Services
    Public NotInheritable Class AuthService
        Private Const SaltSize As Integer = 16
        Private Const HashSize As Integer = 32
        Private Const Iterations As Integer = 10000

        Private Sub New()
            ' Prevent instantiation of utility class
        End Sub

        ''' <summary>
        ''' Generates a cryptographically secure random salt encoded in Base64.
        ''' </summary>
        Public Shared Function GenerateSalt() As String
            Dim saltBytes(SaltSize - 1) As Byte
            Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
                rng.GetBytes(saltBytes)
            End Using
            Return Convert.ToBase64String(saltBytes)
        End Function

        ''' <summary>
        ''' Computes a salted PBKDF2 hash of the given plain-text password using SHA-256 equivalent iterations.
        ''' </summary>
        Public Shared Function HashPassword(password As String, saltBase64 As String) As String
            If String.IsNullOrEmpty(password) Then
                Throw New ArgumentException("Password cannot be empty.")
            End If

            Dim saltBytes As Byte() = Convert.FromBase64String(saltBase64)
            Using pbkdf2 As New Rfc2898DeriveBytes(password, saltBytes, Iterations)
                Dim hashBytes As Byte() = pbkdf2.GetBytes(HashSize)
                Return Convert.ToBase64String(hashBytes)
            End Using
        End Function

        ''' <summary>
        ''' Validates plain password against stored salt and expected hash in constant time.
        ''' </summary>
        Public Shared Function VerifyPassword(password As String, saltBase64 As String, expectedHashBase64 As String) As Boolean
            If String.IsNullOrEmpty(password) OrElse String.IsNullOrEmpty(saltBase64) OrElse String.IsNullOrEmpty(expectedHashBase64) Then
                Return False
            End If

            Try
                Dim actualHash As String = HashPassword(password, saltBase64)
                Return SlowEquals(actualHash, expectedHashBase64)
            Catch
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Constant-time string comparison to prevent timing attacks.
        ''' </summary>
        Private Shared Function SlowEquals(a As String, b As String) As Boolean
            Dim diff As UInteger = CUInt(a.Length) Xor CUInt(b.Length)
            For i As Integer = 0 To Math.Min(a.Length, b.Length) - 1
                diff = diff Or (CUInt(AscW(a(i))) Xor CUInt(AscW(b(i))))
            Next
            Return diff = 0
        End Function

        ''' <summary>
        ''' Authenticates credentials against the database.
        ''' Returns the polymorphic User instance (StoreOwner, Cashier, InventoryClerk, or Customer) if valid.
        ''' </summary>
        Public Shared Function Authenticate(username As String, plainPassword As String) As User
            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(plainPassword) Then
                Return Nothing
            End If

            Dim userRepo As New UserRepository()
            Dim user As User = userRepo.GetByUsername(username.Trim().ToLowerInvariant())

            If user Is Nothing Then
                Return Nothing
            End If

            If Not user.IsActive Then
                Throw New InvalidOperationException("This account has been deactivated. Please contact the Store Owner.")
            End If

            If VerifyPassword(plainPassword, user.Salt, user.PasswordHash) Then
                Return user
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Registers a new Customer account with salted PBKDF2 hash.
        ''' </summary>
        Public Shared Function RegisterCustomer(username As String, plainPassword As String, fullName As String) As Boolean
            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(plainPassword) OrElse String.IsNullOrWhiteSpace(fullName) Then
                Throw New ArgumentException("All fields are required.")
            End If

            If Not ValidationRules.IsValidUsername(username) Then
                Throw New ArgumentException("Username must be 3-20 characters and may only contain letters, numbers, dots and underscores.")
            End If

            If Not ValidationRules.IsValidPassword(plainPassword) Then
                Throw New ArgumentException("Password must be at least 6 characters long and contain no spaces.")
            End If

            If Not ValidationRules.IsValidFullName(fullName) Then
                Throw New ArgumentException("Full name must contain only letters (no numbers or symbols).")
            End If

            Dim userRepo As New UserRepository()
            If userRepo.UsernameExists(username) Then
                Throw New InvalidOperationException("Username is already taken. Please choose another one.")
            End If

            Dim salt As String = GenerateSalt()
            Dim hash As String = HashPassword(plainPassword, salt)

            Dim newCust As New Customer() With {
                .Username = username.Trim().ToLowerInvariant(),
                .FullName = fullName.Trim(),
                .PasswordHash = hash,
                .Salt = salt,
                .IsActive = True,
                .CreatedAt = DateTime.Now
            }

            Return userRepo.CreateUser(newCust) > 0
        End Function
    End Class
End Namespace
