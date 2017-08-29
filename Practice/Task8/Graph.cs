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
            mVertices.Add(new Vertex(visualCoordinates));
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
            SelectedIndex = -1;

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
            if (SelectedIndex != -1)
            {
                Connect(SelectedIndex, indexTo);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryConnect(int firstIndex, int secondIndex)
        {
            if (firstIndex != -1 && secondIndex != -1)
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
            int selected = -1;

            for (int i = 0; i < mVertices.Count; ++i)
            {
                float x = coord.X - mVertices[i].Location.X;
                float y = coord.Y - mVertices[i].Location.Y;

                if (x * x + y * y <= VertexSize * VertexSize)
                    selected = i;
            }

            return selected;
        }

        public void Select(int index)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            SelectedIndex = index;
        }

        public void Reposition(int index, PointF location)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            mVertices[index].Location = location;
        }

        public void Offset(int index, PointF offset)
        {
            if (!CheckVertexIndex(index))
                throw new ArgumentOutOfRangeException("Нет вершины с таким индексом.");

            var pt = new PointF(mVertices[index].Location.X + offset.X, mVertices[index].Location.Y + offset.Y);

            mVertices[index].Location = pt;
        }

        public bool HighlightEmptyGraph(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Количество вершин должно быть натуральным числом.");

            foreach (var v in mVertices)
                v.Color = Vertex.DefaultColor;

            var queue = new Queue<int>();

            for (int i = 0; i < mMatrix.Count && queue.Count < count; ++i)
            {
                if (!HasEdges(i))
                    queue.Enqueue(i);
            }

            if (queue.Count == count)
            {
                while (queue.Count > 0)
                    mVertices[queue.Dequeue()].Color = Vertex.HighlightColor;

                return true;
            }

            return false;
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

                graphics.DrawLine(Pens.Black, mVertices[firstIndex].Location, mVertices[secondIndex].Location);
            }
        }

        private void DrawVertices(Graphics graphics)
        {
            Font font = new Font("Verdana", VertexSize / 2);
            int index = 1;

            foreach (var vertex in mVertices)
            {
                RectangleF ellipse = new RectangleF(
                    vertex.Location.X - VertexSize,
                    vertex.Location.Y - VertexSize,
                    2.0f * VertexSize,
                    2.0f * VertexSize
                );

                graphics.FillEllipse(
                    new SolidBrush(vertex.Color),
                    ellipse
                );

                graphics.DrawEllipse(Pens.Black, ellipse);

                graphics.DrawString(
                    (index++).ToString(), 
                    font, 
                    Brushes.Black, 
                    vertex.Location.X - VertexSize / 4, 
                    vertex.Location.Y - VertexSize / 4 - 2
                );
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

        private bool HasEdges(int index)
        {
            var vertex = mMatrix[index];
            int edge = 0;
            bool result = false;

            foreach (var v in vertex)
                if (result |= v)
                    break;

            return result;
        }

        private class Vertex
        {
            public Vertex(PointF location)
            {
                Location = location;
                Color = DefaultColor;
            }

            public Vertex(PointF location, Color color)
            {
                Location = location;
                Color = color;
            }

            public PointF Location { get; set; }
            public Color Color { get; set; }
            public static Color DefaultColor => Color.Aquamarine;
            public static Color HighlightColor => Color.PaleVioletRed;
            public static Color SelectedColor => Color.ForestGreen;
        }

        public int VertexCount => mMatrix.Count;
        public int SelectedIndex
        {
            get
            {
                return mSelected;
            }
            internal set
            {
                if (mSelected != -1 && CheckVertexIndex(mSelected))
                    mVertices[mSelected].Color = Vertex.DefaultColor;

                mSelected = value;

                if (mSelected != -1 && CheckVertexIndex(mSelected))
                    mVertices[mSelected].Color = Vertex.SelectedColor;
            }
        }

        private const int VertexSize = 16;

        private uint mEdgeCount = 0U;
        private List<List<bool>> mMatrix = new List<List<bool>>();
        private List<Vertex> mVertices = new List<Vertex>();
        private List<Tuple<int, int>> mEdges = new List<Tuple<int, int>>();
        private int mSelected = -1;
        private bool mCacheFlag = false;
    }
}
