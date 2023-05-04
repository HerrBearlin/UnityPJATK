using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinder
{
    public class Node : IComparable
    {
        public bool isObstacle { get; private set; }
        public Vector2 worldPosition { get; private set; }

        public int xCoor { get; private set; }
        public int yCoor { get; private set; }

        public int gCost { get; set; } // distance travelled
        public int hCost { get; set; } // distance to travel
        public Node parent { get; set; }
        public int index { get; set; }

        public Node(bool isObstacle, Vector2 worldPosition, int xCoor, int yCoor)
        {
            this.isObstacle = isObstacle;
            this.worldPosition = worldPosition;
            this.xCoor = xCoor;
            this.yCoor = yCoor;
        }

        public int fCost
        {
            get { return gCost + hCost; }
        }

        public int CompareTo(object obj)
        {
            Node other = (Node)obj;
            int result = fCost.CompareTo(other.fCost);
            if (result == 0)
            {
                result = hCost.CompareTo(other.hCost);
            }
            return -result;
        }
    }
}
