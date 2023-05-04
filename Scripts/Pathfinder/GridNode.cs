using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pathfinder
{
    public class GridNode : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        [SerializeField] private Vector2 _gridWorldSize; //size of the grid
        [SerializeField] private LayerMask _Obstacles; // layer that contains obstacles
        [SerializeField] private float _nodeRadius; // FIGURE OUT
        private float _nodeDiameter;
        private int _gridSizeX, _gridSizeY;

        private Vector2 worldBottomLeft;

        private Node[,] _grid;

        private void Awake()
        {
            _nodeDiameter = _nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
            worldBottomLeft = new Vector2(transform.position.x - _gridWorldSize.x / 2, transform.position.y - _gridWorldSize.y / 2);

            CreateGrid();
        }

        public int GetMaxSize()
        {
            return _gridSizeX * _gridSizeY;
        }

        public Node GetNodeFromWorldPoint(Vector2 worldPosition)
        {
            Vector2 gridPoint = worldPosition - worldBottomLeft;
            float tmpX = Mathf.Clamp01((gridPoint.x / _gridWorldSize.x)) * (_gridSizeX - 1);
            float tmpY = Mathf.Clamp01((gridPoint.y / _gridWorldSize.y)) * (_gridSizeY - 1);

            int x = Mathf.RoundToInt(tmpX);
            int y = Mathf.RoundToInt(tmpY);

            //Debug.Log("X2: " + x + " Y2: " + y);

            return _grid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    int neignbourX = node.xCoor + x;
                    int neignbourY = node.yCoor + y;
                    if (neignbourX >= 0 && neignbourX < _gridSizeX && neignbourY >= 0 && neignbourY < _gridSizeY)
                        neighbours.Add(_grid[neignbourX, neignbourY]);
                }
            }
            return neighbours;
        }

        /*public Node GetNearestWalkableNode(Node node)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    int neignbourX = node.xCoor + x;
                    int neignbourY = node.yCoor + y;
                    if (neignbourX >= 0 && neignbourX < _gridSizeX && neignbourY >= 0 && neignbourY < _gridSizeY)
                    {
                        if (!_grid[neignbourX, neignbourY].isObstacle)
                        {
                            return _grid[neignbourX, neignbourY];
                        }
                    }
                }
            }
            return null;
        }*/

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];

            Debug.Log("_gridSizeX: " + _gridSizeX + " ;_gridSizeY " + _gridSizeY);
            Debug.Log("Grid center positons: " + transform.position);
            Debug.Log("worldBottomLeft: " + worldBottomLeft);

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector2 worldPoint = new Vector2(worldBottomLeft.x + (x * _nodeDiameter + _nodeRadius), worldBottomLeft.y + (y * _nodeDiameter + _nodeRadius));
                    bool isObstacle = Physics2D.OverlapCircle(worldPoint, _nodeRadius, _Obstacles);
                    _grid[x, y] = new Node(isObstacle, worldPoint, x, y);
                }
            }
        }

        public Node ClosestWalkableNode(Node node)
        {
            int maxRadius = Mathf.Max(_gridSizeX, _gridSizeY) / 2;
            for (int i = 1; i < maxRadius; i++)
            {
                Node n = FindWalkableInRadius(node.xCoor, node.yCoor, i);
                if (n != null)
                {
                    return n;

                }
            }
            return null;
        }
        Node FindWalkableInRadius(int centreX, int centreY, int radius)
        {

            for (int i = -radius; i <= radius; i++)
            {
                int verticalSearchX = i + centreX;
                int horizontalSearchY = i + centreY;

                // top
                if (InBounds(verticalSearchX, centreY + radius))
                {
                    if (!_grid[verticalSearchX, centreY + radius].isObstacle)
                    {
                        return _grid[verticalSearchX, centreY + radius];
                    }
                }

                // bottom
                if (InBounds(verticalSearchX, centreY - radius))
                {
                    if (!_grid[verticalSearchX, centreY - radius].isObstacle)
                    {
                        return _grid[verticalSearchX, centreY - radius];
                    }
                }
                // right
                if (InBounds(centreY + radius, horizontalSearchY))
                {
                    if (!_grid[centreX + radius, horizontalSearchY].isObstacle)
                    {
                        return _grid[centreX + radius, horizontalSearchY];
                    }
                }

                // left
                if (InBounds(centreY - radius, horizontalSearchY))
                {
                    if (!_grid[centreX - radius, horizontalSearchY].isObstacle)
                    {
                        return _grid[centreX - radius, horizontalSearchY];
                    }
                }

            }

            return null;

        }

        bool InBounds(int x, int y)
        {
            return x >= 0 && x < _gridSizeX && y >= 0 && y < _gridSizeY;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, _gridWorldSize);

            if (_grid != null)
            {
                Node playerNode = GetNodeFromWorldPoint(_player.position);
                if (playerNode.isObstacle)
                {
                    playerNode = ClosestWalkableNode(playerNode);
                }
                foreach (Node node in _grid)
                {
                    if (node.worldPosition == playerNode.worldPosition)
                    {
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawCube(node.worldPosition, Vector2.one * (_nodeDiameter - 0.1f));
                    }
                    if (node.isObstacle)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(node.worldPosition, Vector2.one * (_nodeDiameter - 0.1f));
                    }
/*                    if (!node.isObstacle)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(node.worldPosition, Vector2.one * (_nodeDiameter - 0.1f));
                    }*/
                }
            }
        }
    }
}