' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: CashierDashboardForm (Code-Behind)
' OOP Concept: POLYMORPHISM (DISCOUNTS), TRANSACTION INTEGRITY, ENCAPSULATION
' Provides the full cashier checkout terminal: item catalog lookup, live cart
' management, polymorphic discount calculation, transaction checkout, receipt
' printing, and daily cashier sales auditing.
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
    Public Class CashierDashboardForm
        Private ReadOnly _cashier As Cashier
        Private ReadOnly _cartService As New CartService()
        Private ReadOnly _productRepo As New ProductRepository()
        Private ReadOnly _categoryRepo As New CategoryRepository()
        Private ReadOnly _saleRepo As New SaleRepository()
        Private ReadOnly _userRepo As New UserRepository()

        Private _lastCompletedSale As Sale = Nothing
        Private _isCheckingOut As Boolean = False

        Public Sub New(cashier As Cashier)
            InitializeComponent()
            _cashier = cashier
            Me.DoubleBuffered = True
            InitializeUiRestyle()
        End Sub

        Private Sub CashierDashboardForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            Me.Refresh()
        End Sub

        Private Sub CashierDashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' SECURITY: verify role on form load
            If _cashier.Role <> "Cashier" Then
                MessageBox.Show("Access denied: this dashboard is restricted to Cashier accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.BeginInvoke(New MethodInvoker(Sub() Me.Close()))
                Return
            End If

            ' Step 7 (Figma): "Role: Cashier · <Full name> (@username)" — the role is
            ' rendered exactly once; GetDisplayName strips any legacy "(Role)" suffix.
            lblCashierInfo.Text = $"Role: {_cashier.GetRoleName()} · {_cashier.GetDisplayName()} (@{_cashier.Username})"

            PopulateCategories()
            PopulateDiscounts()
            PopulateCustomers()
            LoadPosProducts()
            RefreshCartGrid()
            LoadTodayTransactions()
        End Sub

        Private Sub PopulateCategories()
            cboPosCategory.Items.Clear()
            cboPosCategory.Items.Add("All Categories")
            Dim cats As List(Of Category) = _categoryRepo.GetAll()
            For Each c In cats
                cboPosCategory.Items.Add(c)
            Next
            cboPosCategory.SelectedIndex = 0
        End Sub

        ''' <summary>
        ''' Demonstrates POLYMORPHISM: Populate discount strategies inheriting from base Discount.
        ''' </summary>
        Private Sub PopulateDiscounts()
            cboDiscount.Items.Clear()
            cboDiscount.Items.Add(New NoDiscount())
            cboDiscount.Items.Add(New SeniorPwdDiscount())
            cboDiscount.Items.Add(New RegularDiscount(5D))
            cboDiscount.Items.Add(New RegularDiscount(10D))
            cboDiscount.SelectedIndex = 0
        End Sub

        Private Sub PopulateCustomers()
            cboCustomer.Items.Clear()
            cboCustomer.Items.Add("Walk-In Customer")
            Dim customers As List(Of Customer) = _userRepo.GetAllCustomers()
            For Each cust In customers
                cboCustomer.Items.Add(cust)
            Next
            cboCustomer.SelectedIndex = 0
        End Sub

        Private Sub LoadPosProducts()
            dgvPosProducts.Rows.Clear()
            Dim kw As String = txtPosSearch.Text.Trim()
            Dim catId As Nullable(Of Integer) = Nothing

            If cboPosCategory.SelectedIndex > 0 Then
                Dim selectedCat As Category = TryCast(cboPosCategory.SelectedItem, Category)
                If selectedCat IsNot Nothing Then
                    catId = selectedCat.CategoryId
                End If
            End If

            Dim list As List(Of Product) = _productRepo.Search(kw, catId, activeOnly:=True)
            For Each p In list
                Dim rowIdx As Integer = dgvPosProducts.Rows.Add(p.ProductCode, p.Name, p.Price.ToString("N2"), p.StockQty)
                ' Visual highlight for zero or low stock
                If p.StockQty <= 0 Then
                    dgvPosProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.Red
                ElseIf p.StockQty <= p.ReorderLevel Then
                    dgvPosProducts.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.DarkOrange
                End If
            Next

            ' RESTYLE (visual only): grid footer + empty state
            _posLoadedCount = list.Count
            _posTotalCount = list.Count
            ' Step 3: keep one row highlighted so Enter / Add to Cart can act even
            ' before the cashier navigates the popup.
            If dgvPosProducts.RowCount > 0 AndAlso dgvPosProducts.SelectedRows.Count = 0 Then
                dgvPosProducts.Rows(0).Selected = True
            End If
            UpdateProductsFooter()
        End Sub

        Private Sub txtPosSearch_TextChanged(sender As Object, e As EventArgs) Handles txtPosSearch.TextChanged
            LoadPosProducts()
            ' Step 3: the results popup tracks the scan box — open while it has text.
            If txtPosSearch.Text.Trim().Length > 0 Then
                ShowProductsPopup(True)
            ElseIf _productsPopupOpen Then
                ShowProductsPopup(False)
            End If
        End Sub

        Private Sub cboPosCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPosCategory.SelectedIndexChanged
            LoadPosProducts()
        End Sub

        Private Sub btnAddToCart_Click(sender As Object, e As EventArgs) Handles btnAddToCart.Click
            If dgvPosProducts.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a product from the list.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim code As String = Convert.ToString(dgvPosProducts.SelectedRows(0).Cells(0).Value)
            Dim prod As Product = _productRepo.GetByCode(code)
            If prod Is Nothing Then Return

            ' BUSINESS RULE: expired products cannot be sold
            If prod.IsExpired() Then
                MessageBox.Show($"'{prod.Name}' expired on {prod.ExpiryDate.Value:yyyy-MM-dd} and CANNOT be sold." & vbCrLf &
                                "Please remove it from the sellable list (notify the Inventory Clerk).",
                                "Expired Product", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' BUSINESS RULE: warn for near-expiry products (within 30 days)
            If prod.IsExpiringSoon(30) Then
                If MessageBox.Show($"'{prod.Name}' is expiring soon ({prod.ExpiryDate.Value:yyyy-MM-dd})." & vbCrLf &
                                   "Add it to the cart anyway?", "Near-Expiry Warning",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If
            End If

            ' BUSINESS RULE: out-of-stock items blocked up-front
            If prod.StockQty <= 0 Then
                MessageBox.Show($"'{prod.Name}' is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim qty As Integer = Convert.ToInt32(numQuantity.Value)

            ' BUSINESS RULE: quantity must be a positive whole number and within stock
            If qty <= 0 Then
                MessageBox.Show("Quantity must be a positive whole number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                _cartService.AddItem(prod, qty)
                RefreshCartGrid()
                numQuantity.Value = 1
                ' Step 3 (presentation only): a successful add closes the results
                ' popup and re-arms the scan box for the next item.
                ShowProductsPopup(False)
                txtPosSearch.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub RefreshCartGrid()
            dgvCart.Rows.Clear()
            For Each itm In _cartService.Items
                dgvCart.Rows.Add(itm.ProductCode, itm.ProductName, itm.UnitPrice.ToString("N2"), itm.Quantity, itm.LineTotal.ToString("N2"))
            Next

            UpdateCheckoutTotals()
            ' RESTYLE (visual only): cart footer summary + empty state
            UpdateCartSummary()
        End Sub

        ''' <summary>
        ''' Updates subtotal, polymorphic discount deduction, net total, and change.
        ''' </summary>
        Private Sub UpdateCheckoutTotals()
            Dim subtotal As Decimal = _cartService.GetSubtotal()
            Dim discountAmt As Decimal = _cartService.GetDiscountAmount()
            Dim netTotal As Decimal = _cartService.GetNetTotal()
            ' Step 5 (Figma): the subtotal row doubles as the unit counter —
            ' "Subtotal · N units" (counting logic in the cart service unchanged).
            Dim units As Integer = _cartService.GetTotalItemCount()
            lblSubtotalTitle.Text = $"Subtotal · {units} {(If(units = 1, "unit", "units"))}"

            lblSubtotal.Text = $"PHP {subtotal:N2}"
            lblDiscountAmount.Text = $"-PHP {discountAmt:N2}"
            lblNetTotal.Text = $"PHP {netTotal:N2}"
            ' QA B1: big totals (e.g. "PHP 12,345.50") must fit the fixed sidebar
            ' width at any display-scale factor — pick the largest font that fits.
            FitTotalFont(lblNetTotal.Text, Math.Max(90, lblNetTotal.Width))

            Dim cash As Decimal = 0D
            If Decimal.TryParse(txtCashReceived.Text.Trim(), cash) Then
                If cash >= netTotal Then
                    Dim change As Decimal = cash - netTotal
                    lblChange.Text = $"PHP {change:N2}"
                    lblChange.ForeColor = Color.FromArgb(30, 94, 58)
                Else
                    lblChange.Text = "Insufficient Cash"
                    lblChange.ForeColor = Color.Red
                End If
            Else
                lblChange.Text = "PHP 0.00"
                lblChange.ForeColor = Color.Black
            End If
        End Sub

        ''' <summary>
        ''' QA B1: shrink-to-fit fonts for the big "Total due" value, largest first.
        ''' ROOT CAUSE: one fixed 28pt font clipped on the right in a 304px sidebar
        ''' under display scaling; the ladder keeps it legible at every width.
        ''' </summary>
        Private Sub FitTotalFont(text As String, availableWidth As Integer)
            Dim chosen As Font = TotalFontLadder(TotalFontLadder.Length - 1)
            For Each f As Font In TotalFontLadder
                Dim w As Integer = TextRenderer.MeasureText(text, f, Size.Empty,
                                    TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix).Width
                If w <= availableWidth Then
                    chosen = f
                    Exit For
                End If
            Next
            If lblNetTotal.Font IsNot chosen Then lblNetTotal.Font = chosen
        End Sub

        ''' <summary>
        ''' Demonstrates POLYMORPHISM: Dynamically updates the discount calculation strategy.
        ''' </summary>
        Private Sub cboDiscount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDiscount.SelectedIndexChanged
            Dim selectedDiscount As Discount = TryCast(cboDiscount.SelectedItem, Discount)
            If selectedDiscount IsNot Nothing Then
                _cartService.CurrentDiscount = selectedDiscount
            Else
                _cartService.CurrentDiscount = New NoDiscount()
            End If
            UpdateCheckoutTotals()
        End Sub

        Private Sub txtCashReceived_TextChanged(sender As Object, e As EventArgs) Handles txtCashReceived.TextChanged
            UpdateCheckoutTotals()
        End Sub

        Private Sub btnIncQty_Click(sender As Object, e As EventArgs) Handles btnIncQty.Click
            If dgvCart.SelectedRows.Count = 0 Then Return
            Dim code As String = Convert.ToString(dgvCart.SelectedRows(0).Cells(0).Value)
            Dim prod As Product = _productRepo.GetByCode(code)
            If prod Is Nothing Then Return

            Dim currentItem = _cartService.Items.FirstOrDefault(Function(i) i.ProductId = prod.ProductId)
            If currentItem IsNot Nothing Then
                Try
                    _cartService.UpdateItemQuantity(prod.ProductId, currentItem.Quantity + 1, prod.StockQty)
                    RefreshCartGrid()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        End Sub

        Private Sub btnDecQty_Click(sender As Object, e As EventArgs) Handles btnDecQty.Click
            If dgvCart.SelectedRows.Count = 0 Then Return
            Dim code As String = Convert.ToString(dgvCart.SelectedRows(0).Cells(0).Value)
            Dim prod As Product = _productRepo.GetByCode(code)
            If prod Is Nothing Then Return

            Dim currentItem = _cartService.Items.FirstOrDefault(Function(i) i.ProductId = prod.ProductId)
            If currentItem IsNot Nothing Then
                _cartService.UpdateItemQuantity(prod.ProductId, currentItem.Quantity - 1, prod.StockQty)
                RefreshCartGrid()
            End If
        End Sub

        Private Sub btnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
            If dgvCart.SelectedRows.Count = 0 Then Return
            Dim code As String = Convert.ToString(dgvCart.SelectedRows(0).Cells(0).Value)
            Dim prod As Product = _productRepo.GetByCode(code)
            If prod IsNot Nothing Then
                _cartService.RemoveItem(prod.ProductId)
                RefreshCartGrid()
            End If
        End Sub

        Private Sub btnClearCart_Click(sender As Object, e As EventArgs) Handles btnClearCart.Click
            If _cartService.Items.Count > 0 Then
                If MessageBox.Show("Are you sure you want to clear all items in the cart?", "Clear Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    _cartService.ClearCart()
                    cboDiscount.SelectedIndex = 0
                    txtCashReceived.Clear()
                    RefreshCartGrid()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Executes POS checkout inside an atomic database TRANSACTION.
        ''' </summary>
        Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
            ' Double-click / re-entry guard: prevent duplicate transactions
            If _isCheckingOut Then Return

            If _cartService.Items.Count = 0 Then
                MessageBox.Show("Cart is empty. Please add products before checking out.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim netTotal As Decimal = _cartService.GetNetTotal()
            Dim cashReceived As Decimal = 0D

            If Not Decimal.TryParse(txtCashReceived.Text.Trim(), cashReceived) Then
                MessageBox.Show("Cash tendered must be a valid number.", "Payment Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCashReceived.Focus()
                Return
            End If

            If cashReceived < 0D Then
                MessageBox.Show("Cash tendered cannot be negative.", "Payment Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCashReceived.Focus()
                Return
            End If

            If cashReceived > ValidationRules.MaxMoney Then
                MessageBox.Show($"Cash tendered is unreasonably large (max PHP {ValidationRules.MaxMoney:N2}).", "Payment Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCashReceived.Focus()
                Return
            End If

            If cashReceived < netTotal Then
                MessageBox.Show($"Cash tendered is insufficient. Total due is PHP {netTotal:N2}.", "Payment Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCashReceived.Focus()
                Return
            End If

            Dim changeDue As Decimal = cashReceived - netTotal

            ' Confirm with Cashier
            Dim promptMsg As String = $"Confirm Checkout:" & vbCrLf &
                                      $"Total Due : PHP {netTotal:N2}" & vbCrLf &
                                      $"Cash Rec. : PHP {cashReceived:N2}" & vbCrLf &
                                      $"Change Due: PHP {changeDue:N2}"

            If MessageBox.Show(promptMsg, "Confirm Sale Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            Try
                _isCheckingOut = True
                btnCheckout.Enabled = False

                Try
                    ' Prepare Sale Model
                    Dim newSale As New Sale() With {
                        .ReceiptNo = _saleRepo.GenerateReceiptNumber(),
                        .SaleDate = DateTime.Now,
                        .CashierId = _cashier.UserId,
                        .CashierName = _cashier.FullName,
                        .Subtotal = _cartService.GetSubtotal(),
                        .DiscountType = _cartService.CurrentDiscount.DiscountName,
                        .DiscountAmount = _cartService.GetDiscountAmount(),
                        .Total = netTotal,
                        .CashReceived = cashReceived,
                        .ChangeAmount = changeDue,
                        .Items = _cartService.Items.ToList()
                    }

                    ' Optional attached customer
                    If cboCustomer.SelectedIndex > 0 Then
                        Dim attachedCust As Customer = TryCast(cboCustomer.SelectedItem, Customer)
                        If attachedCust IsNot Nothing Then
                            newSale.CustomerId = attachedCust.UserId
                            newSale.CustomerName = attachedCust.FullName
                        End If
                    End If

                    ' Save inside atomic database transaction
                    Dim newSaleId As Integer = _saleRepo.RecordSale(newSale, newSale.Items)
                    newSale.SaleId = newSaleId
                    _lastCompletedSale = newSale

                    MessageBox.Show($"Checkout successful! Receipt #{newSale.ReceiptNo} generated." & vbCrLf & $"Change: PHP {changeDue:N2}", "Transaction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Prompt receipt preview / printing
                    Using receiptForm As New ReceiptPreviewForm(newSale)
                        receiptForm.ShowDialog(Me)
                    End Using

                    ' Reset POS state for next customer
                    _cartService.ClearCart()
                    cboDiscount.SelectedIndex = 0
                    txtCashReceived.Clear()
                    cboCustomer.SelectedIndex = 0
                    RefreshCartGrid()
                    LoadPosProducts()
                    LoadTodayTransactions()

                Catch ex As Exception
                    MessageBox.Show($"Failed to process checkout: {ex.Message}", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Finally
                _isCheckingOut = False
                btnCheckout.Enabled = True
            End Try
        End Sub

        Private Sub btnPrintLast_Click(sender As Object, e As EventArgs) Handles btnPrintLast.Click
            If _lastCompletedSale IsNot Nothing Then
                Using receiptForm As New ReceiptPreviewForm(_lastCompletedSale)
                    receiptForm.ShowDialog(Me)
                End Using
            Else
                MessageBox.Show("No transaction has been completed in this session yet.", "No Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub LoadTodayTransactions()
            dgvTodaySales.Rows.Clear()
            Dim sales As List(Of Sale) = _saleRepo.GetSalesByCashierAndDate(_cashier.UserId, DateTime.Today)
            Dim totalRevenue As Decimal = 0D

            For Each s In sales
                dgvTodaySales.Rows.Add(s.ReceiptNo, s.SaleDate.ToString("hh:mm tt"), If(String.IsNullOrEmpty(s.CustomerName), "Walk-In", s.CustomerName), s.Subtotal.ToString("N2"), s.DiscountAmount.ToString("N2"), s.Total.ToString("N2"))
                totalRevenue += s.Total
            Next

            lblTodaySummary.Text = $"Today: {sales.Count} transaction(s) | Total Sales: PHP {totalRevenue:N2}"

            ' RESTYLE (visual only): grid footer + empty state
            UpdateTodayFooter(sales.Count)
        End Sub

        Private Sub btnRefreshTodaySales_Click(sender As Object, e As EventArgs) Handles btnRefreshTodaySales.Click
            LoadTodayTransactions()
        End Sub

        Private Sub btnViewSaleDetails_Click(sender As Object, e As EventArgs) Handles btnViewSaleDetails.Click
            If dgvTodaySales.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a transaction from the list.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim receiptNo As String = Convert.ToString(dgvTodaySales.SelectedRows(0).Cells(0).Value)
            Dim sale As Sale = _saleRepo.GetByReceiptNo(receiptNo)
            If sale IsNot Nothing Then
                Using dlg As New PastReceiptForm(sale)
                    dlg.ShowDialog(Me)
                End Using
            End If
        End Sub

        Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
            If _cartService.Items.Count > 0 Then
                If MessageBox.Show("There are items currently in the active cart. Are you sure you want to log out and discard the cart?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                    Return
                End If
            Else
                If MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If
            End If
            Me.Close()
        End Sub

#Region "Figma UI restyle (visual only - no business behavior changes)"

        ' ================= UI-only state / new helper controls =================
        Private _btnNewSale As Button
        Private _btnEditQty As Button
        Private _btnEditApply As Button
        Private _btnEditCancel As Button
        Private _numEditQty As NumericUpDown
        Private _lblEditProduct As Label
        Private _lblCartSummary As Label
        Private _lblCartEmpty As Label
        Private _lblProductsFooter As Label
        Private _lblProductsEmpty As Label
        Private _productsCard As CardPanel
        Private _productsBody As Panel
        Private _cartCard As CardPanel
        Private _editorRow As Panel
        Private _lblTodayFooter As Label
        Private _lblTodayEmpty As Label
        Private _nutritionCard As CardPanel
        Private _lnkNutrition As LinkLabel
        Private _payTlp As TableLayoutPanel
        Private _payRowIndex As Integer = 0

        ' Step 3 (Figma option b): the products list is now an auto-open search-
        ' results popup — open while the scan box has text, closed by Enter /
        ' Escape, Add to Cart, or a click outside it.
        Private _productsPopupOpen As Boolean = False
        Private _editingProductId As Integer = 0
        Private _posLoadedCount As Integer = 0
        Private _posTotalCount As Integer = 0

        ' ==== QA B1/B2/B3 layout plumbing (visual only) ====
        Private _posLeftColumn As Panel          ' products ⇄ cart rebalance target
        Private _posSidePanel As Panel           ' payment/nutrition column (bounds cap)
        Private _payCard As CardPanel            ' payment summary card (computed height)
        ' STEP 1 (PRIORITY 1): the AutoSize flag is gone — the card height comes
        ' from measured absolute row heights (ComputePayCardNaturalHeight). This
        ' flag only tracks whether _payTlp fills the card or is top-docked for
        ' the short-window scroll cap.
        Private _payFillDock As Boolean = True
        Private ReadOnly _payRowHeights As New List(Of Integer)() ' absolute payment row heights
        Private _discHint As Label               ' STEP 3: discount-type hint (wrap-measured)
        Private _discHintRow As Integer = -1     ' STEP 3: its index in _payRowHeights
        Private _lblNutDesc As Label             ' nutrition preview description (measured)
        Private _lblNutEmpty As Label            ' "No nutrition data" empty-state title
        Private _lblNutIcon As Label             ' empty-state leaf icon
        Private _lblNutName As Label             ' Step 6: selected cart item name
        Private _lblNutSynopsis As Label         ' Step 6: per-100 g values line

        ' QA B1: font ladder for the big Total due value (same weight as Theme.FontBigAmount).
        Private Shared ReadOnly TotalFontLadder As Font() = {
            Theme.F(28.0F, ThemeWeight.Semibold),
            Theme.F(22.0F, ThemeWeight.Semibold),
            Theme.F(18.0F, ThemeWeight.Semibold),
            Theme.F(15.0F, ThemeWeight.Semibold)}

        ''' <summary>Applies the design tokens + POS layout. Visual only.</summary>
        Private Sub InitializeUiRestyle()
            Me.Text = "Mini-Grocery Store · Cashier"
            Me.ClientSize = New Size(1280, 800)
            Me.MinimumSize = New Size(1024, 700)
            Me.KeyPreview = True
            ' QA E1/E4: open maximized (constructor-time, no size flash); restore
            ' → 1280x800, minimum keeps layouts intact, taskbar stays visible.
            Me.WindowState = FormWindowState.Maximized

            AppHeader.Apply(pnlHeader, lblAppTitle, lblCashierInfo, btnLogout)
            ShellTabs.Apply(tabCashier,
                New ShellTabSpec(tabPos, "Point of sale", "scan"),
                New ShellTabSpec(tabSales, "Today's transactions", "receipt"))

            RestylePos()
            RestyleToday()

            ' Step 3: click-outside dismissal for the search-results popup.
            Application.AddMessageFilter(New PosPopupDismissFilter(Me))

            Theme.RefreshLayout(Me)
        End Sub

        ' ================= POS tab =================
        Private Sub RestylePos()
            tabPos.BackColor = Theme.PageBg

            ' Detach the legacy split container (its controls are re-homed below).
            splitPos.Visible = False
            tabPos.Controls.Remove(splitPos)

            ' ---------- Body: left column + widened sidebar ----------
            ' QA B1 root cause: the sidebar was a fixed 304px column while header
            ' fonts keep scaling with Windows display scaling, so payment values/cues
            ' clipped on the right at 125/150%. A 352px column gives the payment
            ' summary room at every allowed window size; total/cash fonts also
            ' shrink-to-fit (see FitTotalFont / UpdatePayCardBounds).
            Dim leftCol As New Panel()
            leftCol.Dock = DockStyle.Fill
            leftCol.BackColor = Theme.PageBg
            leftCol.Margin = New Padding(0)
            leftCol.Padding = New Padding(0)

            Dim sidePanel As New Panel()
            sidePanel.Dock = DockStyle.Fill
            sidePanel.BackColor = Theme.PageBg
            sidePanel.Margin = New Padding(0)
            sidePanel.Padding = New Padding(0)

            Dim body As New TableLayoutPanel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.PageBg
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)
            body.ColumnCount = 2
            body.RowCount = 1
            body.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            body.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 352F))
            body.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            body.Controls.Add(leftCol, 0, 0)
            body.Controls.Add(sidePanel, 1, 0)

            ' Shared layout targets + rebalance hooks (QA B2).
            _posLeftColumn = leftCol
            _posSidePanel = sidePanel
            AddHandler _posLeftColumn.Resize, Sub() LayoutPosColumns()
            AddHandler _posSidePanel.Resize, Sub() UpdatePayCardBounds()

            ' Left column (Steps 2-4, Figma): the cart fills; a slim actions strip
            ' docks BELOW the card; the products list docks on top ONLY as the
            ' auto-open search-results popup (hidden while idle).
            BuildCartCard(leftCol)

            Dim cartActions As New Panel()
            cartActions.Dock = DockStyle.Bottom
            cartActions.Height = 42
            cartActions.BackColor = Theme.PageBg
            cartActions.Margin = New Padding(0)
            BuildCartActions(cartActions)
            leftCol.Controls.Add(cartActions)

            BuildProductsCard(leftCol)

            ' Sidebar: nutrition card (Fill) added first; payment card added LAST → docks on top.
            BuildNutritionCard(sidePanel)
            BuildPaymentCard(sidePanel)

            ' ---------- Scan row ----------
            Dim scanRow As New Panel()
            scanRow.Dock = DockStyle.Top
            scanRow.Height = 46
            scanRow.BackColor = Theme.PageBg
            scanRow.Margin = New Padding(0)
            scanRow.Padding = New Padding(0)
            BuildScanRow(scanRow)

            ' Assembly (reverse dock processing): Fill first, then strips, heading LAST.
            tabPos.Controls.Add(body)
            tabPos.Controls.Add(scanRow)
            Dim heading As PageHeading = PageHeading.Build(tabPos, "Point of sale", _
                "Scan, review and complete a sale. All prices are in Philippine pesos.")
            heading.AddChip(New StatusChip("Register 01 · Open", ThemeTone.Success))

            _btnNewSale = New Button()
            _btnNewSale.Text = "New sale"
            Theme.StyleButton(_btnNewSale, ThemeButtonKind.Secondary)
            _btnNewSale.AutoSize = True
            AddHandler _btnNewSale.Click, AddressOf OnNewSaleClick
            heading.AddAction(_btnNewSale)

            AddHandler dgvCart.SelectionChanged, AddressOf OnCartSelectionChanged

            ' First-pass card sizing; the Resize hooks keep lanes in step afterwards.
            LayoutPosColumns()
            UpdatePayCardBounds()
            ' QA C1: preview text/empty state come from the shared store status.
            RefreshNutritionPreview()
            ' Step 6: the preview follows the current cart selection as well.
            UpdateNutritionForSelectedCartRow()
        End Sub

        Private Sub BuildProductsCard(parent As Panel)
            _productsCard = New CardPanel()
            _productsCard.Dock = DockStyle.Top
            ' Step 3 (Figma option b): the products list is a border-less, auto-open
            ' search-results popup — hidden while idle, shown under the scan box.
            _productsCard.Height = 262
            _productsCard.Visible = False
            _productsCard.Margin = New Padding(0, 0, 12, 10)
            _productsCard.Padding = New Padding(12, 8, 12, 6)

            _productsBody = New Panel()
            _productsBody.Dock = DockStyle.Fill
            _productsBody.BackColor = Theme.White
            _productsBody.Margin = New Padding(0)
            _productsBody.Padding = New Padding(0)

            GridStyler.Apply(dgvPosProducts)
            GridStyler.ApplyWeights(dgvPosProducts, 1.2, 2.6, 1.1, 0.7)
            _productsBody.Controls.Add(dgvPosProducts)
            _lblProductsEmpty = GridStyler.AttachEmptyOverlay(dgvPosProducts, "No products match your search.")

            ' Step 3: keyboard inside the popup — Enter acts on the selected row via
            ' the SAME handler as the green Add to Cart button (business logic kept),
            ' Escape closes it.
            AddHandler dgvPosProducts.KeyDown,
                Sub(s As Object, e As KeyEventArgs)
                    If e.KeyCode = Keys.Enter Then
                        e.Handled = True
                        e.SuppressKeyPress = True
                        btnAddToCart_Click(Me, EventArgs.Empty)
                    ElseIf e.KeyCode = Keys.Escape Then
                        e.Handled = True
                        e.SuppressKeyPress = True
                        ShowProductsPopup(False)
                        txtPosSearch.Focus()
                    End If
                End Sub

            _lblProductsFooter = GridStyler.NewFooter()
            _lblProductsFooter.Height = 24
            _lblProductsFooter.Dock = DockStyle.Bottom
            _lblProductsFooter.BackColor = Theme.White

            ' Popup slim header keeps the category filter (it still filters the
            ' results). The "Products" caption and collapse chevron are retired with
            ' the permanent panel; lblSearch stays referenced-but-hidden (legacy
            ' fragile-controls rule) and the toolbar is gone.
            lblSearch.Visible = False

            Dim popupHeader As New Panel()
            popupHeader.Dock = DockStyle.Top
            popupHeader.Height = 26
            popupHeader.BackColor = Theme.White
            popupHeader.Margin = New Padding(0)
            popupHeader.Padding = New Padding(0)
            Theme.StyleCombo(cboPosCategory)
            popupHeader.Controls.Add(cboPosCategory)
            AddHandler popupHeader.Resize,
                Sub(s As Object, e As EventArgs)
                    ' Step 3: the category filter hugs the popup's right edge —
                    ' replacing the old chevron toolbar (QA B4 legacy).
                    cboPosCategory.Location = New Point(Math.Max(0, popupHeader.ClientSize.Width - 176), 0)
                    cboPosCategory.Width = 176
                End Sub

            _productsCard.Controls.Add(_productsBody)
            _productsCard.Controls.Add(_lblProductsFooter)
            _productsCard.Controls.Add(popupHeader)

            parent.Controls.Add(_productsCard)
        End Sub

        ''' <summary>
        ''' Step 3 (Figma option b): opens/closes the auto-open search-results popup.
        ''' All add-to-cart logic still reads dgvPosProducts.SelectedRows.
        ''' </summary>
        Private Sub ShowProductsPopup(open As Boolean)
            If _productsCard Is Nothing Then Return
            _productsPopupOpen = open
            If open Then
                ' Grid rows + header + footer + paddings, capped so the popup never
                ' runs under the cart actions strip. (No fixed pixel size rule:
                ' heights derive from the themed grid row and card padding.)
                Dim chrome As Integer = _productsCard.Padding.Vertical + 26 + _lblProductsFooter.Height
                Dim natural As Integer = chrome + 4 * Theme.HeightGridRow
                Dim maxH As Integer = Math.Max(180, _posLeftColumn.ClientSize.Height - 170)
                _productsCard.Height = Math.Min(natural, maxH)
                _productsCard.Visible = True
            Else
                _productsCard.Visible = False
            End If
        End Sub

        Private Sub BuildCartCard(parent As Panel)
            _cartCard = New CardPanel()
            _cartCard.Dock = DockStyle.Fill
            _cartCard.Margin = New Padding(0, 0, 12, 0)
            _cartCard.Padding = New Padding(14, 10, 14, 8)

            Dim cartBody As New Panel()
            cartBody.Dock = DockStyle.Fill
            cartBody.BackColor = Theme.White
            cartBody.Margin = New Padding(0)
            cartBody.Padding = New Padding(0)

            GridStyler.Apply(dgvCart)
            ' Step 4 (Figma): headers renamed and Qty ⇄ Unit price swapped via
            ' DisplayIndex so the DATA indexes — used by add-to-cart, the F4 editor
            ' and the +/- buttons (code=0, name=1, price=2, qty=3, total=4) — stay
            ' untouched. Display order becomes: Barcode, Product, Qty, Unit price,
            ' Line total.
            dgvCart.Columns(0).HeaderText = "Barcode"
            dgvCart.Columns(1).HeaderText = "Product"
            dgvCart.Columns(2).HeaderText = "Unit price"
            dgvCart.Columns(3).HeaderText = "Qty"
            dgvCart.Columns(3).DisplayIndex = 2
            GridStyler.ApplyWeights(dgvCart, 1.4, 2.4, 1.1, 0.7, 1.2)
            cartBody.Controls.Add(dgvCart)
            _lblCartEmpty = GridStyler.AttachEmptyOverlay(dgvCart, "Cart is empty — scan a barcode or search for products.")

            _lblCartSummary = GridStyler.NewFooter()
            _lblCartSummary.Height = 22
            _lblCartSummary.Dock = DockStyle.Bottom
            _lblCartSummary.BackColor = Theme.White

            _editorRow = New Panel()
            _editorRow.Dock = DockStyle.Top
            _editorRow.Height = 40
            _editorRow.BackColor = Theme.White
            _editorRow.Margin = New Padding(0)
            _editorRow.Visible = False
            BuildEditorRow(_editorRow)

            Dim cartHeader As New Panel()
            cartHeader.Dock = DockStyle.Top
            cartHeader.Height = 28
            cartHeader.BackColor = Theme.White
            cartHeader.Margin = New Padding(0)
            cartHeader.Padding = New Padding(0)
            Dim lblCartTitle As New Label()
            lblCartTitle.Text = "Cart"
            lblCartTitle.Font = Theme.FontSection
            lblCartTitle.ForeColor = Theme.Ink
            lblCartTitle.AutoSize = True
            lblCartTitle.BackColor = Theme.White
            lblCartTitle.Location = New Point(0, 2)
            cartHeader.Controls.Add(lblCartTitle)

            ' Order (Step 4): body(Fill), summary(Bottom), editor(Top), header(Top-last).
            ' The Edit/Remove/Clear actions strip moved OUT of the card — it docks to
            ' the left column BELOW the cart card (Figma).
            _cartCard.Controls.Add(cartBody)
            _cartCard.Controls.Add(_lblCartSummary)
            _cartCard.Controls.Add(_editorRow)
            _cartCard.Controls.Add(cartHeader)

            parent.Controls.Add(_cartCard)
        End Sub

        Private Sub BuildCartActions(actions As Panel)
            Dim flow As New FlowLayoutPanel()
            flow.Dock = DockStyle.Fill
            flow.FlowDirection = FlowDirection.LeftToRight
            flow.WrapContents = False
            flow.BackColor = Theme.PageBg
            flow.Margin = New Padding(0)
            flow.Padding = New Padding(0)

            btnDecQty.Text = "−"
            Theme.StyleButton(btnDecQty, ThemeButtonKind.Ghost)
            btnDecQty.Size = New Size(30, 30)
            btnDecQty.Margin = New Padding(0, 8, 4, 0)
            flow.Controls.Add(btnDecQty)

            btnIncQty.Text = "+"
            Theme.StyleButton(btnIncQty, ThemeButtonKind.Ghost)
            btnIncQty.Size = New Size(30, 30)
            btnIncQty.Margin = New Padding(0, 8, 16, 0)
            flow.Controls.Add(btnIncQty)

            _btnEditQty = New Button()
            _btnEditQty.Text = "Edit quantity · F4"
            Theme.StyleButton(_btnEditQty, ThemeButtonKind.Secondary)
            _btnEditQty.AutoSize = True
            _btnEditQty.Margin = New Padding(0, 8, 10, 0)
            AddHandler _btnEditQty.Click, AddressOf OnEditQtyClick
            flow.Controls.Add(_btnEditQty)

            btnRemoveItem.Text = "Remove item"
            Theme.StyleButton(btnRemoveItem, ThemeButtonKind.Destructive)
            btnRemoveItem.AutoSize = True
            btnRemoveItem.Margin = New Padding(0, 6, 10, 0)
            flow.Controls.Add(btnRemoveItem)

            btnClearCart.Text = "Clear cart"
            Theme.StyleButton(btnClearCart, ThemeButtonKind.Secondary)
            btnClearCart.AutoSize = True
            btnClearCart.Margin = New Padding(0, 6, 0, 0)
            flow.Controls.Add(btnClearCart)

            actions.Controls.Add(flow)
        End Sub

        Private Sub BuildEditorRow(editRow As Panel)
            Dim flow As New FlowLayoutPanel()
            flow.Dock = DockStyle.Fill
            flow.FlowDirection = FlowDirection.LeftToRight
            flow.WrapContents = False
            flow.BackColor = Theme.White
            flow.Margin = New Padding(0)
            flow.Padding = New Padding(0)

            Dim lblPrompt As New Label()
            lblPrompt.Text = "Edit quantity:"
            lblPrompt.Font = Theme.FontBody12Semi
            lblPrompt.ForeColor = Theme.Ink
            lblPrompt.AutoSize = True
            lblPrompt.Margin = New Padding(0, 9, 6, 0)
            flow.Controls.Add(lblPrompt)

            _lblEditProduct = New Label()
            _lblEditProduct.Text = String.Empty
            _lblEditProduct.Font = Theme.FontBody12
            _lblEditProduct.ForeColor = Theme.Muted
            _lblEditProduct.AutoSize = True
            _lblEditProduct.Margin = New Padding(0, 9, 12, 0)
            flow.Controls.Add(_lblEditProduct)

            _numEditQty = New NumericUpDown()
            Theme.StyleNum(_numEditQty)
            _numEditQty.Minimum = 0
            _numEditQty.Maximum = 9999
            _numEditQty.Width = 60
            _numEditQty.Margin = New Padding(0, 3, 10, 0)
            AddHandler _numEditQty.KeyDown,
                Sub(s As Object, e As KeyEventArgs)
                    If e.KeyCode = Keys.Enter Then
                        e.Handled = True
                        e.SuppressKeyPress = True
                        OnEditApplyClick(Nothing, EventArgs.Empty)
                    ElseIf e.KeyCode = Keys.Escape Then
                        e.Handled = True
                        e.SuppressKeyPress = True
                        CancelEditor()
                    End If
                End Sub
            flow.Controls.Add(_numEditQty)

            _btnEditApply = New Button()
            _btnEditApply.Text = "Apply"
            Theme.StyleButton(_btnEditApply, ThemeButtonKind.Primary)
            _btnEditApply.AutoSize = True
            _btnEditApply.Margin = New Padding(0, 5, 6, 0)
            AddHandler _btnEditApply.Click, AddressOf OnEditApplyClick
            flow.Controls.Add(_btnEditApply)

            _btnEditCancel = New Button()
            _btnEditCancel.Text = "Cancel"
            Theme.StyleButton(_btnEditCancel, ThemeButtonKind.Ghost)
            _btnEditCancel.AutoSize = True
            _btnEditCancel.Margin = New Padding(0, 5, 0, 0)
            AddHandler _btnEditCancel.Click, AddressOf OnEditCancelClick
            flow.Controls.Add(_btnEditCancel)

            editRow.Controls.Add(flow)
        End Sub

        Private Sub BuildScanRow(row As Panel)
            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Fill
            tlp.BackColor = Theme.PageBg
            tlp.Margin = New Padding(0)
            tlp.Padding = New Padding(0)
            tlp.ColumnCount = 4
            tlp.RowCount = 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 262F))
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            ' Scan/search input (existing txtPosSearch)
            Dim inputWrap As New InputPanel()
            inputWrap.Dock = DockStyle.Fill
            inputWrap.Margin = New Padding(0, 6, 12, 6)
            Theme.StyleInput(txtPosSearch)
            Theme.SetCue(txtPosSearch, "Scan barcode or search product · F2")
            inputWrap.Controls.Add(txtPosSearch)
            inputWrap.Bind(txtPosSearch)
            AddHandler inputWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    txtPosSearch.Location = New Point(12, Math.Max(0, (inputWrap.ClientSize.Height - txtPosSearch.Height) \ 2))
                    txtPosSearch.Width = inputWrap.ClientSize.Width - 24
                End Sub
            ' Step 3: Enter in the scan box adds the selected popup result (same
            ' handler as the Add to Cart button); Escape closes the popup. F2 focus
            ' comes from the global hotkey handler below.
            AddHandler txtPosSearch.KeyDown,
                Sub(s As Object, e As KeyEventArgs)
                    If e.KeyCode = Keys.Enter Then
                        e.Handled = True
                        e.SuppressKeyPress = True
                        btnAddToCart_Click(Me, EventArgs.Empty)
                    ElseIf e.KeyCode = Keys.Escape Then
                        e.Handled = True
                        e.SuppressKeyPress = True
                        ShowProductsPopup(False)
                    End If
                End Sub
            AddHandler txtPosSearch.GotFocus,
                Sub(s As Object, e As EventArgs)
                    If txtPosSearch.Text.Trim().Length > 0 Then ShowProductsPopup(True)
                End Sub
            tlp.Controls.Add(inputWrap, 0, 0)

            ' Quantity spinner cluster (existing lblQty + numQuantity)
            Dim qtyCluster As New Panel()
            qtyCluster.Width = 98
            qtyCluster.Height = 34
            qtyCluster.BackColor = Theme.PageBg
            qtyCluster.Margin = New Padding(0, 6, 10, 6)
            lblQty.Text = "Qty"
            lblQty.Font = Theme.FontSmall
            lblQty.ForeColor = Theme.Muted
            lblQty.AutoSize = True
            lblQty.BackColor = Theme.PageBg
            lblQty.Location = New Point(2, 10)
            qtyCluster.Controls.Add(lblQty)
            Theme.StyleNum(numQuantity)
            numQuantity.Width = 58
            numQuantity.Location = New Point(36, 2)
            qtyCluster.Controls.Add(numQuantity)
            tlp.Controls.Add(qtyCluster, 1, 0)

            ' Add to cart (existing)
            btnAddToCart.Text = "Add to Cart"
            Theme.StyleButton(btnAddToCart, ThemeButtonKind.Primary)
            btnAddToCart.AutoSize = True
            btnAddToCart.Padding = New Padding(14, 0, 14, 0)
            btnAddToCart.Margin = New Padding(0, 6, 12, 6)
            tlp.Controls.Add(btnAddToCart, 2, 0)

            ' Customer field 260px (existing cboCustomer; lblAttachCust as inner caption)
            Dim custWrap As New InputPanel()
            custWrap.Dock = DockStyle.Fill
            custWrap.Margin = New Padding(0, 6, 0, 6)
            custWrap.BackColor = Theme.White
            lblAttachCust.Text = "Customer"
            lblAttachCust.Font = Theme.FontSmall
            lblAttachCust.ForeColor = Theme.Muted
            lblAttachCust.AutoSize = True
            lblAttachCust.BackColor = Theme.White
            custWrap.Controls.Add(lblAttachCust)
            Theme.StyleCombo(cboCustomer)
            custWrap.Controls.Add(cboCustomer)
            AddHandler custWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    Dim w As Integer = custWrap.ClientSize.Width
                    Dim hh As Integer = custWrap.ClientSize.Height
                    lblAttachCust.Location = New Point(10, Math.Max(0, (hh - lblAttachCust.Height) \ 2))
                    cboCustomer.Location = New Point(10 + lblAttachCust.Width + 6, Math.Max(0, (hh - cboCustomer.Height) \ 2))
                    cboCustomer.Width = Math.Max(80, w - cboCustomer.Left - 8)
                End Sub
            tlp.Controls.Add(custWrap, 3, 0)

            row.Controls.Add(tlp)
        End Sub

        ' ================= Sidebar: nutrition preview (empty state until OFF lands) =================
        Private Sub BuildNutritionCard(sidePanel As Panel)
            _nutritionCard = New CardPanel()
            _nutritionCard.Dock = DockStyle.Fill
            _nutritionCard.Margin = New Padding(0)
            _nutritionCard.Padding = New Padding(16, 12, 16, 10)
            _nutritionCard.BackColor = Theme.White
            _nutritionCard.AutoScroll = True

            Dim filler As New Panel()
            filler.Dock = DockStyle.Fill
            filler.BackColor = Theme.White
            filler.Margin = New Padding(0)

            Dim lblNutDesc As New Label()
            lblNutDesc.Text = "Nutrition values will appear here once products are linked in the Owner's Nutrition catalog."
            lblNutDesc.Font = Theme.FontBody12
            lblNutDesc.ForeColor = Theme.Muted
            lblNutDesc.TextAlign = ContentAlignment.TopCenter
            lblNutDesc.Dock = DockStyle.Top
            lblNutDesc.Height = 44
            lblNutDesc.Margin = New Padding(0)
            lblNutDesc.BackColor = Theme.White
            ' QA B3 root cause: the description label had a fixed 44px height while
            ' fonts scale with Windows display scaling, so the wrapped sentence was
            ' cut mid-line. Height is now measured from the real text each time the
            ' card resizes (UpdateNutritionLayout); the text itself becomes
            ' data-aware with the NutritionStore status work.
            _lblNutDesc = lblNutDesc
            AddHandler _nutritionCard.Resize, Sub() UpdateNutritionLayout()

            Dim lblNutEmptyTitle As New Label()
            lblNutEmptyTitle.Text = "No nutrition data"
            lblNutEmptyTitle.Font = Theme.FontBody13Semi
            lblNutEmptyTitle.ForeColor = Theme.Ink
            lblNutEmptyTitle.TextAlign = ContentAlignment.MiddleCenter
            lblNutEmptyTitle.Dock = DockStyle.Top
            lblNutEmptyTitle.Height = 20
            lblNutEmptyTitle.Margin = New Padding(0)
            lblNutEmptyTitle.BackColor = Theme.White
            _lblNutEmpty = lblNutEmptyTitle ' QA C1: hidden/reshown by RefreshNutritionPreview

            Dim lblNutIcon As New Label()
            lblNutIcon.Image = IconArt.Get("leaf", Theme.Muted, 28)
            lblNutIcon.ImageAlign = ContentAlignment.MiddleCenter
            lblNutIcon.Dock = DockStyle.Top
            lblNutIcon.Height = 36
            lblNutIcon.Margin = New Padding(0)
            lblNutIcon.BackColor = Theme.White
            _lblNutIcon = lblNutIcon ' QA C1: hidden/reshown by RefreshNutritionPreview

            Dim lblNutTitle As New Label()
            lblNutTitle.Text = "Nutrition preview"
            lblNutTitle.Font = Theme.FontCardTitle
            lblNutTitle.ForeColor = Theme.Ink
            lblNutTitle.TextAlign = ContentAlignment.MiddleLeft
            lblNutTitle.Dock = DockStyle.Top
            ' QA B3: fixed 24px clipped the 16pt title at higher display scaling;
            ' measure the row from the real font instead.
            lblNutTitle.Height = Theme.FontCardTitle.Height + 6
            lblNutTitle.Margin = New Padding(0)
            lblNutTitle.BackColor = Theme.White

            ' Step 6 (Figma): selected cart item + per-100 g synopsis — filled from
            ' NutritionStore, the same source the Owner's Nutrition catalog reads.
            Dim lblNutName As New Label()
            lblNutName.Text = String.Empty
            lblNutName.Font = Theme.FontBody13Semi
            lblNutName.ForeColor = Theme.Ink
            lblNutName.AutoSize = False
            lblNutName.AutoEllipsis = True
            lblNutName.TextAlign = ContentAlignment.MiddleLeft
            lblNutName.Dock = DockStyle.Top
            lblNutName.Height = RowFontHeight(Theme.FontBody13Semi) + 6
            lblNutName.Margin = New Padding(0)
            lblNutName.BackColor = Theme.White
            _lblNutName = lblNutName

            Dim lblNutSynopsis As New Label()
            lblNutSynopsis.Text = String.Empty
            lblNutSynopsis.Font = Theme.FontSmall
            lblNutSynopsis.ForeColor = Theme.Muted
            lblNutSynopsis.AutoSize = False
            lblNutSynopsis.AutoEllipsis = True
            lblNutSynopsis.TextAlign = ContentAlignment.MiddleLeft
            lblNutSynopsis.Dock = DockStyle.Top
            lblNutSynopsis.Height = RowFontHeight(Theme.FontSmall) + 4
            lblNutSynopsis.Margin = New Padding(0)
            lblNutSynopsis.BackColor = Theme.White
            _lblNutSynopsis = lblNutSynopsis

            _lnkNutrition = New LinkLabel()
            _lnkNutrition.Text = "View full nutrition details ›"
            _lnkNutrition.AutoSize = False
            Theme.StyleLink(_lnkNutrition)
            _lnkNutrition.TextAlign = ContentAlignment.MiddleCenter
            _lnkNutrition.Dock = DockStyle.Bottom
            _lnkNutrition.Height = 24
            _lnkNutrition.Margin = New Padding(0)
            _lnkNutrition.LinkBehavior = LinkBehavior.HoverUnderline
            AddHandler _lnkNutrition.LinkClicked, AddressOf OnNutritionLinkClick

            ' Docking order (reverse processing): title→top1, name→top2, synopsis→top3,
            ' icon→top4, empty→top5, desc→top6, filler, link→bottom
            _nutritionCard.Controls.Add(filler)
            _nutritionCard.Controls.Add(lblNutDesc)
            _nutritionCard.Controls.Add(lblNutEmptyTitle)
            _nutritionCard.Controls.Add(lblNutIcon)
            _nutritionCard.Controls.Add(lblNutTitle)
            _nutritionCard.Controls.Add(lblNutName)
            _nutritionCard.Controls.Add(lblNutSynopsis)
            _nutritionCard.Controls.Add(_lnkNutrition)

            sidePanel.Controls.Add(_nutritionCard)
        End Sub

        ' ================= Sidebar: payment summary card =================
        Private Sub BuildPaymentCard(sidePanel As Panel)
            Dim card As New CardPanel()
            card.Dock = DockStyle.Top
            ' STEP 1 (PRIORITY 1 root cause of the vanished panel): AutoSize +
            ' GrowAndShrink on a panel whose ONLY child is a Dock=Fill
            ' TableLayoutPanel cannot be measured correctly in WinForms — the
            ' fill-docked child is proposed the parent's CURRENT size, so the card
            ' collapsed to its ~24px paddings and the old "restore" read that same
            ' degenerate preferred size and re-asserted the collapse on every pass.
            ' The AutoSize engine is removed: the height is computed from real font
            ' metrics (see ComputePayCardNaturalHeight / UpdatePayCardBounds).
            card.Height = 356 ' placeholder until the rows below are built
            card.Margin = New Padding(0)
            card.Padding = New Padding(16, 12, 16, 12)
            _payCard = card
            _payRowHeights.Clear()

            _payTlp = New TableLayoutPanel()
            _payTlp.Dock = DockStyle.Fill
            _payTlp.BackColor = Theme.White
            _payTlp.ColumnCount = 1
            _payTlp.RowCount = 10
            _payTlp.Margin = New Padding(0)
            _payTlp.Padding = New Padding(0)
            ' STEP 1: rows use ABSOLUTE styles pushed by addRow below (auto-size
            ' rows + fill-docked children make the TLP's preferred size ambiguous,
            ' which is the same degeneracy that hid the whole card).
            ' STEP 2 ("Payment summary is cut off" root cause): with ColumnCount = 1
            ' and no ColumnStyle the lone column defaults to AutoSize, so it widened
            ' to the widest child (the unwrapped hint label ≈ 435px at 125% display
            ' scaling) and every Dock=Fill row became 435px wide — 115px wider than
            ' the card itself. Right-aligned values ("PHP …", Change, Amount
            ' received, the discount picker) overflowed past the card edge and were
            ' clipped. The lone column must FILL the card's inner width instead.
            _payTlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))

            Dim addRow As Action(Of Control, Integer) = Sub(c As Control, absH As Integer)
                                                            Dim h As Integer = Math.Max(13, absH)
                                                            _payTlp.Controls.Add(c, 0, _payRowIndex)
                                                            _payTlp.RowStyles.Add(New RowStyle(SizeType.Absolute, h))
                                                            _payRowHeights.Add(h)
                                                            _payRowIndex += 1
                                                        End Sub

            _payRowIndex = 0

            ' 0 - card title
            Dim lblPayTitle As New Label()
            lblPayTitle.Text = "Payment summary"
            lblPayTitle.Font = Theme.FontCardTitle
            lblPayTitle.ForeColor = Theme.Ink
            lblPayTitle.AutoSize = True
            lblPayTitle.Margin = New Padding(0, 0, 0, 6)
            addRow(lblPayTitle, RowFontHeight(Theme.FontCardTitle) + 6)

            ' 1 - subtotal
            lblSubtotalTitle.Text = "Subtotal"
            lblSubtotalTitle.Font = Theme.FontBody12
            lblSubtotalTitle.ForeColor = Theme.Muted
            lblSubtotalTitle.AutoSize = True
            lblSubtotalTitle.BackColor = Color.Transparent
            lblSubtotal.Font = Theme.FontBody13Semi
            lblSubtotal.ForeColor = Theme.Ink
            lblSubtotal.TextAlign = ContentAlignment.MiddleRight
            lblSubtotal.AutoSize = False
            lblSubtotal.Dock = DockStyle.Fill
            lblSubtotal.BackColor = Color.Transparent
            addRow(MakePair(lblSubtotalTitle, lblSubtotal, RowFontHeight(Theme.FontBody13Semi)),
                   RowFontHeight(Theme.FontBody13Semi) + 2)

            ' 2 - discount (existing cboDiscount feeds the amount line)
            lblDiscountTitle.Text = "Discount"
            lblDiscountTitle.Font = Theme.FontBody12
            lblDiscountTitle.ForeColor = Theme.Muted
            lblDiscountTitle.AutoSize = True
            lblDiscountTitle.BackColor = Color.Transparent

            Dim discFlow As New FlowLayoutPanel()
            discFlow.FlowDirection = FlowDirection.RightToLeft
            discFlow.WrapContents = False
            discFlow.Dock = DockStyle.Fill
            discFlow.Height = 30
            discFlow.BackColor = Color.Transparent
            discFlow.Margin = New Padding(0)

            lblDiscountAmount.Font = Theme.FontBody13Semi
            lblDiscountAmount.ForeColor = Theme.Muted
            lblDiscountAmount.AutoSize = True
            lblDiscountAmount.Margin = New Padding(8, 8, 0, 0)
            lblDiscountAmount.TextAlign = ContentAlignment.MiddleRight
            discFlow.Controls.Add(lblDiscountAmount)

            Dim discWrap As New InputPanel()
            discWrap.Size = New Size(124, 30)
            discWrap.Margin = New Padding(0)
            Theme.StyleCombo(cboDiscount)
            discWrap.Controls.Add(cboDiscount)
            AddHandler discWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    cboDiscount.Location = New Point(8, Math.Max(0, (discWrap.ClientSize.Height - cboDiscount.Height) \ 2))
                    cboDiscount.Width = discWrap.ClientSize.Width - 16
                End Sub
            discFlow.Controls.Add(discWrap)

            addRow(MakePair(lblDiscountTitle, discFlow, 32), 34)

            ' 2b - hint (QA B5): this combo is a discount-strategy picker built from
            ' polymorphic Discount classes, not a free-amount field. The hint makes
            ' the picker's meaning and effect visible without changing behavior.
            Dim discHint As New Label()
            discHint.Text = "Discount type — applies to the subtotal; amount updates below."
            discHint.Font = Theme.FontSmall
            discHint.ForeColor = Theme.Muted
            ' STEP 3 (QA B5 follow-up): with AutoSize this label was the TLP's
            ' widest child and blew every payment row past the card's right edge
            ' (they clipped there). Keep the full sentence but let it WRAP, its
            ' row measured against the real sidebar width (QA B3 pattern) so it
            ' is never cut mid-word at any display-scaling factor. Dock=Fill is
            ' required — a non-docked child keeps its default ~100px width inside
            ' the TLP cell and would wrap far too early and clip mid-line.
            discHint.AutoSize = False
            discHint.Dock = DockStyle.Fill
            discHint.TextAlign = ContentAlignment.MiddleLeft
            discHint.BackColor = Color.Transparent
            discHint.Margin = New Padding(0, 2, 0, 0)
            _discHint = discHint
            _discHintRow = _payRowIndex
            addRow(discHint, RowFontHeight(Theme.FontSmall) + 2)

            ' 3 - divider
            Dim divider As New Label()
            divider.Height = 1
            divider.BackColor = Theme.BorderLine
            divider.Dock = DockStyle.Fill
            ' 6px breathing room above/below keeps the 1px line inside the 13px row
            ' (STEP 1: divider row is absolute, like every other row).
            divider.Margin = New Padding(0, 6, 0, 6)
            divider.AutoSize = False
            addRow(divider, 13)

            ' 4 - total due (28 semibold green)
            lblNetTotalTitle.Text = "Total due"
            lblNetTotalTitle.Font = Theme.FontBody13
            lblNetTotalTitle.ForeColor = Theme.Muted
            lblNetTotalTitle.AutoSize = True
            lblNetTotalTitle.BackColor = Color.Transparent
            lblNetTotalTitle.TextAlign = ContentAlignment.MiddleLeft
            lblNetTotalTitle.Dock = DockStyle.Fill

            lblNetTotal.Font = Theme.FontBigAmount
            lblNetTotal.ForeColor = Theme.Green
            lblNetTotal.TextAlign = ContentAlignment.MiddleRight
            lblNetTotal.AutoSize = False
            lblNetTotal.Dock = DockStyle.Fill
            lblNetTotal.BackColor = Color.Transparent
            addRow(MakePair(lblNetTotalTitle, lblNetTotal, RowFontHeight(Theme.FontBigAmount)),
                   RowFontHeight(Theme.FontBigAmount) + 2)

            ' 5 (Step 5/Figma) - side-by-side "Payment method · Cash" (static box,
            ' presentation-only: only cash exists in the sale flow) and "Amount
            ' received" (existing txtCashReceived — behavior untouched). The legacy
            ' lblCashTitle row is retired; its reference stays (hidden).
            lblCashTitle.Visible = False

            Dim payBoxes As New TableLayoutPanel()
            payBoxes.Dock = DockStyle.Fill
            payBoxes.BackColor = Theme.White
            payBoxes.ColumnCount = 2
            payBoxes.RowCount = 1
            payBoxes.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
            payBoxes.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
            payBoxes.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            payBoxes.Margin = New Padding(0)
            payBoxes.Padding = New Padding(0)

            ' Left box: Payment method (Cash display).
            Dim payMethodStack As New Panel()
            payMethodStack.Dock = DockStyle.Fill
            payMethodStack.BackColor = Theme.White
            payMethodStack.Margin = New Padding(0, 0, 6, 0)
            Dim lblPayMethodCaption As New Label()
            lblPayMethodCaption.Text = "Payment method"
            lblPayMethodCaption.Font = Theme.FontSmall
            lblPayMethodCaption.ForeColor = Theme.Muted
            lblPayMethodCaption.AutoSize = False
            lblPayMethodCaption.TextAlign = ContentAlignment.MiddleLeft
            lblPayMethodCaption.Dock = DockStyle.Top
            ' QA B3 pattern: a fixed 16px box shaves glyph bottoms at 125/150%
            ' display scaling — measure the caption from its real font.
            lblPayMethodCaption.Height = RowFontHeight(Theme.FontSmall)
            lblPayMethodCaption.Margin = New Padding(0)
            lblPayMethodCaption.BackColor = Theme.White
            Dim payMethodWrap As New InputPanel()
            payMethodWrap.Dock = DockStyle.Top
            payMethodWrap.Height = 34
            payMethodWrap.Margin = New Padding(0, 2, 0, 0)
            Dim lblPayMethod As New Label()
            lblPayMethod.Text = "Cash"
            lblPayMethod.Font = Theme.FontBody13Semi
            lblPayMethod.ForeColor = Theme.Ink
            lblPayMethod.TextAlign = ContentAlignment.MiddleLeft
            lblPayMethod.BackColor = Theme.White
            payMethodWrap.Controls.Add(lblPayMethod)
            AddHandler payMethodWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    lblPayMethod.Location = New Point(12, Math.Max(0, (payMethodWrap.ClientSize.Height - lblPayMethod.Height) \ 2))
                    lblPayMethod.Width = payMethodWrap.ClientSize.Width - 24
                End Sub
            payMethodStack.Controls.Add(payMethodWrap)
            payMethodStack.Controls.Add(lblPayMethodCaption)
            payBoxes.Controls.Add(payMethodStack, 0, 0)

            ' Right box: Amount received (input logic untouched).
            Dim cashStack As New Panel()
            cashStack.Dock = DockStyle.Fill
            cashStack.BackColor = Theme.White
            cashStack.Margin = New Padding(6, 0, 0, 0)
            Dim lblCashCaption As New Label()
            lblCashCaption.Text = "Amount received"
            lblCashCaption.Font = Theme.FontSmall
            lblCashCaption.ForeColor = Theme.Muted
            lblCashCaption.AutoSize = False
            lblCashCaption.TextAlign = ContentAlignment.MiddleLeft
            lblCashCaption.Dock = DockStyle.Top
            ' QA B3 pattern: same fixed-16px vertical clipping as above.
            lblCashCaption.Height = RowFontHeight(Theme.FontSmall)
            lblCashCaption.Margin = New Padding(0)
            lblCashCaption.BackColor = Theme.White
            Dim cashWrap As New InputPanel()
            cashWrap.Dock = DockStyle.Top
            cashWrap.Height = 34
            cashWrap.Margin = New Padding(0, 2, 0, 0)
            Theme.StyleInput(txtCashReceived)
            txtCashReceived.Font = Theme.FontBody13
            cashWrap.Controls.Add(txtCashReceived)
            cashWrap.Bind(txtCashReceived, "0.00")
            AddHandler cashWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    txtCashReceived.Location = New Point(12, Math.Max(0, (cashWrap.ClientSize.Height - txtCashReceived.Height) \ 2))
                    txtCashReceived.Width = cashWrap.ClientSize.Width - 24
                End Sub
            cashStack.Controls.Add(cashWrap)
            cashStack.Controls.Add(lblCashCaption)
            payBoxes.Controls.Add(cashStack, 1, 0)

            addRow(payBoxes, RowFontHeight(Theme.FontSmall) + 2 + 34)

            ' 7 - change
            lblChangeTitle.Text = "Change"
            lblChangeTitle.Font = Theme.FontBody12
            lblChangeTitle.ForeColor = Theme.Muted
            lblChangeTitle.AutoSize = True
            lblChangeTitle.BackColor = Color.Transparent
            lblChange.Font = Theme.FontBody13Semi
            lblChange.TextAlign = ContentAlignment.MiddleRight
            lblChange.AutoSize = False
            lblChange.Dock = DockStyle.Fill
            lblChange.BackColor = Color.Transparent
            addRow(MakePair(lblChangeTitle, lblChange, RowFontHeight(Theme.FontBody13Semi)),
                   RowFontHeight(Theme.FontBody13Semi) + 2)

            ' 8 - complete sale (existing btnCheckout)
            btnCheckout.Text = "Complete Sale · F12"
            Theme.StyleButton(btnCheckout, ThemeButtonKind.Primary)
            btnCheckout.Dock = DockStyle.Fill
            btnCheckout.Margin = New Padding(0, 14, 0, 0)
            btnCheckout.Height = 34
            addRow(btnCheckout, 56)

            ' 9 - reprint (existing btnPrintLast)
            ' Step 5 (Figma): Reprint stays (approved deviation) as a quiet ghost.
            btnPrintLast.Text = "Reprint last receipt"
            Theme.StyleButton(btnPrintLast, ThemeButtonKind.Ghost)
            btnPrintLast.Dock = DockStyle.Fill
            btnPrintLast.Margin = New Padding(0, 10, 0, 0)
            btnPrintLast.Height = 34
            addRow(btnPrintLast, 52)

            card.Controls.Add(_payTlp)
            sidePanel.Controls.Add(card)

            ' STEP 1: replace the placeholder with the computed natural height; the
            ' sidebar Resize handler keeps the (rare) scroll cap in step afterwards.
            card.Height = ComputePayCardNaturalHeight()
            UpdatePayCardBounds()
        End Sub

        ''' <summary>Font-metric line height (px) used for payment row sizing (STEP 1).</summary>
        Private Shared Function RowFontHeight(f As Font) As Integer
            Return TextRenderer.MeasureText("Ágy", f, Size.Empty,
                                            TextFormatFlags.SingleLine Or TextFormatFlags.NoPadding).Height
        End Function

        ''' <summary>
        ''' STEP 1 (PRIORITY 1 root-cause fix): natural height of the payment card =
        ''' vertical card padding + the sum of the measured, ABSOLUTE row heights.
        ''' Root cause of the vanished panel: AutoSize+GrowAndShrink on the card with
        ''' a Dock=Fill child collapsed measurement to ~24px and neither the layout
        ''' nor the old restore pass ever saw the real content height again.
        ''' </summary>
        Private Function ComputePayCardNaturalHeight() As Integer
            If _payCard Is Nothing OrElse _payRowHeights Is Nothing Then Return 0
            Dim rows As Integer = 0
            For Each h As Integer In _payRowHeights
                rows += h
            Next
            Return _payCard.Padding.Vertical + rows
        End Function

        ''' <summary>
        ''' STEP 3 ("cut off" fix, QA B3 pattern): re-measures the wrapped
        ''' discount-type hint against the payment card's REAL inner width (only
        ''' known after the sidebar's fixed 352px column is laid out) and keeps the
        ''' absolute RowStyle and _payRowHeights in sync, so ComputePayCardNatural-
        ''' Height accounts for the wrapped lines and the row is never clipped.
        ''' </summary>
        Private Sub ApplyDiscHintRowHeight()
            If _discHint Is Nothing OrElse _discHintRow < 0 Then Return
            If _payRowHeights Is Nothing OrElse _discHintRow >= _payRowHeights.Count Then Return
            Dim sidePad As Integer = If(_posSidePanel IsNot Nothing, _posSidePanel.Padding.Horizontal, 0)
            Dim innerW As Integer = If(_posSidePanel IsNot Nothing, _posSidePanel.ClientSize.Width, 0) _
                                     - sidePad - _payCard.Padding.Horizontal
            If innerW < 120 Then Return ' not laid out yet — keep the single-line estimate
            Dim h As Integer = Math.Max(RowFontHeight(Theme.FontSmall) + 2,
                                        WrappedHeight(_discHint.Text, Theme.FontSmall, innerW,
                                                      RowFontHeight(Theme.FontSmall)) + 2)
            If CInt(_payRowHeights(_discHintRow)) <> h Then
                _payRowHeights(_discHintRow) = h
                If _discHintRow < _payTlp.RowStyles.Count Then
                    _payTlp.RowStyles(_discHintRow) = New RowStyle(SizeType.Absolute, h)
                End If
                _discHint.Height = h
            End If
        End Sub

        ''' <summary>
        ''' STEP 1: sizes the payment card to the computed natural height, capping it
        ''' with AutoScroll only when the sidebar is genuinely too short. When the
        ''' cap engages, _payTlp is switched to top-docking with an explicit height
        ''' so the content overflows the card — a Fill-docked child can never scroll
        ''' (it always matches the visible area), which is why the cap branch must
        ''' change the dock mode instead of only toggling AutoScroll.
        ''' </summary>
        Private Sub UpdatePayCardBounds()
            If _posSidePanel Is Nothing OrElse _payCard Is Nothing OrElse _payTlp Is Nothing Then Return

            ' STEP 3: the hint row depends on the measured inner width — bring it
            ' up to date BEFORE the natural height is summed.
            ApplyDiscHintRowHeight()

            Dim natural As Integer = ComputePayCardNaturalHeight()
            Dim cap As Integer = Math.Max(240, _posSidePanel.ClientSize.Height - 100)

            If natural > cap Then
                ' Too tall: cap the card and top-dock the rows so AutoScroll works.
                If _payFillDock Then
                    _payFillDock = False
                    _payTlp.Dock = DockStyle.Top
                    _payTlp.Height = natural - _payCard.Padding.Vertical
                End If
                _payCard.AutoScroll = True
                _payCard.Height = cap
            ElseIf natural > 0 Then
                If Not _payFillDock Then
                    _payFillDock = True
                    _payTlp.Dock = DockStyle.Fill
                End If
                _payCard.AutoScroll = False
                _payCard.Height = natural
            End If

            ' Keep the big total fitting whenever the sidebar's width can change.
            FitTotalFont(lblNetTotal.Text, Math.Max(90, lblNetTotal.Width))
        End Sub

        ''' <summary>
        ''' Snaps the cart card's grid area to whole grid rows so no row is ever
        ''' half-cut (QA B2 engine, Step 4). The products list is now the auto-open
        ''' search-results popup, so the old 50/50 products share is gone; the cart
        ''' simply fills the idle column (Figma).
        ''' </summary>
        Private Sub LayoutPosColumns()
            If _posLeftColumn Is Nothing OrElse _cartCard Is Nothing OrElse _editorRow Is Nothing Then Return

            Const rowH As Integer = 40 ' Theme.HeightGridRow

            ' ---- cart snapping (Dock=Fill card; leftover space becomes bottom padding) ----
            ' Anchored to the 8px design bottom padding so successive layout passes
            ' cannot oscillate. Chrome: header 28 + footer 22 (the Edit/Remove/Clear
            ' strip is now OUTSIDE the card) + the temporary F4 editor strip.
            Dim editorH As Integer = If(_editorRow.Visible, _editorRow.Height, 0)
            Dim cartChromeBase As Integer = _cartCard.Padding.Top + 8 + 28 + 22 + editorH
            Dim cartGridBase As Integer = _cartCard.ClientSize.Height - cartChromeBase
            If cartGridBase > rowH Then
                Dim remainder As Integer = cartGridBase Mod rowH
                Dim newBottom As Integer = 8 + remainder
                If newBottom <> _cartCard.Padding.Bottom Then
                    _cartCard.Padding = New Padding(14, 10, 14, newBottom)
                End If
            End If
        End Sub

        ''' <summary>
        ''' QA B3: measures and applies the wrapped height of the nutrition preview
        ''' description so the sentence is never cut mid-line at any width or
        ''' display-scale factor.
        ''' </summary>
        Private Sub UpdateNutritionLayout()
            If _lblNutDesc Is Nothing OrElse _nutritionCard Is Nothing Then Return
            Dim availW As Integer = _nutritionCard.ClientSize.Width - _nutritionCard.Padding.Horizontal
            _lblNutDesc.Height = WrappedHeight(_lblNutDesc.Text, Theme.FontBody12,
                                               Math.Max(120, availW), 24)
        End Sub

        ''' <summary>
        ''' QA C1: the Cashier nutrition preview must reflect the real store state.
        ''' Reads NutritionStore.GetStatus() — the same source the Owner screen and
        ''' its banner/grid use, so the two portals can never disagree.
        ''' </summary>
        Private Sub RefreshNutritionPreview()
            If _lblNutDesc Is Nothing Then Return
            Dim st As NutritionStatus = NutritionStore.GetStatus()
            If st.IsImported Then
                Dim stamp As String = If(st.LastImportAt > DateTime.MinValue,
                                         $" · imported {st.LastImportAt:HH:mm}", "")
                _lblNutDesc.Text = $"Per 100 g / 100 ml values for {st.RecordsFound} products — " &
                                   $"{st.RecordsVerified} verified, {st.RecordsNeedReview} to review" & stamp & "."
                If _lblNutEmpty IsNot Nothing Then _lblNutEmpty.Visible = False
                If _lblNutIcon IsNot Nothing Then _lblNutIcon.Visible = False
            Else
                _lblNutDesc.Text = "Nutrition values will appear here once products are linked in the Owner's Nutrition catalog."
                If _lblNutEmpty IsNot Nothing Then _lblNutEmpty.Visible = True
                If _lblNutIcon IsNot Nothing Then _lblNutIcon.Visible = True
            End If
            UpdateNutritionLayout()
            ' Step 6: per-selection lines re-check against the store state.
            UpdateNutritionForSelectedCartRow()
        End Sub

        ''' <summary>
        ''' Step 6 (Figma): the Nutrition preview follows the SELECTED CART ROW and
        ''' reads NutritionStore.TryGetForProduct — the same import-gated source the
        ''' Owner's Nutrition catalog uses, so both portals always agree (Wave C).
        ''' </summary>
        Private Sub UpdateNutritionForSelectedCartRow()
            If _lblNutName Is Nothing OrElse _lblNutSynopsis Is Nothing Then
                UpdateNutritionLayout()
                Return
            End If

            Dim showing As Boolean = False
            If dgvCart IsNot Nothing AndAlso dgvCart.SelectedRows.Count > 0 Then
                Dim code As String = Convert.ToString(dgvCart.SelectedRows(0).Cells(0).Value)
                Dim st As NutritionStatus = NutritionStore.GetStatus()
                If st.IsImported Then
                    Dim nameText As String = Convert.ToString(dgvCart.SelectedRows(0).Cells(1).Value)
                    If nameText.Length > 0 Then
                        _lblNutName.Text = nameText
                        Dim rec As NutritionRecord = NutritionStore.TryGetForProduct(code)
                        If rec IsNot Nothing Then
                            _lblNutSynopsis.Text = $"Per 100 g · {rec.EnergyKcal:N0} kcal · Protein {rec.ProteinG:N0} g"
                        Else
                            _lblNutSynopsis.Text = "No nutrition data for this item."
                        End If
                        _lblNutSynopsis.ForeColor = Theme.Muted
                        _lblNutName.Visible = True
                        _lblNutSynopsis.Visible = True
                        showing = True
                    End If
                End If
            End If

            If Not showing Then
                _lblNutName.Visible = False
                _lblNutSynopsis.Visible = False
            End If
            UpdateNutritionLayout()
        End Sub

        ''' <summary>Wrapped text height (px) for a label of the given width.</summary>
        Private Function WrappedHeight(text As String, font As Font, width As Integer, minH As Integer) As Integer
            If text Is Nothing OrElse text.Length = 0 Then Return minH
            Dim measured As Size = TextRenderer.MeasureText(text, font, New Size(width, Integer.MaxValue),
                                                            TextFormatFlags.WordBreak Or
                                                            TextFormatFlags.NoPrefix)
            Return Math.Max(minH, measured.Height + 2)
        End Function

        Private Function MakePair(leftCtl As Control, rightCtl As Control, rowHeight As Integer) As TableLayoutPanel
            Dim t As New TableLayoutPanel()
            t.Dock = DockStyle.Fill
            t.AutoSize = False
            t.Height = rowHeight
            t.Margin = New Padding(0, 2, 0, 0)
            t.BackColor = Color.Transparent
            t.ColumnCount = 2
            t.RowCount = 1
            t.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            t.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            t.Controls.Add(rightCtl, 1, 0)
            t.Controls.Add(leftCtl, 0, 0)
            Return t
        End Function

        ' ================= Today's transactions tab =================
        Private Sub RestyleToday()
            tabSales.BackColor = Theme.PageBg

            pnlSalesTop.Visible = False
            tabSales.Controls.Remove(pnlSalesTop)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvTodaySales)
            GridStyler.ApplyWeights(dgvTodaySales, 1.2, 1.0, 1.6, 1.1, 1.1, 1.2)
            body.Controls.Add(dgvTodaySales)
            _lblTodayEmpty = GridStyler.AttachEmptyOverlay(dgvTodaySales, "No sales have been recorded yet today.")

            _lblTodayFooter = GridStyler.NewFooter()
            _lblTodayFooter.Height = 24
            _lblTodayFooter.Dock = DockStyle.Bottom
            _lblTodayFooter.BackColor = Theme.White

            card.Controls.Add(body)
            card.Controls.Add(_lblTodayFooter)

            tabSales.Controls.Add(card)

            Dim heading As PageHeading = PageHeading.Build(tabSales, "Today's transactions", _
                "Review and audit every sale completed at this register today.")

            lblTodaySummary.Font = Theme.FontSmall
            lblTodaySummary.ForeColor = Theme.Muted
            lblTodaySummary.AutoSize = True
            lblTodaySummary.BackColor = Color.Transparent
            heading.AddLabel(lblTodaySummary)

            btnRefreshTodaySales.Text = "Refresh"
            Theme.StyleButton(btnRefreshTodaySales, ThemeButtonKind.Secondary)
            btnRefreshTodaySales.AutoSize = True
            heading.AddAction(btnRefreshTodaySales)

            btnViewSaleDetails.Text = "View receipt"
            Theme.StyleButton(btnViewSaleDetails, ThemeButtonKind.Secondary)
            btnViewSaleDetails.AutoSize = True
            heading.AddAction(btnViewSaleDetails)
        End Sub

        ' ================= Footer/empty-state updates =================
        Private Sub UpdateProductsFooter()
            If _lblProductsFooter Is Nothing Then Return
            Dim n As Integer = dgvPosProducts.RowCount
            GridStyler.UpdateFooter(_lblProductsFooter, n, n, If(n = 1, "product", "products"))
            If _lblProductsEmpty IsNot Nothing Then _lblProductsEmpty.Visible = (n = 0)
        End Sub

        Private Sub UpdateCartSummary()
            If _lblCartSummary Is Nothing Then Return
            Dim products As Integer = _cartService.Items.Count
            Dim units As Integer = _cartService.GetTotalItemCount()
            GridStyler.UpdateSummary(_lblCartSummary, $"{products} {(If(products = 1, "product", "products"))} · {units} units")
            lblSubtotalTitle.Text = $"Subtotal · {units} units"
            If _lblCartEmpty IsNot Nothing Then _lblCartEmpty.Visible = (dgvCart.RowCount = 0)
        End Sub

        Private Sub UpdateTodayFooter(total As Integer)
            If _lblTodayFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblTodayFooter, total, total, If(total = 1, "sale", "sales"))
            If _lblTodayEmpty IsNot Nothing Then _lblTodayEmpty.Visible = (dgvTodaySales.RowCount = 0)
        End Sub

        ' ================= New interactions (visual / flow only) =================
        Private Sub OnNewSaleClick(sender As Object, e As EventArgs)
            If _cartService.Items.Count > 0 Then
                If MessageBox.Show("Start a new sale? The current cart will be cleared.", "New Sale",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
            End If
            _cartService.ClearCart()
            cboDiscount.SelectedIndex = 0
            txtCashReceived.Clear()
            ' Step 2 (Figma "New sale"): a fresh cart starts with a clean scan box
            ' and no results popup visible (presentation only).
            ShowProductsPopup(False)
            txtPosSearch.Clear()
            Try
                numQuantity.Value = 1
            Catch
            End Try
            If cboCustomer.Items.Count > 0 Then cboCustomer.SelectedIndex = 0
            RefreshCartGrid()
            txtPosSearch.Focus()
        End Sub

        ''' <summary>Step 3: leaving the POS tab closes the results popup.</summary>
        Private Sub OnPosTabChanged(sender As Object, e As EventArgs) Handles tabCashier.SelectedIndexChanged
            If tabCashier.SelectedIndex <> 0 Then ShowProductsPopup(False)
        End Sub

        Private Sub OnEditQtyClick(sender As Object, e As EventArgs)
            If tabCashier.SelectedIndex <> 0 Then Return
            If dgvCart.SelectedRows.Count = 0 Then
                MessageBox.Show("Select a cart item to edit its quantity.", "Notice",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Dim code As String = Convert.ToString(dgvCart.SelectedRows(0).Cells(0).Value)
            Dim prod As Product = _productRepo.GetByCode(code)
            If prod Is Nothing Then Return
            _editingProductId = prod.ProductId
            _lblEditProduct.Text = prod.Name
            Dim currentQty As Integer = Convert.ToInt32(dgvCart.SelectedRows(0).Cells(3).Value)
            _numEditQty.Minimum = 0
            _numEditQty.Maximum = Math.Max(1, prod.StockQty)
            Try
                _numEditQty.Value = Math.Min(currentQty, CInt(_numEditQty.Maximum))
            Catch
                ' clamp issues are non-fatal
            End Try
            _editorRow.Visible = True
            _numEditQty.Focus()
            ' QA B2: the cart grid area shrinks by the editor strip — re-snap rows.
            LayoutPosColumns()
        End Sub

        Private Sub OnEditApplyClick(sender As Object, e As EventArgs)
            If _editingProductId <= 0 Then Return
            Dim prod As Product = _productRepo.GetById(_editingProductId)
            If prod Is Nothing Then
                CancelEditor()
                Return
            End If
            Dim qty As Integer = Convert.ToInt32(_numEditQty.Value)
            Try
                _cartService.UpdateItemQuantity(prod.ProductId, qty, prod.StockQty)
                RefreshCartGrid()
                CancelEditor()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub OnEditCancelClick(sender As Object, e As EventArgs)
            CancelEditor()
        End Sub

        Private Sub CancelEditor()
            _editingProductId = 0
            If _editorRow IsNot Nothing Then _editorRow.Visible = False
            ' QA B2: editor strip released — re-snap the cart rows.
            LayoutPosColumns()
        End Sub

        Private Sub OnCartSelectionChanged(sender As Object, e As EventArgs)
            If _editorRow IsNot Nothing AndAlso _editorRow.Visible Then
                CancelEditor()
            End If
            ' Step 6: the nutrition preview follows the selected cart row.
            UpdateNutritionForSelectedCartRow()
        End Sub

        Private Sub OnNutritionLinkClick(sender As Object, e As LinkLabelLinkClickedEventArgs)
            ' QA C1: the message must reflect the real store state, not claim
            ' "not linked" while data actually exists.
            Dim st As NutritionStatus = NutritionStore.GetStatus()
            If st.IsImported Then
                MessageBox.Show("Open Food Facts sample data is loaded — " &
                                $"{st.RecordsFound} products with per 100 g / 100 ml values " &
                                $"({st.RecordsVerified} verified, {st.RecordsNeedReview} to review).",
                                "Nutrition preview", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Nutrition data is not imported yet. Use the Owner's Nutrition catalog 'Import sample data' button to fill this panel.",
                                "Nutrition preview", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        ''' <summary>Hotkeys per design: F2 scan/search, F4 edit quantity, F12 complete sale.</summary>
        Private Sub CashierDashboardForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
            If tabCashier.SelectedIndex <> 0 Then Return
            Select Case e.KeyCode
                Case Keys.F2
                    txtPosSearch.Focus()
                    e.Handled = True
                Case Keys.F4
                    OnEditQtyClick(Me, EventArgs.Empty)
                    e.Handled = True
                Case Keys.F12
                    If btnCheckout.Enabled Then btnCheckout.PerformClick()
                    e.Handled = True
                Case Keys.Escape
                    ' Step 3: Escape closes the search-results popup as well.
                    ShowProductsPopup(False)
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Step 3: closes the search-results popup when a click lands outside the
        ''' popup or the scan box ("click-outside dismiss" dropdown behavior).
        ''' Guarded, so replaced/disposed cashier windows never touch stale controls.
        ''' </summary>
        Friend Sub DismissProductsPopupOnOutsideClick()
            If IsDisposed OrElse Disposing Then Return
            If _productsCard Is Nothing OrElse Not _productsPopupOpen Then Return
            Dim popupRect As Rectangle = _productsCard.RectangleToScreen(_productsCard.ClientRectangle)
            Dim searchRect As Rectangle = txtPosSearch.RectangleToScreen(txtPosSearch.ClientRectangle)
            If popupRect.Contains(Cursor.Position) OrElse searchRect.Contains(Cursor.Position) Then Return
            ShowProductsPopup(False)
        End Sub

        ''' <summary>
        ''' Step 3: mouse hook that fires the popup dismissal on any button-down,
        ''' before the click can change focus (standard dropdown convention).
        ''' Registered once per cashier window in InitializeUiRestyle.
        ''' </summary>
        Private Class PosPopupDismissFilter
            Implements IMessageFilter

            Private ReadOnly _form As CashierDashboardForm

            Public Sub New(form As CashierDashboardForm)
                _form = form
            End Sub

            Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
                If m.Msg = &H201 Then _form.DismissProductsPopupOnOutsideClick() ' WM_LBUTTONDOWN
                Return False
            End Function
        End Class
#End Region
    End Class
End Namespace
