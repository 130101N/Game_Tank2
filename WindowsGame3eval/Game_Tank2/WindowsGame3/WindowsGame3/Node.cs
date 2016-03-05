using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsGame3
{
    // represents a single tile in a map
    public class Node
    {
        private Node parentNode;

        public Point Location { get; private set; }     // The node's location in the map

        public bool IsWalkable { get; set; }        // True when the node may be traversed, otherwise false

        public float G { get; private set; }        // Cost from start point to here

        public float H { get; private set; }        // Estimated cost from here to end point

        public NodeState State { get; set; }        

        public float F
        {
            get { return this.G + this.H; }     //total cost (F = G + H)
        }
        public Node ParentNode
        {
            get { return this.parentNode; }
            set
            {
                //calculate the 'G' value)
                this.parentNode = value;
                this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location);
            }
        }
        
        public Node(int x, int y, bool isWalkable, Point endLocation)
        {
            this.Location = new Point(x, y);
            this.State = NodeState.Untested;
            this.IsWalkable = isWalkable;
            this.H = GetTraversalCost(this.Location, endLocation);
            this.G = 0;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}: {2}", this.Location.X, this.Location.Y, this.State);
        }

        internal static float GetTraversalCost(Point location, Point otherLocation)     // Gets the distance between two points
        {
            float deltaX = otherLocation.X - location.X;
            float deltaY = otherLocation.Y - location.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
