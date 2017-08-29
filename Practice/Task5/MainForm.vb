Public Class MainForm

    Private mMatrix As SquareMatrix = Nothing

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        mMatrix = New SquareMatrix(Enumerable.Range(1, 64).ToArray, Inserter:=New DiagonalInserter)

    End Sub

    Private Sub MainForm_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        If mMatrix IsNot Nothing Then
            Call mMatrix.Draw(e.Graphics)
        End If
    End Sub

End Class
