Public Class MainForm

    Private Const SCALE_CHANGE = 10.0F

    Private mPlane As Plane

    Public Sub New()

        mPlane = New Plane(Width, Height)
        mPlane.AddShape(New Diamond)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

    End Sub

    Private Sub Surface_Paint(sender As Object, e As PaintEventArgs) Handles Surface.Paint

        Call mPlane.Draw(e.Graphics)

    End Sub

    Private Sub Surface_Resize(sender As Object, e As EventArgs) Handles Surface.Resize

        Call mPlane?.Resize(Surface.Width - 3, Surface.Height - 3)
        Call Surface.Invalidate()

    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)

        Call MyBase.OnMouseWheel(e)

        Dim Delta = If(e.Delta > 0, 1.0F, -1.0F) * SCALE_CHANGE

        ' Меняем масштаб
        Call mPlane.ChangeScale(Delta)

        Call Surface.Invalidate()

    End Sub

    Private Sub CheckCollision(e As Point)

        If mPlane.Test(e.X, e.Y) Then

            Dim Scaled = mPlane.UnscalePoint(e)
            StatusLabel.Text = "Точка (" & Scaled.X & "; " & Scaled.Y & ") принадлежит заштрихованной области."
            StatusLabel.ForeColor = Color.Green

        Else

            StatusLabel.Text = "Точка находится вне заштрихованной области."
            StatusLabel.ForeColor = Color.Red

        End If

    End Sub

    Private Sub Surface_MouseClick(sender As Object, e As MouseEventArgs) Handles Surface.MouseClick

        If e.Button = MouseButtons.Left Then
            Call CheckCollision(e.Location)
        End If

    End Sub

    Private Sub Surface_MouseMove(sender As Object, e As MouseEventArgs) Handles Surface.MouseMove

        If e.Button = MouseButtons.Left Then
            Call CheckCollision(e.Location)
        End If

    End Sub
End Class