' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Class: ValidationRules
' OOP Concept: ENCAPSULATION & SINGLE SOURCE OF TRUTH
' Centralizes all account credential business rules so the Login, Registration,
' Staff Management, and Profile forms enforce exactly the same constraints.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Text.RegularExpressions

Namespace Services
    Public NotInheritable Class ValidationRules
        ' Fixed limits used across all forms
        Public Const MinUsernameLength As Integer = 3
        Public Const MaxUsernameLength As Integer = 20
        Public Const MinPasswordLength As Integer = 6

        ' Currency bounds (PHP)
        Public Const MaxMoney As Decimal = 999999.99D
        Public Const MaxQuantity As Integer = 999

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Username: 3-20 chars, letters/digits/dot/underscore only, no spaces.
        ''' </summary>
        Public Shared Function IsValidUsername(username As String) As Boolean
            If String.IsNullOrWhiteSpace(username) Then Return False
            If username.Trim().Length < MinUsernameLength OrElse username.Trim().Length > MaxUsernameLength Then Return False
            Return Regex.IsMatch(username.Trim(), "^[A-Za-z0-9._]+$")
        End Function

        ''' <summary>
        ''' Human full name: must contain at least one letter and no digits/symbols.
        ''' Allowed: letters, spaces, hyphen, apostrophe, dot, comma.
        ''' </summary>
        Public Shared Function IsValidFullName(fullName As String) As Boolean
            If String.IsNullOrWhiteSpace(fullName) Then Return False
            If Not Regex.IsMatch(fullName.Trim(), "[A-Za-z]") Then Return False
            Return Not Regex.IsMatch(fullName.Trim(), "[0-9@#%&*!+=?/\[\]{}|~<>:;`""^\\]")
        End Function

        ''' <summary>
        ''' Password: minimum length, no whitespace anywhere.
        ''' </summary>
        Public Shared Function IsValidPassword(password As String) As Boolean
            If String.IsNullOrEmpty(password) Then Return False
            If password.Length < MinPasswordLength Then Return False
            Return Not Regex.IsMatch(password, "\s")
        End Function

        ''' <summary>
        ''' Money amount: non-negative, at most 2 decimal places, within max cap.
        ''' </summary>
        Public Shared Function IsValidMoney(amount As Decimal) As Boolean
            If amount < 0D OrElse amount > MaxMoney Then Return False
            Return (amount * 100D) = Math.Floor(amount * 100D)
        End Function

        ''' <summary>
        ''' Supplier contact number: digits only, 7-15 characters.
        ''' </summary>
        Public Shared Function IsValidContactNumber(contact As String) As Boolean
            If String.IsNullOrWhiteSpace(contact) Then Return False
            Return Regex.IsMatch(contact.Trim(), "^[0-9+\-]{7,15}$")
        End Function
    End Class
End Namespace
