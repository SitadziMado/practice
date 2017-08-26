using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Graph
    {
        public Graph()
        {

        }

        public void AddVertex(float x, float y)
        {
            AddVertex(new PointF(x, y));
        }

        public void AddVertex(PointF visualCoordinates)
        {
            // Добавляем новую вершину и координаты для ее визуализации.
            mMatrix.Add(new List<bool>());
            mCoords.Add(visualCoordinates);
        }

        public void RemoveVertex(int index)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            // Удаляем вершину.
            mMatrix.RemoveAt(index);
            mCoords.RemoveAt(index);
            mSelected = -1;

            // ToDo: Удалять ребра.
        }

        public void Connect(int firstIndex, int secondIndex)
        {
            if (!CheckVertexIndex(firstIndex) || !CheckVertexIndex(secondIndex))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            if (!IsConnected(firstIndex, secondIndex))
            {
                var first = mMatrix[firstIndex];
                var second = mMatrix[secondIndex];

                // Дополняем до актуального количества вершин.
                while (first.Count < mEdgeCount)
                    first.Add(false);

                while (second.Count < mEdgeCount)
                    second.Add(false);

                ++mEdgeCount;

                // Соединяем.
                first.Add(true);

                if (firstIndex != secondIndex)
                    second.Add(true);

                mEdges.Add(new Tuple<int, int>(firstIndex, secondIndex));
                // mSelected = -1;
            }
        }

        public bool TryConnectTo(int indexTo)
        {
            if (mSelected != -1)
            {
                Connect(mSelected, indexTo);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Disconnect(int edgeIndex)
        {
            if (!CheckEdgeIndex(edgeIndex))
                throw new ArgumentOutOfRangeException("Нет ребра с таким индексом.");

            foreach (var vertex in mMatrix)
            {
                try
                {
                    vertex.RemoveAt(edgeIndex);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }

            mEdges.RemoveAt(edgeIndex);

            // Ребер стало на одно меньше.
            --mEdgeCount;
        }

        public void Draw(Graphics graphics)
        {
            DrawEdges(graphics);
            DrawVertices(graphics);
        }

        public int IndexByCoordinates(PointF coord)
        {
            for (int i = 0; i < mCoords.Count; ++i)
            {
                float x = coord.X - mCoords[i].X;
                float y = coord.Y - mCoords[i].Y;

                if (x * x + y * y <= VertexSize * VertexSize)
                    return i;
            }

            return -1;
        }

        public void Select(int index)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            mSelected = index;
        }

        private bool IsConnected(int firstIndex, int secondIndex)
        {
            try
            {
                var first = mMatrix[firstIndex];
                var second = mMatrix[secondIndex];

                // Проверяем, есть ли соединение между заданными вершинами.
                for (int i = 0; i < first.Count; ++i)
                    if (first[i] == true && second[i] == true)
                        return true;
            }
            catch (ArgumentOutOfRangeException)
            {

            }

            return false;
        }

        private bool CheckVertexIndex(int index)
        {
            if (index < 0 || index >= mMatrix.Count)
                return false;
            else
                return true;
        }

        private bool CheckEdgeIndex(int index)
        {
            if (index < 0 || index >= mEdgeCount)
                return false;
            else
                return true;
        }

        private void DrawEdges(Graphics graphics)
        {
            for (int i = 0; i < mEdgeCount; ++i)
            {
                int firstIndex = mEdges[i].Item1;
                int secondIndex = mEdges[i].Item2;

                graphics.DrawLine(Pens.Black, mCoords[firstIndex], mCoords[secondIndex]);

                /*PointF first = new PointF(), second = new PointF();
                bool firstSet = false;
                bool secondSet = false;

                for (int j = 0; j < mMatrix.Count; ++j)
                {
                    try
                    {
                        if (mMatrix[j][i])
                        {
                            if (!firstSet)
                            {
                                firstSet = true;
                                first = mCoords[j];
                            }
                            else
                            {
                                secondSet = true;
                                second = mCoords[j];
                                break;
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                }

                if (!secondSet)
                    graphics.DrawEllipse(Pens.Black, first.X, first.Y, VertexSize, VertexSize);
                else
                    graphics.DrawLine(Pens.Black, first, second);
                */
            }
        }

        private void DrawVertices(Graphics graphics)
        {
            Font font = new Font("Verdana", VertexSize / 4);
            int index = 1;

            foreach (var vertex in mCoords)
            {
                RectangleF ellipse = new RectangleF(
                    vertex.X - VertexSize / 2,
                    vertex.Y - VertexSize / 2,
                    VertexSize,
                    VertexSize
                );

                graphics.FillEllipse(((index - 1) == mSelected) ? (Brushes.Aquamarine) : (Brushes.LightSeaGreen), ellipse);
                graphics.DrawEllipse(Pens.Black, ellipse);
                graphics.DrawString((index++).ToString(), font, Brushes.Black, vertex.X - VertexSize / 4 + 3, vertex.Y - VertexSize / 4 + 1);
            }
        }

        private const int VertexSize = 32;

        private uint mEdgeCount = 0U;
        private List<List<bool>> mMatrix = new List<List<bool>>();
        private List<PointF> mCoords = new List<PointF>();
        private List<Tuple<int, int>> mEdges = new List<Tuple<int, int>>();
        private int mSelected = -1;
    }
}
