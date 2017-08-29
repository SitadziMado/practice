Imports Task5

Public Class DefaultInserter
    Inherits Inserter

    Public Sub New()
        ' MyBase.New(MatrixSize)
    End Sub

    Public Overrides Sub Fill(ByRef This As SquareMatrix, ByRef Elements() As Integer)

        Dim Current = 0%

        For I% = 0 To This.Size - 1
            For J% = 0 To This.Size - 1

                This(I, J) = Elements(Current)
                Current += 1

            Next J
        Next I

    End Sub

End Class
