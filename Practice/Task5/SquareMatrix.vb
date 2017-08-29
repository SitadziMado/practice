Public Class SquareMatrix

    Public Sub New(
                  Elements() As Integer,
                  Optional MatrixSize As Integer = 8%,
                  Optional Inserter As Inserter = Nothing
    )

        If Elements.Length < Size * Size Then

            Throw New ArgumentOutOfRangeException(
                "Массив инициализаторов должен быть больше или равен количеству элементов матрицы."
            )

        End If

        If MatrixSize <= 0 Then Throw New ArgumentOutOfRangeException("Запрещается отрицательное значение размера.")

        Size = MatrixSize
        ReDim mData(Size - 1, Size - 1)

        Inserter = If(Inserter, New DefaultInserter)
        Call Inserter.Fill(Me, Elements)

    End Sub

    Default Public Property Item(ByVal I As Integer, ByVal J As Integer) As Integer

        Get
            If I >= 0 AndAlso J >= 0 AndAlso I < mData.GetLength(0) AndAlso J < mData.GetLength(1) Then
                Return mData(I, J)
            Else
                Throw New ArgumentOutOfRangeException("Неверный индекс.")
            End If
        End Get

        Set(Value As Integer)
            If I >= 0 AndAlso J >= 0 AndAlso I < mData.GetLength(0) AndAlso J < mData.GetLength(1) Then
                mData(I, J) = Value
            Else
                Throw New ArgumentOutOfRangeException("Неверный индекс.")
            End If
        End Set

    End Property

    Public Sub Draw(GraphicsToDraw As Graphics)

        Dim Font As Font = New Font("Verdana", FontSize)

        With GraphicsToDraw
            For I% = 0 To mData.GetLength(0) - 1
                For J% = 0 To mData.GetLength(1) - 1

                    Call .DrawString(mData(J, I).ToString(), Font, Brushes.Black, I * 4.0! * FontSize, J * 4.0! * FontSize)

                Next J%
            Next I%
        End With

    End Sub

    Private Const FontSize = 14%

    Public Property Size()

    Private mData(,) As Integer

End Class