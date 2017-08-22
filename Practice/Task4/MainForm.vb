Public Class MainForm

    Private Const INPUT_LBOUND = 0
    Private Const INPUT_MBOUND = 300
    Private Const INPUT_UBOUND = 500

    Private ResultLock = New Object
    Private Result As UPUInt
    Private CpuCounter As PerformanceCounter = Nothing
    Private RamCounter As PerformanceCounter = Nothing
    Private MsCount As Long

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ComputeButton_Click(sender As Object, e As EventArgs) Handles ComputeButton.Click

        ComputeButton.Enabled = False
        DiagnosticsTimer.Enabled = True
        CancelButton.Enabled = True

        ResultTextBox.Text = "Работаем..." &
            If(
                CpuCounter Is Nothing OrElse RamCounter Is Nothing,
                vbNewLine & "Инициализация счетчиков производительности...", ""
            )

        Call ComputeFactorial.RunWorkerAsync()

    End Sub


    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click

        Call ComputeFactorial.CancelAsync()
        CancelButton.Enabled = False

    End Sub

    Private Sub IllegalNumber()
        Call MsgBox("Введите корректное целое неотрицательное число.")
    End Sub

    Private Function Factorial(Number As Integer) As UPUInt

        Factorial = New UPUInt(1)

        For I% = 1 To Number

            Factorial *= New UPUInt(I)
            Call ComputeFactorial.ReportProgress(100 * I / Number)

            If ComputeFactorial.CancellationPending Then Return New UPUInt

        Next I%

    End Function

    Private Sub ComputeFactorial_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ComputeFactorial.DoWork

        Try

            Call InitCounters()

            Dim Number = Integer.Parse(SourceTextBox.Text)

            If Number < 0 Then Call IllegalNumber() : Exit Sub

            Dim Counter = New Stopwatch
            Call Counter.Start()

            SyncLock ResultLock
                Result = Factorial(Number)
            End SyncLock

            Call Counter.Stop()
            MsCount = Counter.ElapsedMilliseconds

            If ComputeFactorial.CancellationPending Then e.Cancel = True

        Catch ex As FormatException
            Call IllegalNumber()
        End Try

    End Sub

    Private Sub ComputeFactorial_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ComputeFactorial.RunWorkerCompleted

        ComputeProgress.Value = 100

        ResultTextBox.Text = "Готово! Затраченное время: " & (MsCount / 1000).ToString() & " c" & vbNewLine & vbNewLine &
            Result.ToString() &
            If(e.Cancelled, " (прервано)", "")

        ComputeButton.Enabled = True
        DiagnosticsTimer.Enabled = False
        CancelButton.Enabled = False

    End Sub

    Private Sub SourceTextBox_TextChanged(sender As Object, e As EventArgs) Handles SourceTextBox.TextChanged

        Dim Number%
        Dim Flag = Integer.TryParse(SourceTextBox.Text, Number)

        If Flag Then

            If INPUT_LBOUND <= Number And Number < INPUT_MBOUND Then
                SourceTextBox.ForeColor = Color.Green
            ElseIf INPUT_MBOUND <= Number And Number < INPUT_UBOUND Then
                SourceTextBox.ForeColor = Color.Orange
            Else
                SourceTextBox.ForeColor = Color.Red
            End If

        Else
            SourceTextBox.ForeColor = Color.Black
        End If

    End Sub

    Private Sub ComputeFactorial_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles ComputeFactorial.ProgressChanged
        ComputeProgress.Value = e.ProgressPercentage
    End Sub

    Private Sub DiagnosticsTimer_Tick(sender As Object, e As EventArgs) Handles DiagnosticsTimer.Tick

        If CpuCounter IsNot Nothing AndAlso RamCounter IsNot Nothing Then

            ResultTextBox.Text = "Работаем..." & vbNewLine &
                "Загрузка ЦП: " & Convert.ToInt32(CpuCounter.NextValue()).ToString() & "%" & vbNewLine &
                "Потребление памяти: " & Convert.ToInt32(RamCounter.NextValue()).ToString() & "MiB"

        End If

    End Sub

    Private Sub InitCounters()

        If CpuCounter Is Nothing Then
            CpuCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total", True)
        End If

        If RamCounter Is Nothing Then
            RamCounter = New PerformanceCounter("Memory", "Available MBytes", True)
        End If

    End Sub

End Class
