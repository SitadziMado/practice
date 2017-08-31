namespace Task7
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AddControlBitsButton = new System.Windows.Forms.Button();
            this.RawTextBox = new System.Windows.Forms.TextBox();
            this.HammingRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.AddControlBitsButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.RawTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.HammingRichTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 462);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // AddControlBitsButton
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.AddControlBitsButton, 2);
            this.AddControlBitsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddControlBitsButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddControlBitsButton.Location = new System.Drawing.Point(3, 429);
            this.AddControlBitsButton.Name = "AddControlBitsButton";
            this.AddControlBitsButton.Size = new System.Drawing.Size(978, 30);
            this.AddControlBitsButton.TabIndex = 1;
            this.AddControlBitsButton.Text = "Добавить и заполнить контрольные биты";
            this.AddControlBitsButton.UseVisualStyleBackColor = true;
            this.AddControlBitsButton.Click += new System.EventHandler(this.AddControlBitsButton_Click);
            // 
            // RawTextBox
            // 
            this.RawTextBox.BackColor = System.Drawing.Color.White;
            this.RawTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RawTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RawTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RawTextBox.Location = new System.Drawing.Point(3, 39);
            this.RawTextBox.Multiline = true;
            this.RawTextBox.Name = "RawTextBox";
            this.RawTextBox.Size = new System.Drawing.Size(486, 384);
            this.RawTextBox.TabIndex = 0;
            this.RawTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RawTextBox_KeyPress);
            // 
            // HammingRichTextBox
            // 
            this.HammingRichTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.HammingRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HammingRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HammingRichTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HammingRichTextBox.Location = new System.Drawing.Point(495, 39);
            this.HammingRichTextBox.Name = "HammingRichTextBox";
            this.HammingRichTextBox.ReadOnly = true;
            this.HammingRichTextBox.Size = new System.Drawing.Size(486, 384);
            this.HammingRichTextBox.TabIndex = 1;
            this.HammingRichTextBox.TabStop = false;
            this.HammingRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(563, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите строку из `0` и `1` для добавления контрольных битов:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Исходная информация:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(495, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(458, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Код Хэмминга (контрольные биты выделены цветом):";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "MainForm";
            this.Text = "Задание 7: код Хэмминга";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox RawTextBox;
        private System.Windows.Forms.RichTextBox HammingRichTextBox;
        private System.Windows.Forms.Button AddControlBitsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

