Imports Task3

Public Class Plane
    Implements IDrawable

    Public Sub New(
                  Width As Single,
                  Height As Single,
                  Optional Scale As Double = 100.0,
                  Optional GridPen As Pen = Nothing,
                  Optional ClearColor? As Color = Nothing
    )
        mWidth = Width
        mHeight = Height
        mShapes = New List(Of Diamond)
        mScale = Scale

        If ClearColor Is Nothing Then
            mClearColor = Color.White
        Else
            mClearColor = ClearColor
        End If

        If GridPen Is Nothing Then
            mGridPen = Pens.Black
        Else
            mGridPen = GridPen
        End If
    End Sub

    Public Sub Draw(GraphicsToDraw As Graphics) Implements IDrawable.Draw

        Call GraphicsToDraw.Clear(mClearColor)
        Call DrawShapes(GraphicsToDraw)
        Call DrawGrid(GraphicsToDraw)

    End Sub

    Public Sub Resize(NewWidth As Single, NewHeight As Single)

        mWidth = NewWidth
        mHeight = NewHeight

    End Sub

    Public Sub ChangeScale(Delta As Single)

        mScale += Delta

        If mScale < MINIMUM_SCALE Then mScale = MINIMUM_SCALE

    End Sub

    Public Function Test(RawPt As PointF) As Boolean

        With RawPt

            Return Test(.X, .Y)

        End With

    End Function

    Public Function Test(RawX As Single, RawY As Single) As Boolean

        Dim Result = True
        Dim Pt = UnscalePoint(RawX, RawY)

        For Each S As Diamond In mShapes

            Result = Result And S.Test(Pt)

            If Not Result Then Exit For

        Next

        Return Result

    End Function

    Public Function AddShape(ByVal Shape As Diamond) As Plane

        Shape.Parent = Me
        mShapes.Add(Shape)

        Return Me

    End Function

    Public Function ScalePoint(Pt As PointF) As PointF

        With Pt

            Return New PointF(ScaleX(.X), ScaleY(.Y))

        End With

    End Function

    Public Function ScalePoint(X As Single, Y As Single) As PointF

        Return New PointF(ScaleX(X), ScaleY(Y))

    End Function

    Public Function ScaleX(X As Single) As Single

        Dim CX = mWidth * 0.5

        Return CX + X * mScale

    End Function

    Public Function ScaleY(Y As Single) As Single

        Dim CY = mHeight * 0.5

        Return CY + Y * mScale

    End Function

    Public Function UnscalePoint(Pt As PointF) As PointF

        Return UnscalePoint(Pt.X, Pt.Y)

    End Function

    Public Function UnscalePoint(X As Single, Y As Single) As PointF

        Dim CX = mWidth * 0.5
        Dim CY = mHeight * 0.5

        Return New PointF((X - CX) / mScale, (Y - CY) / mScale)

    End Function

    Private Sub DrawGrid(GraphicsToDraw As Graphics)

        With GraphicsToDraw

            Dim MX = mWidth / 2
            Dim MY = mHeight / 2
            Dim X = mWidth
            Dim Y = mHeight
            Dim DrawingFont = New Font("Verdana", 12)
            Dim DrawingBrush = New SolidBrush(mGridPen.Color)

            Call .DrawLine(mGridPen, 0.0F, MY, mWidth, MY)
            Call .DrawLine(mGridPen, MX, 0.0F, MX, mHeight)

            REM Стрелки
            Call .DrawLine(mGridPen, X, MY, X - OFFSET, MY - OFFSET)

            REM Стрелки
            Call .DrawLine(mGridPen, X, MY, X - OFFSET, MY + OFFSET)

            REM Стрелки
            Call .DrawLine(mGridPen, MX, Y, MX - OFFSET, Y - OFFSET)

            REM Стрелки
            Call .DrawLine(mGridPen, MX, Y, MX + OFFSET, Y - OFFSET)

            Call .DrawString(
                "x",
                DrawingFont,
                DrawingBrush,
                New PointF(X - OFFSET * 4.0F, MY + OFFSET * 2.0F)
            )

            Call .DrawString(
                "y",
                DrawingFont,
                DrawingBrush,
                New PointF(MX + OFFSET * 2.0F, Y - OFFSET * 4.0F)
            )

            For I& = -10 To 10

                For J& = -10 To 10

                    Dim Horizontal(0 To 1) As PointF

                    Horizontal(0) = ScalePoint(I, 0.0F)
                    Horizontal(0).Y -= OFFSET
                    Horizontal(1) = ScalePoint(I, 0.0F)
                    Horizontal(1).Y += OFFSET

                    Dim Vertical(0 To 1) As PointF

                    Vertical(0) = ScalePoint(0.0F, J)
                    Vertical(0).X -= OFFSET
                    Vertical(1) = ScalePoint(0.0F, J)
                    Vertical(1).X += OFFSET

                    ' Разметка
                    Call .DrawLines(mGridPen, Horizontal)
                    Call .DrawLines(mGridPen, Vertical)

                    ' Подписи под метками
                    Call .DrawString(I, DrawingFont, DrawingBrush, Horizontal(1))

                    If (J <> 0) Then Call .DrawString(J, DrawingFont, DrawingBrush, Vertical(1))

                Next J

            Next I

        End With

    End Sub

    Private Sub DrawShapes(GraphicsToDraw As Graphics)

        For Each S As Diamond In mShapes

            Call S.Draw(GraphicsToDraw)

        Next

    End Sub

    Private Const OFFSET = 4.0F
    Private Const MINIMUM_SCALE = 30.0F

    Private mWidth As Single
    Private mHeight As Single
    Private mScale As Double
    Private mGridPen As Pen
    Private mClearColor As Color

    Private mShapes As List(Of Diamond)

End Class