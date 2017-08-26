using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task8
{
    public partial class MainForm : Form
    {
        private Graph mGraph = new Graph();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            mGraph.Draw(e.Graphics);
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            int selected = mGraph.IndexByCoordinates(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (selected == -1)
                {
                    mGraph.AddVertex(e.Location);
                }
                else if ((ModifierKeys & Keys.Control) != Keys.None)
                {
                    mGraph.TryConnectTo(selected);
                }
                else
                {
                    mGraph.Select(selected);
                }
            }
            else if (e.Button == MouseButtons.Right && selected != -1)
            {
                mGraph.RemoveVertex(selected);
            }

            Invalidate();
        }
    }
}
