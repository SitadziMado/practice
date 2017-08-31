using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task9
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            mList = IntList.MakeProgressive((int)SizeUpDown.Value);
            RefreshContent();
            UnlockCtl();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            var index = mList.Find((int)ValueUpDown.Value);

            if (index == null)
                MessageBox.Show(
                    "Элемент не найден.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            else
                MessageBox.Show(
                    String.Format(
                        "Элемент найден на позиции {0}.", 
                        index.Value + 1
                    ),
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (!mList.Remove((int)ValueUpDown.Value))
                MessageBox.Show(
                    "Элемент для удаления не найден.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            else
                RefreshContent();

            if (mList.Count == 0)
                LockCtl();
        }

        private void RefreshContent()
        {
            ContentTextBox.Text = mList.ToString();
        }

        private void LockCtl()
        {
            FindButton.Enabled = false;
            RemoveButton.Enabled = false;
            ValueUpDown.Enabled = false;
        }

        private void UnlockCtl()
        {
            FindButton.Enabled = true;
            RemoveButton.Enabled = true;
            ValueUpDown.Enabled = true;
        }

        private IntList mList;
    }
}
