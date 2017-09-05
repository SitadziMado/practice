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
        private int mPreviouslySelected = -1;
        private int mSelected = -1;
        private Point mTouchLocation;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            int selected = mGraph.IndexByCoordinates(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (selected == -1)
                {
                    mGraph.AddVertex(e.Location);
                    selected = mGraph.VertexCount - 1;
                }

                if ((ModifierKeys & Keys.Control) != Keys.None)
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

            Canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            mGraph.Draw(e.Graphics);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                case MouseButtons.Right:
                case MouseButtons.Middle:
                    mTouchLocation = e.Location;
                    mPreviouslySelected = mSelected;
                    mSelected = mGraph.IndexByCoordinates(e.Location);
                    if (mSelected != -1)
                        mGraph.Select(mSelected);
                    Canvas.Invalidate();
                    break;
                default:
                    break;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (mSelected != -1)
                    {
                        mGraph.Offset(
                            mSelected, 
                            new Point(
                                e.Location.X - mTouchLocation.X, 
                                e.Location.Y - mTouchLocation.Y
                            )
                        );

                        mTouchLocation = e.Location;
                        Canvas.Invalidate();
                    }
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
                default:
                    break;
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (mSelected == -1)
                        mGraph.AddVertex(e.Location);
                    else if ((ModifierKeys & Keys.Control) != Keys.None)
                        mGraph.TryConnect(mPreviouslySelected, mSelected);

                    Canvas.Invalidate();
                    break;
                case MouseButtons.Right:
                    if (mSelected != -1)
                    {
                        mGraph.RemoveVertex(mSelected);
                        Canvas.Invalidate();
                    }
                    break;
                case MouseButtons.Middle:
                    break;
                default:
                    break;
            }


        }

        private void CountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '\b':
                    break;

                default:
                    e.Handled = true;
                    break;
            }
        }

        private void HighlightEmptyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!mGraph.HighlightEmptyGraph(Int32.Parse(CountTextBox.Text)))
                    MessageBox.Show(
                        "Пустой подграф не найден.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );

                Canvas.Invalidate();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show(
                    "Размер пустого подграфа должен быть натуральным числом.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show(
@"Для создания вершины щелкните левой кнопкой по полю.
Для соединения вершины выделите ее левой кнопкой, зажмите Ctrl, щелкните по другой.
Для удаления вершины щелкните по ней правой кнопкой
",
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
