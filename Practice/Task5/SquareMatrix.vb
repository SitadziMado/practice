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
        ReDim mData(Size, Size)

        Inserter = If(Inserter, New DefaultInserter(Size))
        Call Inserter.Fill(Me, Elements)

    End Sub

    Default Public Property Item(ByVal I As Integer, ByVal J As Integer) As Integer

        Get
            Return mData(I, J)
        End Get

        Set(Value As Integer)
            mData(I, J) = Value
        End Set

    End Property

    Public Property Size()

    Private mData(,) As Integer

End Class