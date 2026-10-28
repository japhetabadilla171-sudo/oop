' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: CustomerDashboardForm (Code-Behind)
' OOP Concept: POLYMORPHISM & ENCAPSULATION
' Customer interface allowing product catalog browsing, real-time availability
' status viewing, personal purchase history auditing, and profile updates.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports oopfinals.Data
Imports oopfinals.Models
Imports oopfinals.Services
Imports oopfinals.Controls

Namespace Forms
    Public Class CustomerDashboardForm
        Private ReadOnly _customer As Customer
        Private ReadOnly _productRepo As New ProductRepository()
        Private ReadOnly _categoryRepo As New CategoryRepository()
        Private ReadOnly _saleRepo As New SaleRepository()
        Private ReadOnly _userRepo As New UserRepository()

        Public Sub New(customer As Customer)
            InitializeComponent()
            _customer = customer
            Me.DoubleBuffered = True
            InitializeUiRestyle()
        End Sub

        Private Sub CustomerDashboardForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            Me.Refresh()
        End Sub

        Private Sub CustomerDashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' SECURITY: verify role on form load (not just navigation/button-hiding)
            If _customer.Role <> "Customer" Then
                MessageBox.Show("Access denied: this dashboard is restricted to Customer accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TryCast(Me, Form).BeginInvoke(New MethodInvoker(Sub() Me.Close()))
                Return
            End If

            ' Step 7 (Figma): "Role: Customer · <Full name> (@username)" — role once.
            lblUserInfo.Text = $"Role: {_customer.GetRoleName()} · {_customer.GetDisplayName()} (@{_customer.Username})"
            lblProfUsernameVal.Text = _customer.Username
            txtProfFullName.Text = _customer.FullName
            AppHeader.Apply(pnlHeader, lblAppTitle, lblUserInfo, btnLogout) ' RESTYLE: reposition after label text set here

            LoadCategories()
            LoadProducts()
            LoadHistory()
        End Sub

        Private Sub LoadCategories()
            cboCategory.Items.Clear()
            cboCategory.Items.Add("All Categories")
            Dim cats As List(Of Category) = _categoryRepo.GetAll()
            For Each c In cats
                cboCategory.Items.Add(c)
            Next
            cboCategory.SelectedIndex = 0
        End Sub

        Private Sub LoadProducts()
            dgvProducts.Rows.Clear()
            Dim kw As String = txtSearch.Text.Trim()
            Dim catId As Nullable(Of Integer) = Nothing

            If cboCategory.SelectedIndex > 0 Then
                Dim selectedCat As Category = TryCast(cboCategory.SelectedItem, Category)
                If selectedCat IsNot Nothing Then
                    catId = selectedCat.CategoryId
                End If
            End If

            Dim list As List(Of Product) = _productRepo.Search(kw, catId, activeOnly:=True)
            For Each p In list
                Dim rowIdx As Integer = dgvProducts.Rows.Add(p.ProductCode, p.Name, p.CategoryName, p.Price.ToString("N2"), p.GetStockStatus())
                Dim status As String = p.GetStockStatus()
                If status = "Out of Stock" Then
                    dgvProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.Red
                ElseIf status = "Low Stock" Then
                    dgvProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.DarkOrange
                Else
                    dgvProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.DarkGreen
                End If
            Next

            ' RESTYLE (visual only): card gallery + footer/empty state use the same result set
            _lastProductCount = list.Count
            UpdateCardGallery(list)
        End Sub

        Private Sub LoadHistory()
            dgvHistory.Rows.Clear()
            Dim sales As List(Of Sale) = _saleRepo.GetSalesByCustomer(_customer.UserId)
            For Each s In sales
                dgvHistory.Rows.Add(s.ReceiptNo, s.SaleDate.ToString("yyyy-MM-dd hh:mm tt"), s.CashierName, s.Subtotal.ToString("N2"), s.DiscountAmount.ToString("N2"), s.Total.ToString("N2"))
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateHistoryFooter(sales.Count)
        End Sub

        Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
            LoadProducts()
        End Sub

        Private Sub cboCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedIndexChanged
            LoadProducts()
        End Sub

        Private Sub btnRefreshHistory_Click(sender As Object, e As EventArgs) Handles btnRefreshHistory.Click
            LoadHistory()
        End Sub

        Private Sub btnViewReceiptDetails_Click(sender As Object, e As EventArgs) Handles btnViewReceiptDetails.Click
            If dgvHistory.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a transaction from the history table to view details.", "Select Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim receiptNo As String = Convert.ToString(dgvHistory.SelectedRows(0).Cells(0).Value)
            Dim sale As Sale = _saleRepo.GetByReceiptNo(receiptNo)
            If sale IsNot Nothing Then
                Using dlg As New PastReceiptForm(sale)
                    dlg.ShowDialog(Me)
                End Using
            End If
        End Sub

        Private Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
            lblProfileMsg.Text = String.Empty
            Dim newName As String = txtProfFullName.Text.Trim()
            If String.IsNullOrWhiteSpace(newName) Then
                lblProfileMsg.Text = "Full name cannot be empty."
                lblProfileMsg.ForeColor = Color.Red
                Return
            End If

            If Not ValidationRules.IsValidFullName(newName) Then
                lblProfileMsg.Text = "Full name may only contain letters, spaces, hyphens, apostrophes, dots and commas."
                lblProfileMsg.ForeColor = Color.Red
                Return
            End If

            Try
                _customer.FullName = newName
                If _userRepo.UpdateUser(_customer) Then
                    lblUserInfo.Text = $"Role: {_customer.GetRoleName()} · {_customer.GetDisplayName()} (@{_customer.Username})"
                    lblProfileMsg.Text = "Profile updated successfully!"
                    lblProfileMsg.ForeColor = Color.DarkGreen
                Else
                    lblProfileMsg.Text = "Failed to update profile."
                    lblProfileMsg.ForeColor = Color.Red
                End If
            Catch ex As Exception
                lblProfileMsg.Text = ex.Message
                lblProfileMsg.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
            lblPassMsg.Text = String.Empty
            Dim curPass As String = txtCurPass.Text
            Dim newPass As String = txtNewPass.Text
            Dim confPass As String = txtConfPass.Text

            If String.IsNullOrEmpty(curPass) OrElse String.IsNullOrEmpty(newPass) Then
                lblPassMsg.Text = "Please fill in all password fields."
                lblPassMsg.ForeColor = Color.Red
                Return
            End If

            If Not AuthService.VerifyPassword(curPass, _customer.Salt, _customer.PasswordHash) Then
                lblPassMsg.Text = "Current password is incorrect."
                lblPassMsg.ForeColor = Color.Red
                Return
            End If

            If newPass.Length < 6 Then
                lblPassMsg.Text = "New password must be at least 6 characters long."
                lblPassMsg.ForeColor = Color.Red
                Return
            End If

            If Not ValidationRules.IsValidPassword(newPass) Then
                lblPassMsg.Text = "New password cannot contain spaces."
                lblPassMsg.ForeColor = Color.Red
                Return
            End If

            If newPass <> confPass Then
                lblPassMsg.Text = "New passwords do not match."
                lblPassMsg.ForeColor = Color.Red
                Return
            End If

            Try
                Dim newSalt As String = AuthService.GenerateSalt()
                Dim newHash As String = AuthService.HashPassword(newPass, newSalt)
                If _userRepo.UpdatePassword(_customer.UserId, newHash, newSalt) Then
                    _customer.PasswordHash = newHash
                    _customer.Salt = newSalt
                    lblPassMsg.Text = "Password changed successfully!"
                    lblPassMsg.ForeColor = Color.DarkGreen
                    txtCurPass.Clear()
                    txtNewPass.Clear()
                    txtConfPass.Clear()
                Else
                    lblPassMsg.Text = "Failed to update password."
                    lblPassMsg.ForeColor = Color.Red
                End If
            Catch ex As Exception
                lblPassMsg.Text = ex.Message
                lblPassMsg.ForeColor = Color.Red
            End Try
        End Sub

        ' Typing-level validation: profile name accepts letters only, password fields block spaces
        Private Sub BlockDigitsInProfileName(sender As Object, e As KeyPressEventArgs) Handles txtProfFullName.KeyPress
            Dim allowedPunct As String = " '-.,"
            If Not (Char.IsControl(e.KeyChar) OrElse Char.IsLetter(e.KeyChar) OrElse
                    Char.IsWhiteSpace(e.KeyChar) OrElse allowedPunct.Contains(e.KeyChar.ToString())) Then
                e.Handled = True
            End If
        End Sub

        Private Sub BlockSpaceTyping(sender As Object, e As KeyPressEventArgs) _
            Handles txtCurPass.KeyPress, txtNewPass.KeyPress, txtConfPass.KeyPress
            If Char.IsWhiteSpace(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
            If MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Close()
            End If
        End Sub

#Region "Figma UI restyle (visual only - no business behavior changes)"

        ' ================= UI-only state / new helper controls =================
        Private _cardFlow As FlowLayoutPanel
        Private _lblCardFooter As Label
        Private _lblCardsEmpty As Label
        Private _lblHistoryFooter As Label
        Private _lblHistoryEmpty As Label
        Private _lastProductCount As Integer = 0

        ''' <summary>Applies the design tokens + layout. Visual only.</summary>
        Private Sub InitializeUiRestyle()
            Me.Text = "Mini-Grocery Store · Customer"
            Me.ClientSize = New Size(1280, 800)
            Me.MinimumSize = New Size(1024, 700)
            ' QA E1/E4: the portal opens maximized — set here in the constructor,
            ' before the window handle is shown, so no other size can flash. The
            ' restore button returns to the sensible 1280x800 bounds above and the
            ' minimum size keeps the docked/anchored layouts intact; this is the
            ' normal maximized state, so the taskbar stays visible.
            Me.WindowState = FormWindowState.Maximized

            AppHeader.Apply(pnlHeader, lblAppTitle, lblUserInfo, btnLogout)
            ShellTabs.Apply(tabCustomer,
                New ShellTabSpec(tabBrowse, "Browse", "basket"),
                New ShellTabSpec(tabHistory, "History", "receipt"),
                New ShellTabSpec(tabProfile, "Profile", "user"))

            RestyleBrowse()
            RestyleHistory()
            RestyleProfile()

            Theme.RefreshLayout(Me)
        End Sub

        ' ================= Browse tab (card gallery) =================
        Private Sub RestyleBrowse()
            tabBrowse.BackColor = Theme.PageBg

            ' Legacy filter strip: hidden + detached, fields kept.
            pnlFilter.Visible = False
            tabBrowse.Controls.Remove(pnlFilter)

            ' IMPORTANT: the product grid keeps loading data (all handlers intact);
            ' it is simply hidden because the design shows a card gallery.
            dgvProducts.Visible = False

            ' ---------- Body: filter row (top) + gallery card (fill) ----------
            Dim galleryCard As New CardPanel()
            galleryCard.Dock = DockStyle.Fill
            galleryCard.Margin = New Padding(0)
            galleryCard.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            _cardFlow = New FlowLayoutPanel()
            _cardFlow.Dock = DockStyle.Fill
            _cardFlow.AutoScroll = True
            _cardFlow.FlowDirection = FlowDirection.LeftToRight
            _cardFlow.WrapContents = True
            _cardFlow.BackColor = Theme.White
            _cardFlow.Margin = New Padding(0)
            _cardFlow.Padding = New Padding(2, 4, 6, 6)
            ' QA A4: recompute uniform tile widths whenever the gallery's client
            ' width changes (window resize, scrollbar appearing/disappearing) so each
            ' row fills the available width instead of leaving a dead strip on the right.
            AddHandler _cardFlow.ClientSizeChanged, Sub() LayoutGalleryTiles()

            _lblCardsEmpty = New Label()
            _lblCardsEmpty.Text = "No products match your filters."
            _lblCardsEmpty.Font = Theme.FontBody12
            _lblCardsEmpty.ForeColor = Theme.Muted
            _lblCardsEmpty.BackColor = Theme.White
            _lblCardsEmpty.TextAlign = ContentAlignment.MiddleCenter
            _lblCardsEmpty.AutoSize = True
            _lblCardsEmpty.Visible = False
            body.Controls.Add(_lblCardsEmpty)

            body.Controls.Add(_cardFlow)

            ' Card header row: "Products" (existing lblSearch) + footer (bottom)
            Dim header As New Panel()
            header.Dock = DockStyle.Top
            header.Height = 30
            header.BackColor = Theme.White
            header.Margin = New Padding(0)

            lblSearch.Text = "Products"
            lblSearch.Font = Theme.FontSection
            lblSearch.ForeColor = Theme.Ink
            lblSearch.AutoSize = True
            lblSearch.BackColor = Theme.White
            lblSearch.Location = New Point(2, 4)
            header.Controls.Add(lblSearch)

            _lblCardFooter = GridStyler.NewFooter()
            _lblCardFooter.Height = 24
            _lblCardFooter.Dock = DockStyle.Bottom
            _lblCardFooter.BackColor = Theme.White

            galleryCard.Controls.Add(body)
            galleryCard.Controls.Add(_lblCardFooter)
            galleryCard.Controls.Add(header)

            ' ---------- Filter row: search + category ----------
            Dim filterRow As New Panel()
            filterRow.Dock = DockStyle.Top
            filterRow.Height = 46
            filterRow.BackColor = Theme.PageBg
            filterRow.Margin = New Padding(0)

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Fill
            tlp.BackColor = Theme.PageBg
            tlp.Margin = New Padding(0)
            tlp.Padding = New Padding(0)
            tlp.ColumnCount = 2
            tlp.RowCount = 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 292F))
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            Dim searchWrap As New InputPanel()
            searchWrap.Dock = DockStyle.Fill
            searchWrap.Margin = New Padding(0, 6, 12, 6)
            Theme.StyleInput(txtSearch)
            Theme.SetCue(txtSearch, "Search products · type to filter")
            searchWrap.Controls.Add(txtSearch)
            searchWrap.Bind(txtSearch)
            AddHandler searchWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    txtSearch.Location = New Point(12, Math.Max(0, (searchWrap.ClientSize.Height - txtSearch.Height) \ 2))
                    txtSearch.Width = searchWrap.ClientSize.Width - 24
                End Sub
            tlp.Controls.Add(searchWrap, 0, 0)

            ' Category dropdown with inner caption (existing lblCategory/cboCategory)
            Dim catWrap As New InputPanel()
            catWrap.Dock = DockStyle.Fill
            catWrap.Margin = New Padding(0, 6, 0, 6)
            lblCategory.Text = "Category"
            lblCategory.Font = Theme.FontSmall
            lblCategory.ForeColor = Theme.Muted
            lblCategory.AutoSize = True
            lblCategory.BackColor = Theme.White
            catWrap.Controls.Add(lblCategory)
            Theme.StyleCombo(cboCategory)
            catWrap.Controls.Add(cboCategory)
            AddHandler catWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    Dim w As Integer = catWrap.ClientSize.Width
                    Dim hh As Integer = catWrap.ClientSize.Height
                    lblCategory.Location = New Point(10, Math.Max(0, (hh - lblCategory.Height) \ 2))
                    cboCategory.Location = New Point(10 + lblCategory.Width + 6, Math.Max(0, (hh - cboCategory.Height) \ 2))
                    cboCategory.Width = Math.Max(80, w - cboCategory.Left - 8)
                End Sub
            tlp.Controls.Add(catWrap, 1, 0)

            filterRow.Controls.Add(tlp)

            ' Assembly: Fill first, then strips, heading LAST.
            tabBrowse.Controls.Add(galleryCard)
            tabBrowse.Controls.Add(filterRow)
            Dim heading As PageHeading = PageHeading.Build(tabBrowse, "Browse", _
                "Everyday essentials with live stock status. Prices are in Philippine pesos.")
            heading.AddChip(New StatusChip("Catalogue · Live stock", ThemeTone.Success))
        End Sub

        ' ================= QA A4: responsive uniform tile grid =================
        Private _tileGridCols As Integer = -1
        Private _tileGridTileWidth As Integer = -1

        ''' <summary>
        ''' Sizes product tiles to one uniform width that divides the available flow
        ''' width. Root cause: tiles fixed at 172px never divided the row, leaving a
        ''' wide empty strip to the right of the gallery at any window size. Columns
        ''' come from a minimum-tile floor so tiles never get too small, and the
        ''' cached (cols, width) guard prevents layout feedback loops when a
        ''' scrollbar change re-fires the client-size event.
        ''' </summary>
        Private Sub LayoutGalleryTiles()
            If _cardFlow Is Nothing Then Return

            ' Children flow area = client width minus the flow panel's horizontal padding.
            Dim avail As Integer = _cardFlow.ClientSize.Width - _cardFlow.Padding.Horizontal
            If avail < 120 Then Return ' too narrow to lay out meaningfully; scroll handles the rest
            Const tileMarginH As Integer = 14 ' tile Margin 7 + 7
            Dim minTileW As Integer = ProductCardTile.MinTileWidth
            Dim cols As Integer = Math.Max(1, avail \ (minTileW + tileMarginH))
            Dim tileW As Integer = Math.Max(minTileW, (avail \ cols) - tileMarginH)

            If cols = _tileGridCols AndAlso tileW = _tileGridTileWidth Then Return
            _tileGridCols = cols
            _tileGridTileWidth = tileW
            For Each c As Control In _cardFlow.Controls
                Dim tile As ProductCardTile = TryCast(c, ProductCardTile)
                If tile IsNot Nothing AndAlso Not tile.IsSharedLegacy Then
                    tile.Width = tileW
                End If
            Next
        End Sub

        ''' <summary>Rebuilds the product card gallery from the same result list LoadProducts produced.</summary>
        Private Sub UpdateCardGallery(products As List(Of Product))
            If _cardFlow Is Nothing Then Return

            ' New tiles start at default size: drop the cached grid math so the
            ' layout pass below cannot skip resizing them.
            _tileGridCols = -1
            _tileGridTileWidth = -1

            _cardFlow.SuspendLayout()
            For i As Integer = _cardFlow.Controls.Count - 1 To 0 Step -1
                Dim card As ProductCardTile = TryCast(_cardFlow.Controls(i), ProductCardTile)
                If card IsNot Nothing AndAlso card.IsSharedLegacy Then Continue For
                _cardFlow.Controls.RemoveAt(i)
            Next

            For Each p In products
                Dim card As New ProductCardTile()
                card.SetData(p.ProductCode, p.Name, p.CategoryName, Convert.ToDecimal(p.Price), p.GetStockStatus())
                _cardFlow.Controls.Add(card)
            Next
            _cardFlow.ResumeLayout()
            LayoutGalleryTiles()

            Dim n As Integer = If(products IsNot Nothing, products.Count, 0)
            If _lblCardFooter IsNot Nothing Then
                GridStyler.UpdateFooter(_lblCardFooter, n, n, If(n = 1, "product", "products"))
            End If
            If _lblCardsEmpty IsNot Nothing Then
                _lblCardsEmpty.Visible = (n = 0)
                If _lblCardsEmpty.Visible Then
                    _lblCardsEmpty.Left = Math.Max(0, (_cardFlow.ClientSize.Width - _lblCardsEmpty.Width) \ 2)
                    _lblCardsEmpty.Top = Math.Max(0, (_cardFlow.ClientSize.Height - _lblCardsEmpty.Height) \ 2)
                    _lblCardsEmpty.BringToFront()
                End If
            End If
        End Sub

        ' ================= History tab =================
        Private Sub RestyleHistory()
            tabHistory.BackColor = Theme.PageBg

            pnlHistoryTop.Visible = False
            tabHistory.Controls.Remove(pnlHistoryTop)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvHistory)
            GridStyler.ApplyWeights(dgvHistory, 1.3, 1.4, 1.2, 1.0, 1.0, 1.1)
            body.Controls.Add(dgvHistory)
            _lblHistoryEmpty = GridStyler.AttachEmptyOverlay(dgvHistory, "No purchases yet — completed sales will appear here.")

            _lblHistoryFooter = GridStyler.NewFooter()
            _lblHistoryFooter.Height = 24
            _lblHistoryFooter.Dock = DockStyle.Bottom
            _lblHistoryFooter.BackColor = Theme.White

            card.Controls.Add(body)
            card.Controls.Add(_lblHistoryFooter)

            tabHistory.Controls.Add(card)

            Dim heading As PageHeading = PageHeading.Build(tabHistory, "Purchase history", _
                "Review every receipt issued under your account.")
            heading.AddChip(New StatusChip("Credit ledger · Personal", ThemeTone.Neutral))

            btnRefreshHistory.Text = "Refresh"
            Theme.StyleButton(btnRefreshHistory, ThemeButtonKind.Secondary)
            btnRefreshHistory.AutoSize = True
            heading.AddAction(btnRefreshHistory)

            btnViewReceiptDetails.Text = "View receipt"
            Theme.StyleButton(btnViewReceiptDetails, ThemeButtonKind.Secondary)
            btnViewReceiptDetails.AutoSize = True
            heading.AddAction(btnViewReceiptDetails)
        End Sub

        Private Sub UpdateHistoryFooter(total As Integer)
            If _lblHistoryFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblHistoryFooter, total, total, If(total = 1, "receipt", "receipts"))
            If _lblHistoryEmpty IsNot Nothing Then _lblHistoryEmpty.Visible = (dgvHistory.RowCount = 0)
        End Sub

        ' ================= Profile tab (cards) =================
        Private Sub RestyleProfile()
            tabProfile.BackColor = Theme.PageBg

            ' Legacy group boxes: hidden + detached, all fields kept.
            grpProfile.Visible = False
            grpPassword.Visible = False
            tabProfile.Controls.Remove(grpProfile)
            tabProfile.Controls.Remove(grpPassword)

            ' ---------- Profile information card ----------
            Dim profCard As New CardPanel()
            profCard.Dock = DockStyle.Fill
            profCard.Margin = New Padding(0, 0, 6, 0)
            profCard.Padding = New Padding(16, 12, 16, 12)

            Dim profTlp As New TableLayoutPanel()
            profTlp.Dock = DockStyle.Fill
            profTlp.BackColor = Theme.White
            profTlp.ColumnCount = 1
            profTlp.RowCount = 7
            profTlp.Margin = New Padding(0)
            profTlp.Padding = New Padding(0)
            For r As Integer = 0 To 5
                profTlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            profTlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            Dim addProf As Action(Of Control) = Sub(c As Control)
                                                    profTlp.Controls.Add(c, 0, profTlp.Controls.Count)
                                                End Sub

            Dim lblProfTitle As New Label()
            lblProfTitle.Text = "Profile information"
            lblProfTitle.Font = Theme.FontCardTitle
            lblProfTitle.ForeColor = Theme.Ink
            lblProfTitle.AutoSize = True
            lblProfTitle.BackColor = Color.Transparent
            addProf(lblProfTitle)

            lblProfUsername.Font = Theme.FontBody12
            lblProfUsername.ForeColor = Theme.Muted
            lblProfUsername.AutoSize = True
            lblProfUsernameVal.Font = Theme.FontBody13Semi
            lblProfUsernameVal.ForeColor = Theme.Ink
            lblProfUsernameVal.AutoSize = True
            addProf(MakePair(lblProfUsername, lblProfUsernameVal, 26))

            lblProfFullName.Font = Theme.FontBody12
            lblProfFullName.ForeColor = Theme.Muted
            lblProfFullName.AutoSize = True
            lblProfFullName.Margin = New Padding(0, 8, 0, 0)
            addProf(lblProfFullName)

            Dim nameWrap As New InputPanel()
            nameWrap.Dock = DockStyle.Fill
            nameWrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleInput(txtProfFullName)
            nameWrap.Controls.Add(txtProfFullName)
            AddHandler nameWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    txtProfFullName.Location = New Point(12, Math.Max(0, (nameWrap.ClientSize.Height - txtProfFullName.Height) \ 2))
                    txtProfFullName.Width = nameWrap.ClientSize.Width - 24
                End Sub
            addProf(nameWrap)

            btnSaveProfile.Text = "Save profile"
            Theme.StyleButton(btnSaveProfile, ThemeButtonKind.Primary)
            btnSaveProfile.AutoSize = True
            btnSaveProfile.Margin = New Padding(0, 14, 12, 0)

            lblProfileMsg.Font = Theme.FontBody12
            lblProfileMsg.AutoSize = False
            lblProfileMsg.Dock = DockStyle.Fill
            lblProfileMsg.TextAlign = ContentAlignment.MiddleLeft
            lblProfileMsg.BackColor = Color.Transparent

            Dim saveRow As New Panel()
            saveRow.Dock = DockStyle.Fill
            saveRow.Height = 40
            saveRow.Margin = New Padding(0, 14, 0, 0)
            saveRow.BackColor = Color.Transparent
            saveRow.Controls.Add(lblProfileMsg)
            saveRow.Controls.Add(btnSaveProfile)
            AddHandler saveRow.Resize,
                Sub(s As Object, e As EventArgs)
                    btnSaveProfile.Location = New Point(0, 0)
                    lblProfileMsg.Location = New Point(btnSaveProfile.Width + 12, (saveRow.ClientSize.Height - Math.Min(20, lblProfileMsg.Height)) \ 2)
                    lblProfileMsg.Size = New Size(Math.Max(60, saveRow.ClientSize.Width - btnSaveProfile.Width - 16), saveRow.ClientSize.Height)
                End Sub
            addProf(saveRow)

            Dim profFiller As New Panel()
            profFiller.Dock = DockStyle.Fill
            profFiller.BackColor = Color.Transparent
            profFiller.Margin = New Padding(0)
            addProf(profFiller)

            profCard.Controls.Add(profTlp)

            ' ---------- Change password card ----------
            Dim passCard As New CardPanel()
            passCard.Dock = DockStyle.Fill
            passCard.Margin = New Padding(6, 0, 0, 0)
            passCard.Padding = New Padding(16, 12, 16, 12)

            Dim passTlp As New TableLayoutPanel()
            passTlp.Dock = DockStyle.Fill
            passTlp.BackColor = Theme.White
            passTlp.ColumnCount = 1
            passTlp.RowCount = 9
            passTlp.Margin = New Padding(0)
            passTlp.Padding = New Padding(0)
            For r As Integer = 0 To 7
                passTlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            passTlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            Dim addPass As Action(Of Control) = Sub(c As Control)
                                                    passTlp.Controls.Add(c, 0, passTlp.Controls.Count)
                                                End Sub

            Dim lblPassTitle As New Label()
            lblPassTitle.Text = "Change password"
            lblPassTitle.Font = Theme.FontCardTitle
            lblPassTitle.ForeColor = Theme.Ink
            lblPassTitle.AutoSize = True
            addPass(lblPassTitle)

            addPass(BuildPasswordField(lblCurPass, txtCurPass))
            addPass(BuildPasswordField(lblNewPass, txtNewPass))
            addPass(BuildPasswordField(lblConfPass, txtConfPass))

            btnChangePassword.Text = "Change password"
            Theme.StyleButton(btnChangePassword, ThemeButtonKind.Primary)
            btnChangePassword.AutoSize = True
            btnChangePassword.Margin = New Padding(0, 14, 12, 0)

            lblPassMsg.Font = Theme.FontBody12
            lblPassMsg.AutoSize = False
            lblPassMsg.Dock = DockStyle.Fill
            lblPassMsg.TextAlign = ContentAlignment.MiddleLeft
            lblPassMsg.BackColor = Color.Transparent

            Dim passRow As New Panel()
            passRow.Dock = DockStyle.Fill
            passRow.Height = 40
            passRow.Margin = New Padding(0, 14, 0, 0)
            passRow.BackColor = Color.Transparent
            passRow.Controls.Add(lblPassMsg)
            passRow.Controls.Add(btnChangePassword)
            AddHandler passRow.Resize,
                Sub(s As Object, e As EventArgs)
                    btnChangePassword.Location = New Point(0, 0)
                    lblPassMsg.Location = New Point(btnChangePassword.Width + 12, (passRow.ClientSize.Height - Math.Min(20, lblPassMsg.Height)) \ 2)
                    lblPassMsg.Size = New Size(Math.Max(60, passRow.ClientSize.Width - btnChangePassword.Width - 16), passRow.ClientSize.Height)
                End Sub
            addPass(passRow)

            Dim passFiller As New Panel()
            passFiller.Dock = DockStyle.Fill
            passFiller.BackColor = Color.Transparent
            passFiller.Margin = New Padding(0)
            addPass(passFiller)

            passCard.Controls.Add(passTlp)

            ' ---------- Two-column body ----------
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
            columns.Controls.Add(profCard, 0, 0)
            columns.Controls.Add(passCard, 1, 0)

            tabProfile.Controls.Add(columns)

            Dim heading As PageHeading = PageHeading.Build(tabProfile, "My profile", _
                "Keep your account details current. Password changes take effect on your next sign-in.")
            heading.AddChip(New StatusChip("Signed in · Personal", ThemeTone.Info))
        End Sub

        ''' <summary>Label above an input: returns [label; input] stacked in a 54px panel.</summary>
        Private Function BuildPasswordField(fieldLabel As Label, fieldInput As TextBox) As Panel
            fieldLabel.Font = Theme.FontBody12
            fieldLabel.ForeColor = Theme.Muted
            fieldLabel.AutoSize = True
            fieldLabel.BackColor = Color.Transparent
            fieldLabel.Location = New Point(0, 0)

            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.None
            wrap.Location = New Point(0, 20)
            wrap.Size = New Size(240, Theme.HeightButton)
            Theme.StyleInput(fieldInput)
            wrap.Controls.Add(fieldInput)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    fieldInput.Location = New Point(12, Math.Max(0, (wrap.ClientSize.Height - fieldInput.Height) \ 2))
                    fieldInput.Width = wrap.ClientSize.Width - 24
                End Sub

            Dim holder As New Panel()
            holder.Dock = DockStyle.Top
            holder.Height = 58
            holder.Margin = New Padding(0, 8, 0, 0)
            holder.BackColor = Color.Transparent
            holder.Controls.Add(wrap)
            holder.Controls.Add(fieldLabel)

            AddHandler holder.Resize,
                Sub(s As Object, e As EventArgs)
                    wrap.Width = Math.Max(160, holder.ClientSize.Width - 2)
                End Sub

            Return holder
        End Function

        ''' <summary>label left / value right pair for profile details.</summary>
        Private Function MakePair(leftLbl As Label, rightLbl As Label, rowHeight As Integer) As TableLayoutPanel
            Dim t As New TableLayoutPanel()
            t.Dock = DockStyle.Fill
            t.Height = rowHeight
            t.Margin = New Padding(0, 2, 0, 0)
            t.BackColor = Color.Transparent
            t.ColumnCount = 2
            t.RowCount = 1
            t.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            t.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            rightLbl.Dock = DockStyle.Fill
            rightLbl.TextAlign = ContentAlignment.MiddleLeft
            t.Controls.Add(rightLbl, 1, 0)
            t.Controls.Add(leftLbl, 0, 0)
            Return t
        End Function

