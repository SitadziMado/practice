using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task7
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void RawTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void AddControlBitsButton_Click(object sender, EventArgs e)
        {
            if (RawTextBox.TextLength > 0)
            {
                var hb = new HammingBuffer(
                    from v
                    in RawTextBox.Text
                    where true
                    select v == '1' ? true : false
                );

                HammingRichTextBox.Text = hb.ToString();
                Colorize();
            }
            else
            {
                MessageBox.Show(
                    "В поле ввода нет ни одного символа. Введите что-либо.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
        }

        private void Colorize()
        {
            int color = 0;
            int factor = FactorMax;

            for (int i = 1; i < HammingRichTextBox.TextLength; i <<= 1)
            {
                int index = i - 1;

                HammingRichTextBox.Select(index, 1);

                HammingRichTextBox.SelectionColor = Color.FromArgb(
                    (color & ColorRed) * factor,
                    ((color & ColorGreen) >> 1) * factor,
                    ((color & ColorBlue) >> 2) * factor
                );

                if (++color == ColorMax)
                {
                    color = 0;
                    if ((factor >>= 1) == 0)
                        break;
                }
            }
        }

        private const int ColorMax = 8;
        private const int FactorMax = 255;
        private const int ColorRed = 1;
        private const int ColorGreen = 2;
        private const int ColorBlue = 4;
    }
}
