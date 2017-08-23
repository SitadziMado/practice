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

        public void AddVertex(Point visualCoordinates)
        {
            mMatrix.Add(new List<bool>());
            mCoords.Add(visualCoordinates);
        }

        public void Connect(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || secondIndex < 0 ||
                firstIndex >= mMatrix.Count || secondIndex >= mMatrix.Count)
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
                second.Add(true);
            }
        }

        public void Disconnect(int edgeIndex)
        {
            if (edgeIndex < 0 || edgeIndex >= mEdgeCount)
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

            --mEdgeCount;
        }

        private bool IsConnected(int firstIndex, int secondIndex)
        {
            try
            {
                var first = mMatrix[firstIndex];
                var second = mMatrix[secondIndex];

                for (int i = 0; i < first.Count; ++i)
                    if (first[i] == true && second[i] == true)
                        return true;
            }
            catch (IndexOutOfRangeException)
            {
                
            }

            return false;
        }

        private uint mEdgeCount = 0U;
        private List<List<bool>> mMatrix = new List<List<bool>>();
        private List<Point> mCoords = new List<Point>();
    }
}
