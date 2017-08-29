Imports Task5

Public Class DiagonalInserter
    Inherits Inserter

    Public Sub New()
        ' MyBase.New(MatrixSize)
    End Sub

    Public Overrides Sub Fill(ByRef This As SquareMatrix, ByRef Elements() As Integer)

        Dim Direction As Direction = Direction.Right
        Dim Reverse = False
        Dim I% = 0, J% = 0
        Dim UpperBound = This.Size - 1
        Dim K = 0%

        Try
            Do While True

                ' Пишем элемент.
                This(I, J) = Elements(K)

                Call Move(Direction, I, J)

                ' Перемещаем индексы.
                Select Case Direction
                    Case Direction.Right
                        Direction = Direction.BottomLeft
                    Case Direction.Bottom
                        Direction = Direction.TopRight
                    Case Direction.BottomLeft
                        If J = 0 Then Direction = Direction.Bottom
                    Case Direction.TopRight
                        If I = 0 Then Direction = Direction.Right
                End Select

                K += 1

            Loop
        Catch ex As ArgumentOutOfRangeException
            If J = This.Size Then
                J -= 1
                Direction = Direction.Bottom
            Else
                I -= 1
                Direction = Direction.Right
            End If
        End Try

        K -= 1

        For Num% = K To Elements.Count - 1

            ' Пишем элемент.
            This(I, J) = Elements(Num)

            Call Move(Direction, I, J)

            ' Перемещаем индексы.
            Select Case Direction
                Case Direction.Right
                    Direction = Direction.TopRight
                Case Direction.Bottom
                    Direction = Direction.BottomLeft
                Case Direction.BottomLeft
                    If I = This.Size - 1 Then Direction = Direction.Right
                Case Direction.TopRight
                    If J = This.Size - 1 Then Direction = Direction.Bottom
            End Select

        Next Num%

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
