Imports Task3

Public Class Diamond
    Implements IDrawable

    Public Sub New(
                  Optional X As Single = 0.0F,
                  Optional Y As Single = 0.0F,
                  Optional Width As Single = 1.0F,
                  Optional Height As Single = 2.0F
    )

        mX = X
        mY = Y
        mWidth = Width
        mHeight = Height

    End Sub

    Public Sub Draw(GraphicsToDraw As Graphics) Implements IDrawable.Draw

        With GraphicsToDraw

            Dim X = Parent.ScaleX(mX)
            Dim Y = Parent.ScaleY(mY)
            Dim Left = Parent.ScaleX(mX - mWidth * 0.5F)
            Dim Right = Parent.ScaleX(mX + mWidth * 0.5F)
            Dim Top = Parent.ScaleY(mY - mHeight * 0.5F)
            Dim Bottom = Parent.ScaleY(mY + mHeight * 0.5F)

            Dim Pts(0 To 4) As PointF

            Pts(0) = New PointF(Left, Y)
            Pts(1) = New PointF(X, Top)
            Pts(2) = New PointF(Right, Y)
            Pts(3) = New PointF(X, Bottom)
            Pts(4) = Pts(0)

            Call .DrawPolygon(Pens.Black, Pts)
            Call .FillPolygon(Brushes.Aqua, Pts)

        End With

    End Sub

    Public Function Test(Pt As PointF) As Boolean

        Dim Coeff = mHeight / mWidth

        Dim Left = mX - mWidth * 0.5F
        Dim Right = mX + mWidth * 0.5F
        Dim Top = mY - mHeight * 0.5F
        Dim Bottom = mY + mHeight * 0.5F

        With Pt

            If .X >= Left And .X <= Right And .Y >= Top And .Y <= Bottom Then

                Dim Offset = mHeight / 2
                Dim AbsValue = -Math.Abs(Coeff * Pt.X - mX) + Offset

                If ((AbsValue + mY) > Pt.Y) And
                   ((-AbsValue + mY) < Pt.Y) Then

                    Return True

                End If

            End If

        End With

        Return False

    End Function

    Public Property Parent() As Plane

    Private mX As Single
    Private mY As Single
    Private mWidth As Single
    Private mHeight As Single

End Class
