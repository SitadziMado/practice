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
            mVertices.Add(visualCoordinates);
        }

        public void RemoveVertex(int index)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            // Удаляем ребра.
            RemoveEdges(index);

            // Удаляем вершину.
            mMatrix.RemoveAt(index);
            mVertices.RemoveAt(index);
            mSelected = -1;

            // Устанавливаем флаг для перекэширования.
            mCacheFlag = true;
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

        public bool TryConnect(int firstIndex, int secondIndex)
        {
            if (mSelected != -1)
            {
                Connect(firstIndex, secondIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveEdge(int edgeIndex)
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
            for (int i = 0; i < mVertices.Count; ++i)
            {
                float x = coord.X - mVertices[i].X;
                float y = coord.Y - mVertices[i].Y;

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

        public void Reposition(int index, PointF location)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            mVertices[index] = location;
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
            Recache();

            for (int i = 0; i < mEdgeCount; ++i)
            {
                int firstIndex = mEdges[i].Item1;
                int secondIndex = mEdges[i].Item2;

                graphics.DrawLine(Pens.Black, mVertices[firstIndex], mVertices[secondIndex]);
            }
        }

        private void DrawVertices(Graphics graphics)
        {
            Font font = new Font("Verdana", VertexSize / 4);
            int index = 1;

            foreach (var vertex in mVertices)
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

        private void RemoveEdges(int vertexIndex)
        {
            if (!CheckVertexIndex(vertexIndex))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            var vertex = mMatrix[vertexIndex];
            var edgesToRemove = new Stack<int>();

            for (int i = 0; i < vertex.Count; ++i)
                if (vertex[i])
                    edgesToRemove.Push(i);

            while (edgesToRemove.Count > 0)
                RemoveEdge(edgesToRemove.Pop());
        }

        private void Recache()
        {
            if (mCacheFlag)
            {
                mEdges.Clear();

                for (int i = 0; i < mEdgeCount; ++i)
                {
                    int first = -1;
                    int second = -1;
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
                                    first = j;
                                }
                                else
                                {
                                    secondSet = true;
                                    second = j;
                                    break;
                                }
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {

                        }
                    }

                    mEdges.Add(new Tuple<int, int>(first, second));
                }

                mCacheFlag = false;
            }
        }

        public int VertexCount { get { return mMatrix.Count; } } 
        public int SelectedIndex { get { return mSelected; } }

        private const int VertexSize = 32;

        private uint mEdgeCount = 0U;
        private List<List<bool>> mMatrix = new List<List<bool>>();
        private List<PointF> mVertices = new List<PointF>();
        private List<Tuple<int, int>> mEdges = new List<Tuple<int, int>>();
        private int mSelected = -1;
        private bool mCacheFlag = false;
    }
}
