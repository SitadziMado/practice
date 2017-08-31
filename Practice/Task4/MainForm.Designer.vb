<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.ComputeButton = New System.Windows.Forms.Button()
        Me.SourceTextBox = New System.Windows.Forms.TextBox()
        Me.ResultTextBox = New System.Windows.Forms.TextBox()
        Me.ResultLabel = New System.Windows.Forms.Label()
        Me.ComputeProgress = New System.Windows.Forms.ProgressBar()
        Me.ComputeFactorial = New System.ComponentModel.BackgroundWorker()
        Me.DiagnosticsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CancelButton, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ComputeButton, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.SourceTextBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ResultTextBox, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ResultLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ComputeProgress, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(624, 442)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CancelButton
        '
        Me.CancelButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CancelButton.Enabled = False
        Me.CancelButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.CancelButton.Location = New System.Drawing.Point(3, 409)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(618, 30)
        Me.CancelButton.TabIndex = 2
        Me.CancelButton.Text = "Прервать расчёт"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'ComputeButton
        '
        Me.ComputeButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ComputeButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ComputeButton.Location = New System.Drawing.Point(3, 373)
        Me.ComputeButton.Name = "ComputeButton"
        Me.ComputeButton.Size = New System.Drawing.Size(618, 30)
        Me.ComputeButton.TabIndex = 1
        Me.ComputeButton.Text = "Вычислить факториал (может занять время)"
        Me.ComputeButton.UseVisualStyleBackColor = True
        '
        'SourceTextBox
        '
        Me.SourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SourceTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.SourceTextBox.ForeColor = System.Drawing.Color.Green
        Me.SourceTextBox.Location = New System.Drawing.Point(3, 3)
        Me.SourceTextBox.MaxLength = 3
        Me.SourceTextBox.Name = "SourceTextBox"
        Me.SourceTextBox.Size = New System.Drawing.Size(618, 23)
        Me.SourceTextBox.TabIndex = 0
        Me.SourceTextBox.Text = "100"
        '
        'ResultTextBox
        '
        Me.ResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ResultTextBox.Location = New System.Drawing.Point(3, 48)
        Me.ResultTextBox.Multiline = True
        Me.ResultTextBox.Name = "ResultTextBox"
        Me.ResultTextBox.ReadOnly = True
        Me.ResultTextBox.Size = New System.Drawing.Size(618, 283)
        Me.ResultTextBox.TabIndex = 2
        Me.ResultTextBox.Text = "Нажмите кнопку вычисления"
        '
        'ResultLabel
        '
        Me.ResultLabel.AutoSize = True
        Me.ResultLabel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ResultLabel.Location = New System.Drawing.Point(3, 29)
        Me.ResultLabel.Name = "ResultLabel"
        Me.ResultLabel.Size = New System.Drawing.Size(82, 16)
        Me.ResultLabel.TabIndex = 3
        Me.ResultLabel.Text = "Результат:"
        '
        'ComputeProgress
        '
        Me.ComputeProgress.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ComputeProgress.Location = New System.Drawing.Point(3, 337)
        Me.ComputeProgress.Name = "ComputeProgress"
        Me.ComputeProgress.Size = New System.Drawing.Size(618, 30)
        Me.ComputeProgress.TabIndex = 4
        '
        'ComputeFactorial
        '
        Me.ComputeFactorial.WorkerReportsProgress = True
        Me.ComputeFactorial.WorkerSupportsCancellation = True
        '
        'DiagnosticsTimer
        '
        Me.DiagnosticsTimer.Interval = 500
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "MainForm"
        Me.Text = "Задание 4: вычисление факториала"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ComputeButton As Button
    Friend WithEvents SourceTextBox As TextBox
    Friend WithEvents ResultTextBox As TextBox
    Friend WithEvents ResultLabel As Label
    Friend WithEvents ComputeFactorial As System.ComponentModel.BackgroundWorker
    Friend WithEvents ComputeProgress As ProgressBar
    Friend WithEvents CancelButton As Button
    Friend WithEvents DiagnosticsTimer As Timer
End Class
