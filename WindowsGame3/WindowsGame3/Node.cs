using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Node
    {
        private Vector2 mPosition;
        private Node mParent;

        public Node(Vector2 position, int cost, int estimated, Node parent)
        {
            mPosition = position;
            CostFromA = cost;
            EstimatedCostToB = estimated;
            Total = CostFromA + EstimatedCostToB;
            mParent = parent;
        }

        /// F Score
        public int Total { get; set; }
        /// G Score
        public int CostFromA { get; set; }
        /// H Score
        public int EstimatedCostToB { get; set; }

        public int X { get { return (int)mPosition.X; } }
        public int Y { get { return (int)mPosition.Y; } }

        public Vector2 Position
        {
            get { return mPosition; }
        }

        public Node Parent
        {
            get { return mParent; }
        }
    }
}
