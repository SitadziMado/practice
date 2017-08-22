Imports Task5

Public Class DiagonalInserter
    Inherits Inserter

    Public Sub New(MatrixSize As Integer)
        MyBase.New(MatrixSize)
    End Sub

    Public Overrides Sub Fill(ByRef This As SquareMatrix, ByRef Elements() As Integer)

        Dim Direction As Direction = Direction.Right
        Dim Reverse = False
        Dim I% = 0, J% = 0
        Dim UpperBound = Size - 1

        For K% = 0 To UpperBound

            ' Пишем элемент.
            This(I, J) = Elements(K)

            ' Перемещаем индексы.
            Select Case Direction
                Case Direction.Right

                    If I = UpperBound AndAlso J = UpperBound Then
                        Reverse = True
                    Else
                        Call Move(Direction, I, J)
                    End If

                    If Reverse Then
                        Direction = Direction.TopRight
                    Else
                        Direction = Direction.BottomLeft
                    End If

                Case Direction.Bottom

                Case Direction.BottomLeft

                Case Direction.TopRight

            End Select

        Next K%

    End Sub

    Private Sub Move(Direction As Direction, ByRef I As Integer, ByRef J As Integer)
        Select Case Direction
            Case Direction.Right
                J += 1
            Case Direction.Bottom
                I += 1
            Case Direction.BottomLeft
                I += 1
                J -= 1
            Case Direction.TopRight
                I -= 1
                J += 1
        End Select
    End Sub

    Private Enum Direction
        Right
        Bottom
        BottomLeft
        TopRight
    End Enum

End Class
