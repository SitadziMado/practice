Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim A = New UPUInt

        For I% = 0 To 10

            Dim S$ = Factorial(200).ToString()

        Next I

        A = Nothing

    End Sub

    Private Function Factorial(Number As Integer) As UPUInt

        Factorial = New UPUInt(1)

        For I% = 1 To Number
            Factorial *= New UPUInt(I)
        Next I%

    End Function

End Class
