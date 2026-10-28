' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Services / NutritionStore
' Purpose: Nutrition data layer for the finals build. The roster is a documented
'          DEMO dataset in memory keyed by seeded product code (PRD001..PRD030):
'          25 grocery rows carry plausible per-100g values; personal-care rows
'          intentionally have no nutrition record (honest empty lookups).
'          TryGet returns Nothing until the demo import runs, matching the
'          "No nutrition records yet" empty state on the Owner screen.
'
' WIRING CONTRACT (fulfilled by the demo import):
'   - RunImportAsync(progress, ct) walks the seeded codes, reports per-item
'     progress via IProgress(Of OffImportProgress) and publishes shared state
'     through OffImportState.Current so the Owner "Nutrition catalog" screen
'     can drive its ProgressRail, state label and counters
'     (records processed / valid / need review).
'   - Lookup: TryGetForProduct(TryGetForCode); a product is "Verified" when
'     NutritionStore.HasPer100Nutrition(record) is True, and "Needs review"
'     when serving size or sodium is missing.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Namespace Services

    ''' <summary>Per-product nutrition summary (per 100g basis).</summary>
    Public Class NutritionRecord
        Public Property ProductId As Integer
        Public Property ProductCode As String = String.Empty
        Public Property HasPer100 As Boolean = False
        Public Property EnergyKcal As Decimal = 0D
        Public Property ProteinG As Decimal = 0D
        Public Property FatG As Decimal = 0D
        Public Property CarbsG As Decimal = 0D
        Public Property SodiumMg As Nullable(Of Decimal) = Nothing
        Public Property ServingSizeG As Nullable(Of Decimal) = Nothing
        Public Property IsPerServingMissing As Boolean = True
        Public Property SourceName As String = "Open Food Facts"
        Public Property SourceDate As DateTime
        Public Property ImportedBy As String = String.Empty
    End Class

    ''' <summary>Progress payload for the import service.</summary>
    Public Structure OffImportProgress
        Public RecordsProcessed As Integer
        Public RecordsTotal As Integer
        Public RecordsValid As Integer
        Public RecordsNeedReview As Integer
        Public Phase As String
        Public HasFinished As Boolean
        Public HasFailed As Boolean
    End Structure

    ''' <summary>Shared import state; the import service publishes here.</summary>
    Public NotInheritable Class OffImportState

        Private Sub New()
        End Sub

        Public Shared ReadOnly Current As New OffImportState__Runtime()

        Public NotInheritable Class OffImportState__Runtime
            Public Property IsRunning As Boolean = False
            Public Property Progress As OffImportProgress
            Public Property LastError As String = String.Empty
            ' QA C1: the import moment feeds the Owner status card and the Cashier
            ' preview ("imported 14:05") from one shared place.
            Public Property LastImportAt As DateTime = DateTime.MinValue
        End Class
    End Class

    ''' <summary>
    ''' QA C1: one snapshot that every nutrition UI (Owner status card, banner,
    ''' grid and the Cashier preview) reads, so the screens can never contradict
    ''' each other. Counters come from the store itself, not from a progress
    ''' message that resets between runs.
    ''' </summary>
    Public Class NutritionStatus
        Public RecordsFound As Integer
        Public RecordsVerified As Integer
        Public RecordsNeedReview As Integer
        Public IsImported As Boolean = False
        Public IsRunning As Boolean = False
        Public LastImportAt As DateTime = DateTime.MinValue
        Public SourceName As String = "Open Food Facts (sample data)"
    End Class

    Public NotInheritable Class NutritionStore

        Private Sub New()
        End Sub

        ' ------------------------------------------------------------------
        ' DEMO DATA (in-memory; keyed by seeded product code)
        ' ------------------------------------------------------------------
        Private Shared ReadOnly _sync As New Object()
        Private Shared ReadOnly _byCode As New Dictionary(Of String, NutritionRecord)(StringComparer.OrdinalIgnoreCase)
        Private Shared _demoReady As Boolean = False

        ' QA C1: True once an import in THIS session published data. Everything
        ' user-visible derives from GetStatus(), so "empty" is always honest.
        Private Shared _imported As Boolean = False

        ''' <summary>Single source of truth for the Owner status card/banner and the Cashier nutrition preview.</summary>
        Public Shared Function GetStatus() As NutritionStatus
            Dim st As New NutritionStatus()
            SyncLock _sync
                st.RecordsFound = _byCode.Count
                For Each rec As NutritionRecord In _byCode.Values
                    If NeedsReview(rec) Then
                        st.RecordsNeedReview += 1
                    Else
                        st.RecordsVerified += 1
                    End If
                Next
                st.IsImported = _imported
            End SyncLock
            st.IsRunning = OffImportState.Current.IsRunning
            st.LastImportAt = OffImportState.Current.LastImportAt
            Return st
        End Function

        ''' <summary>Lookup by product code (Nothing until an import has run).
        ''' QA C1 ROOT CAUSE: this lookup used to call EnsureDemoData() itself, so
        ''' simply opening the Owner grid silently hydrated all 25 demo records
        ''' while the status card (fed from OffImportState) still showed 0 /
        ''' "Not imported yet" — the screen contradicted itself. Lookups are now
        ''' import-gated: only RunImportAsync seeds the store.</summary>
        Public Shared Function TryGetForProduct(code As String) As NutritionRecord
            If String.IsNullOrWhiteSpace(code) Then Return Nothing
            SyncLock _sync
                Dim rec As NutritionRecord = Nothing
                If _byCode.TryGetValue(code.Trim(), rec) Then Return rec
                Return Nothing
            End SyncLock
        End Function

        ''' <summary>Kept for compatibility with the old id-keyed contract;
        ''' demo data is code-keyed, so this always returns Nothing.</summary>
        Public Shared Function TryGetForProduct(productId As Integer) As NutritionRecord
            Return Nothing
        End Function

        Public Shared Function TryGetForCode(code As String) As NutritionRecord
            Return TryGetForProduct(code)
        End Function

        ''' <summary>Verified = complete per-100g nutrition present.</summary>
        Public Shared Function HasPer100Nutrition(record As NutritionRecord) As Boolean
            Return record IsNot Nothing AndAlso record.HasPer100
        End Function

        ''' <summary>Needs review = missing serving size or sodium.</summary>
        Public Shared Function NeedsReview(record As NutritionRecord) As Boolean
            Return record IsNot Nothing AndAlso
                   (Not record.SodiumMg.HasValue OrElse Not record.ServingSizeG.HasValue)
        End Function

        Private Shared Sub EnsureDemoData()
            If _demoReady Then Return
            _demoReady = True
            Dim today As DateTime = DateTime.Today

            ' Rice / grains -------------------------------------------------
            AddDemo("PRD001", 356D, 7.1D, 1.2D, 78.0D, 5D, 5D, today)
            AddDemo("PRD002", 354D, 6.9D, 1.0D, 77.8D, 4D, 5D, today)
            AddDemo("PRD003", 356D, 7.4D, 1.5D, 78.3D, 6D, 1D, today)
            AddDemo("PRD004", 362D, 7.9D, 2.7D, 75.9D, 7D, Nothing, today) ' serving missing -> needs review

            ' Canned / preserved --------------------------------------------
            AddDemo("PRD005", 206D, 24.6D, 11.4D, 0.5D, 480D, 155D, today)
            AddDemo("PRD006", 208D, 24.9D, 11.8D, 0.6D, 540D, 155D, today)
            AddDemo("PRD007", 250D, 20.0D, 18.0D, 1.5D, 860D, 150D, today)
            AddDemo("PRD008", 260D, 16.0D, 21.0D, 2.0D, Nothing, 150D, today) ' sodium missing -> needs review
            AddDemo("PRD009", 234D, 24.0D, 15.6D, 0.8D, 600D, 180D, today)
            AddDemo("PRD010", 250D, 26.0D, 17.0D, 1.0D, 700D, 155D, today)

            ' Instant noodles / pasta ---------------------------------------
            AddDemo("PRD011", 445D, 9.5D, 15.5D, 74.0D, 920D, 80D, today)
            AddDemo("PRD012", 448D, 9.8D, 16.0D, 74.5D, 990D, 80D, today)
            AddDemo("PRD013", 452D, 10.2D, 17.8D, 73.4D, 1080D, 55D, today)
            AddDemo("PRD014", 450D, 10.0D, 17.0D, 74.0D, Nothing, 130D, today) ' sodium missing -> needs review
            AddDemo("PRD015", 356D, 12.0D, 1.5D, 74.0D, 6D, 900D, today)

            ' Beverages ------------------------------------------------------
            AddDemo("PRD016", 42D, 0D, 0D, 10.6D, 5D, 1500D, today)
            AddDemo("PRD017", 43D, 0D, 0D, 10.8D, 10D, 1500D, today)
            AddDemo("PRD018", 42D, 0D, 0D, 10.5D, 8D, 1500D, today)
            AddDemo("PRD019", 36D, 0D, 0D, 9.0D, 15D, 500D, today)
            AddDemo("PRD020", 43D, 0.5D, 0D, 3.6D, 4D, 330D, today)

            ' Snacks ---------------------------------------------------------
            AddDemo("PRD021", 536D, 6.0D, 32.0D, 57.0D, 500D, 85D, today)
            AddDemo("PRD022", 494D, 6.0D, 25.0D, 62.0D, 520D, 78D, today)
            AddDemo("PRD023", 530D, 7.0D, 30.0D, 59.0D, 560D, 110D, today)
            AddDemo("PRD024", 436D, 8.0D, 12.0D, 72.0D, 640D, 240D, today)
            AddDemo("PRD025", 470D, 7.0D, 18.0D, 64.0D, 520D, Nothing, today) ' serving missing -> needs review

            ' PRD026..PRD030 are personal-care items - no nutrition record
            ' is created for them (honest "no data" lookups).
        End Sub

        Private Shared Sub AddDemo(code As String, kcal As Decimal, protein As Decimal, fat As Decimal,
                                   carbs As Decimal, sodium As Nullable(Of Decimal),
                                   serving As Nullable(Of Decimal), sourceDate As DateTime)
            If _byCode.ContainsKey(code) Then Return
            Dim rec As New NutritionRecord() With {
                .ProductCode = code,
                .HasPer100 = True,
                .EnergyKcal = kcal,
                .ProteinG = protein,
                .FatG = fat,
                .CarbsG = carbs,
                .SodiumMg = sodium,
                .ServingSizeG = serving,
                .IsPerServingMissing = Not serving.HasValue,
                .SourceDate = sourceDate,
                .ImportedBy = "Demo import"}
            _byCode.Add(code, rec)
        End Sub

        ''' <summary>Demo import: walks the seeded codes, animates the progress
        ''' rail and publishes Open-Data-style counters. Replay-safe.</summary>
        Public Shared Function RunImportAsync(progress As IProgress(Of OffImportProgress), ct As CancellationToken) As Task
            EnsureDemoData()
            ' QA C1: the import is the only legitimate seeding trigger — mark the
            ' store as carrying data from this point (kept even on cancel, because
            ' EnsureDemoData has already filled every record).
            SyncLock _sync
                _imported = True
            End SyncLock
            Return Task.Run(
                Sub()
                    Dim codes As New List(Of String)(_byCode.Keys)
                    Dim codesTotal As Integer = codes.Count
                    Dim canReport As IProgress(Of OffImportProgress) = progress
                    Try
                        OffImportState.Current.IsRunning = True
                        Dim interimValid As Integer = 0
                        Dim interimReview As Integer = 0
                        For i As Integer = 0 To codesTotal - 1
                            ct.ThrowIfCancellationRequested()
                            If NeedsReview(_byCode(codes(i))) Then
                                interimReview += 1
                            Else
                                interimValid += 1
                            End If
                            Dim p As New OffImportProgress() With {
                                .RecordsProcessed = i + 1,
                                .RecordsTotal = codesTotal,
                                .RecordsValid = interimValid,
                                .RecordsNeedReview = interimReview,
                                .Phase = "Reading Open Food Facts demo data…"}
                            OffImportState.Current.Progress = p
                            If canReport IsNot Nothing Then canReport.Report(p)
                            Thread.Sleep(12) ' brief simulated fetch per record
                        Next

                        Dim done As New OffImportProgress() With {
                            .RecordsProcessed = codesTotal,
                            .RecordsTotal = codesTotal,
                            .RecordsValid = interimValid,
                            .RecordsNeedReview = interimReview,
                            .Phase = "Demo import finished",
                            .HasFinished = True}
                        ' QA C1: record when the import happened (status card/preview).
                        OffImportState.Current.LastImportAt = DateTime.Now
                        OffImportState.Current.Progress = done
                        OffImportState.Current.IsRunning = False
                        OffImportState.Current.LastError = String.Empty
                        If canReport IsNot Nothing Then canReport.Report(done)
                    Catch canceled As OperationCanceledException
                        OffImportState.Current.IsRunning = False
                        OffImportState.Current.LastError = "Import cancelled."
                        Dim stopped As New OffImportProgress() With {.Phase = "Cancelled", .HasFailed = False}
                        If canReport IsNot Nothing Then canReport.Report(stopped)
                    End Try
                End Sub)
        End Function

    End Class

End Namespace
