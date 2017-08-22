#Const PARALLEL = True ' False

Imports System.Text

Public Class UPUInt

    Public Sub New()

        Call mDigits.Add(0)

    End Sub

    Public Sub New(Number As ULong)

        Call Init(Number)

    End Sub

    Public Sub New(Number As Long)

        If Number < 0 Then Throw New ArgumentOutOfRangeException("Нужны неотрицательные значения.")

        Call Init(Number)

    End Sub

    Public Sub New(Number As Double)

        Call Init(Number)

    End Sub

    Public Sub New(StringNumber As String)

        For Each C As Char In StringNumber
            Call mDigits.Insert(0, Convert.ToByte(C))
        Next

    End Sub

    Public Sub New(ByRef Other As UPUInt)

        mDigits.AddRange(Other.mDigits)

    End Sub

    Public Shared Operator +(Lhs As UPUInt, Rhs As UPUInt)

        If Rhs.mDigits.Count > Lhs.mDigits.Count Then

            Dim Tmp = Rhs
            Rhs = Lhs
            Lhs = Tmp

        End If

        For I% = 0 To Rhs.mDigits.Count - 1

            Lhs.mDigits(I) += Rhs.mDigits(I)
            Call Lhs.Carry(I)

        Next I

        Return Lhs

    End Operator

    Public Shared Operator *(Lhs As UPUInt, Rhs As UPUInt) As UPUInt

        If Rhs.mDigits.Count > Lhs.mDigits.Count Then

            Dim Tmp = Rhs
            Rhs = Lhs
            Lhs = Tmp

        End If

#If PARALLEL Then
        If Rhs.mDigits.Count + Lhs.mDigits.Count < PARALLEL_MINIMUN OrElse Environment.ProcessorCount = 1 Then
            Return Multiply(Lhs, Rhs)
        Else
            Return ParallelMultiply(Lhs, Rhs)
        End If
#Else
        Return Multiply(Lhs, Rhs)
#End If

    End Operator

    Public Overrides Function ToString() As String

        Dim Sb = New StringBuilder

        For Each B As Byte In mDigits
            Call Sb.Append(B)
        Next

        If Sb.Length = 0 Then Sb.Append("0")

        Return New String(Sb.ToString().Reverse().ToArray())

    End Function

    Private Shared Function Multiply(Lhs As UPUInt, Rhs As UPUInt) As UPUInt

        Dim Result = New UPUInt

        For I% = 0 To Lhs.mDigits.Count - 1
            Result += Mul(New UPUInt(Rhs), Lhs.mDigits(I), I)
        Next I

        Return Result

    End Function

    Private Shared Function ParallelMultiply(Lhs As UPUInt, Rhs As UPUInt) As UPUInt

        Dim ThreadNumber = Environment.ProcessorCount
        Dim Result = New UPUInt
        Dim Queue = New Queue(Of Task(Of UPUInt))

        For I% = 0 To Lhs.mDigits.Count - 1

            Dim Counter = I
            Call Queue.Enqueue(Task(Of UPUInt).Factory.StartNew(Function() Mul(New UPUInt(Rhs), Lhs.mDigits(Counter), Counter)))

        Next I%

        For I% = 0 To Lhs.mDigits.Count - 1

            Dim Cur = Queue.Dequeue()
            Call Cur.Wait()
            Result += Cur.Result

        Next I%

        Return Result

    End Function

    Private Sub Init(Number As ULong)

        While Number > 0

            Call mDigits.Add(Number Mod BASE)
            Number \= BASE

        End While

    End Sub

    Private Sub Carry(Index As Integer)

        While mDigits(Index) >= BASE

            mDigits(Index) -= BASE

            If mDigits.Count > Index + 1 Then
                mDigits(Index + 1) += 1
            Else
                Call mDigits.Add(1)
            End If

        End While

    End Sub

    Private Shared Function Mul(Lhs As UPUInt, Rhs As UInteger, I As Integer) As UPUInt

        For J% = 0 To Lhs.mDigits.Count - 1
            Lhs.mDigits(J) *= Rhs
        Next J

        For J% = 0 To Lhs.mDigits.Count - 1
            Call Lhs.Carry(J)
        Next J

        For J% = 0 To I - 1
            Lhs.mDigits.Insert(0, 0)
        Next J

        Return Lhs

    End Function

    Private Const BASE = 10UL
    Private Const PARALLEL_MINIMUN = 20UL

    Private mDigits As List(Of UInteger) = New List(Of UInteger)
    Private mLock = New Object

End Class