#End Region
    End Class

    ' ==============================================================================
    ' ProductCardTile - pastel category icon tile for the customer Browse gallery.
    ' Visual-only control (display): reads product data on SetData and paints itself.
    ' ==============================================================================
    Public Class ProductCardTile
        Inherits Control

        ''' <summary>
        ''' Minimum tile width used by the gallery layout math (QA A4): rows are
        ''' divided starting from this floor, then stretched to fill the width.
        ''' </summary>
        Public Const MinTileWidth As Integer = 176

        Private _code As String = String.Empty
        Private _productName As String = String.Empty
        Private _categoryName As String = String.Empty
        Private _priceText As String = String.Empty
        Private _statusText As String = String.Empty

        Private _tileBg As Color
        Private _tileInk As Color
        Private _statusIndex As Integer  ' 0 neutral, 1 in stock, 2 low, 3 out

        Private Shared ReadOnly PastelBgs As Color() = {
            Color.FromArgb(245, 240, 250),   ' lavender
            Color.FromArgb(250, 238, 241),   ' rose
            Color.FromArgb(244, 247, 236),   ' sage
            Color.FromArgb(238, 241, 248),   ' sky
            Color.FromArgb(250, 241, 232),   ' apricot
            Color.FromArgb(236, 245, 244)}   ' mint

        Private Shared ReadOnly PastelInks As Color() = {
            Color.FromArgb(124, 92, 168),
            Color.FromArgb(154, 84, 112),
            Color.FromArgb(110, 132, 74),
            Color.FromArgb(96, 110, 160),
            Color.FromArgb(146, 116, 60),
            Color.FromArgb(66, 124, 118)}

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)

            ' QA A1: the old fixed 172x148 tile could not fit a 2-line product name
            ' plus code, category and price rows, so the second name line collided
            ' with the code/category text. Height is computed from the real fonts.
            Size = New Size(MinTileWidth + 40, TileHeight)
            Margin = New Padding(7, 7, 7, 7)
            _statusIndex = 0
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        ''' <summary>
        ''' Marks tiles that wrap legacy external (hidden) controls; never discarded
        ''' during gallery rebuilds.
        ''' </summary>
        Public Property IsSharedLegacy As Boolean = False

        Public Sub SetData(code As String, name As String, categoryName As String, price As Decimal, status As String)
            _code = code
            _productName = name
            _categoryName = categoryName
            _priceText = $"PHP {price:N2}"
            _statusText = status

            Dim st As String = If(status, String.Empty)
            If String.Equals(st, "In Stock", StringComparison.OrdinalIgnoreCase) Then
                _statusIndex = 1
            ElseIf String.Equals(st, "Low Stock", StringComparison.OrdinalIgnoreCase) Then
                _statusIndex = 2
            ElseIf String.Equals(st, "Out of Stock", StringComparison.OrdinalIgnoreCase) Then
                _statusIndex = 3
            Else
                _statusIndex = 0
            End If

            ' Deterministic pastel from the product code + name
            Dim h As Integer = 17
            Dim source As String = code & name
            For Each ch As Char In source
                h = (h * 31 + AscW(ch)) Mod 6
            Next
            _tileBg = PastelBgs(h)
            _tileInk = PastelInks(h)

            ' QA A1: full name stays reachable via tooltip even when the painted
            ' text is ellipsised; code · category added as context.
            TileTip.SetToolTip(Me, If(name, String.Empty) & vbCrLf & _code & " · " & _categoryName)

            Invalidate()
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Theme.RoundControl(Me, Theme.RadiusCard)
        End Sub

        Private Function StatusColors() As Tuple(Of Color, Color)
            Select Case _statusIndex
                Case 1 : Return Tuple.Create(Theme.GreenTint, Theme.Green)
                Case 2 : Return Tuple.Create(Theme.WarnTint, Theme.Warn)
                Case 3 : Return Tuple.Create(Theme.ErrorTint, Theme.ErrorText)
                Case Else : Return Tuple.Create(Theme.GridHeader, Theme.Muted)
            End Select
        End Function

        ''' <summary>One cached font pair per tile class (avoid per-paint allocations).</summary>
        Private Shared ReadOnly FontTileName As Font = Theme.F(12.0F, ThemeWeight.Semibold)
        Private Shared ReadOnly FontTilePrice As Font = Theme.F(12.5F, ThemeWeight.Semibold)

        ''' <summary>Shared tooltip so every tile shows its full name when ellipsised (QA A1).</summary>
        Private Shared ReadOnly TileTip As New ToolTip()

        ''' <summary>
        ''' Tile height measured from the actual fonts (QA A1): icon band 66 +
        ''' two name lines + code line + category line + price row + bottom margin.
        ''' </summary>
        Private Shared ReadOnly TileHeight As Integer = ComputeTileHeight()

        Private Shared Function ComputeTileHeight() As Integer
            Dim nameH As Integer = FontTileName.Height * 2
            Dim codeH As Integer = Theme.FontSmall.Height + 2   ' code line
            Dim catH As Integer = Theme.FontSmall.Height + 2    ' category line
            Dim priceH As Integer = FontTilePrice.Height + 4
            Return CInt(Math.Ceiling(66.0 + nameH + 4 + codeH + 2 + catH + 8 + priceH + 12))
        End Function

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            Dim w As Integer = ClientSize.Width
            Dim h As Integer = ClientSize.Height

            Using path As System.Drawing.Drawing2D.GraphicsPath = Theme.RoundedPath(New Rectangle(0, 0, w - 1, h - 1), Theme.RadiusCard)
                Using b As New SolidBrush(Theme.White)
                    e.Graphics.FillPath(b, path)
                End Using
                Using p As New Pen(Theme.BorderLine, 1.0F)
                    e.Graphics.DrawPath(p, path)
                End Using
            End Using

            ' Pastel icon tile (44px)
            Dim tileRect As New Rectangle(14, 14, 44, 44)
            Using path As System.Drawing.Drawing2D.GraphicsPath = Theme.RoundedPath(tileRect, 8)
                Using b As New SolidBrush(_tileBg)
                    e.Graphics.FillPath(b, path)
                End Using
            End Using
            Dim icon As Bitmap = IconArt.Get(GetGlyphKey(), _tileInk, 22)
            If icon IsNot Nothing Then
                e.Graphics.DrawImage(icon, tileRect.X + (44 - 22) \ 2, tileRect.Y + (44 - 22) \ 2, 22, 22)
            End If

            ' Status pill (top right)
            Dim pillPair As Tuple(Of Color, Color) = StatusColors()
            Dim pillText As String = _statusText
            Dim pillW As Integer = 24 + TextRenderer.MeasureText(pillText, Theme.FontSmall).Width
            Dim pillRect As New Rectangle(w - pillW - 12, 16, pillW, 21)
            Using path As System.Drawing.Drawing2D.GraphicsPath = Theme.RoundedPath(pillRect, pillRect.Height \ 2)
                Using b As New SolidBrush(pillPair.Item1)
                    e.Graphics.FillPath(b, path)
                End Using
            End Using
            TextRenderer.DrawText(g, pillText, Theme.FontSmall, pillRect, pillPair.Item2,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)

            ' Name — QA A1: two full lines with ellipsis. The rect height is measured
            ' from the real font (2 × line height) instead of the old hardcoded 32px,
            ' so the second line no longer clips or collides with the code/category
            ' row. QA A3: NoPrefix stops TextRenderer from eating "&" as a mnemonic
            ' prefix ("Head & Shoulders", "Sunsilk Smooth & Manageable").
            Dim nameLineH As Integer = FontTileName.Height
            Dim nameRect As New Rectangle(14, 66, w - 28, nameLineH * 2)
            TextRenderer.DrawText(g, _productName, FontTileName, nameRect, Theme.Ink,
                                  TextFormatFlags.WordBreak Or TextFormatFlags.EndEllipsis Or
                                  TextFormatFlags.NoPrefix)

            ' Code — its own line (QA A1): keeps the identity row away from the name
            ' rows entirely.
            Dim smallH As Integer = Theme.FontSmall.Height + 2
            Dim codeRect As New Rectangle(14, nameRect.Bottom + 4, w - 28, smallH)
            TextRenderer.DrawText(g, _code, Theme.FontSmall, codeRect, Theme.Muted,
                                  TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or
                                  TextFormatFlags.NoPrefix)

            ' Category — QA A2: full category on its own full-width line, so
            ' "Beverages & Drinks" / "Personal Care & Toiletries" are not truncated
            ' against the code text. Ellipsis remains as a last-resort at extreme sizes.
            Dim catRect As New Rectangle(14, codeRect.Bottom + 2, w - 28, smallH)
            TextRenderer.DrawText(g, _categoryName, Theme.FontSmall, catRect, Theme.Muted,
                                  TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or
                                  TextFormatFlags.NoPrefix)

            ' Price — anchored to the tile bottom; NoPrefix for consistency.
            Dim priceH As Integer = FontTilePrice.Height + 4
            Dim priceRect As New Rectangle(14, h - priceH - 12, w - 28, priceH)
            TextRenderer.DrawText(g, _priceText, FontTilePrice, priceRect, Theme.Green,
                                  TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix)
        End Sub

        Private Function GetGlyphKey() As String
            ' pastel tile keeps the basket family design across categories
            Return "basket"
        End Function

    End Class
End Namespace
