using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            StreamWriter sw = new StreamWriter("output.txt");

            int count = Int32.Parse(sr.ReadLine()) - 1;

            mNodes.Add(new Node(null, -2));

            for (int i = 0; i < count; ++i)
            {
                string line = sr.ReadLine();
                string[] input = line.Split(' ');
                string type = input[0];
                int parent = Int32.Parse(input[1]) - 1;
                int winner = -2;

                if (type == "L")
                    winner = Int32.Parse(input[2]);

                Node temp = new Node(mNodes[parent], winner);
                mNodes[parent].AddChild(temp);
                mNodes.Add(temp);
            }

            mNodes[0].FillWinners();

            sw.Write(mNodes[0].ToString());
            sw.Flush();
            sr.Close();
            sw.Close();
        }

        private class Node
        {
            public Node(Node parent, int winner)
            {
                SetWinner(winner);
            }
            
            public Node AddChild(Node rhs)
            {
                mChildren.Add(rhs);
                return this;
            }

            public void FillWinners()
            {
                FillWinnersImpl(this, true);
            }

            public void SetParent(Node parent)
            {
                mParent = parent;
            }

            public Node GetParent()
            {
                return mParent;
            }

            public int GetWinner()
            {
                return mWinner;
            }

            public void SetWinner(int winner)
            {
                mWinner = winner;
            }

            public bool HasChildren()
            {
                return mChildren.Count > 0;
            }

            private static void FillWinnersImpl(Node cur, bool first)
            {
                foreach (var v in cur.mChildren)
                    FillWinnersImpl(v, !first);

                if (cur.HasChildren())
                {
                    int best;

                    if (first)
                    {
                        best = -2;

                        foreach (var v in cur.mChildren)
                            if (best < v.GetWinner())
                                best = v.GetWinner();
                    }
                    else
                    {
                        best = +2;

                        foreach (var v in cur.mChildren)
                            if (best > v.GetWinner())
                                best = v.GetWinner();
                    }

                    cur.SetWinner(best);
                }
            }

            public override string ToString()
            {
                switch (GetWinner())
                {
                    case -1:
                        return "-1";

                    case 0:
                        return "0";

                    case +1:
                        return "+1";

                    default:
                        return base.ToString();
                }
            }

            private int mWinner;
            private List<Node> mChildren = new List<Node>();
            private Node mParent = null;
        }

        private static List<Node> mNodes = new List<Node>();
    }
}
