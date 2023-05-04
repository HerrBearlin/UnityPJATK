using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinder
{
    public class Pathfinder : MonoBehaviour
    {
        private GridNode _grid;
        private static Pathfinder instance;

        private void Awake()
        {
            _grid = GetComponent<GridNode>();
            if (instance != null && instance != this)
            {
                Destroy(instance);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
        }

        public static Vector2[] RequestPath(Vector2 from, Vector2 to)
        {
            return instance.GetPath(from, to);
        }

        private Vector2[] GetPath(Vector2 startPos, Vector2 goalPos)
        {
            Node startNode = _grid.GetNodeFromWorldPoint(startPos);
            Node goalNode = _grid.GetNodeFromWorldPoint(goalPos);

            if (goalNode.isObstacle)
            {
                Debug.Log("PLAYER IS OBSTACLE");
                goalNode = _grid.ClosestWalkableNode(goalNode);
            }

            HashSet<Node> visitedNodes = new HashSet<Node>();
            BinaryHeap waitingNodes = new BinaryHeap(_grid.GetMaxSize());

            waitingNodes.Add(startNode);

            while (waitingNodes.currentNodeCount > 0)
            {
                Node currentNode = waitingNodes.Extract();
                visitedNodes.Add(currentNode);

                if (currentNode == goalNode)
                {
                    return RetrievePath(startNode, goalNode);
                }

                foreach (Node neighbour in _grid.GetNeighbours(currentNode))
                {
                    if (neighbour.isObstacle || visitedNodes.Contains(neighbour))
                    {
                        continue;
                    }

                    int tmp_G = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (tmp_G < neighbour.gCost || !waitingNodes.Contains(neighbour))
                    {
                        neighbour.gCost = tmp_G;
                        neighbour.hCost = GetDistance(neighbour, goalNode);
                        neighbour.parent = currentNode;

                        if (!waitingNodes.Contains(neighbour))
                            waitingNodes.Add(neighbour);
                        else
                            waitingNodes.UpdateNode(neighbour);
                    }
                }
            }
            return new Vector2[0];
        }

        private Vector2[] RetrievePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();

            Vector2 currentDirection;
            Vector2 previousDirection = Vector2.zero;
            List<Vector2> points = new List<Vector2>();

            Vector2[] pathPoints;

            while (endNode != startNode)
            {
                path.Add(endNode);
                endNode = endNode.parent;
            }

            for (int i = 1; i < path.Count; i++)
            {
                currentDirection = new Vector2(path[i].xCoor - path[i - 1].xCoor, path[i].yCoor - path[i - 1].yCoor);
                if (currentDirection != previousDirection)
                {
                    points.Add(path[i].worldPosition);
                }
                previousDirection = currentDirection;
            }

            pathPoints = points.ToArray();
            Array.Reverse(pathPoints);
            return pathPoints;
        }

        private int GetDistance(Node node1, Node node2)
        {
            int distanceX = Mathf.Abs(node1.xCoor - node2.xCoor);
            int distanceY = Mathf.Abs(node1.yCoor - node2.yCoor);
            int remainnig = Mathf.Abs(distanceX - distanceY);
            return 14 * Mathf.Min(distanceX, distanceY) + 10 * remainnig;
        }
    }

}