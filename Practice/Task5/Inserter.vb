Public MustInherit Class Inserter

    Public Sub New()
        ' Size = MatrixSize
    End Sub

    Public MustOverride Sub Fill(ByRef This As SquareMatrix, ByRef Elements() As Integer)

    ' Public Property Size()

End Class