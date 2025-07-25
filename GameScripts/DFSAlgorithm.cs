using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePF2D
{
    public class DFSAlgorithm
    {
        private SimplePathFinding2D pf;
        private Stack<NavNode> stack;
        private Dictionary<NavNode, NavNode> cameFrom;
        private List<Vector3Int> pathPoints;
        private bool pathFound;
        private GameObject debugMarkerPrefab;
        private List<GameObject> debugMarkers = new List<GameObject>();

        public DFSAlgorithm(SimplePathFinding2D newPf, GameObject markerPrefab)
        {
            pf = newPf;
            stack = new Stack<NavNode>();
            cameFrom = new Dictionary<NavNode, NavNode>();
            pathPoints = new List<Vector3Int>();
            pathFound = false;
            debugMarkerPrefab = markerPrefab;
        }

        public bool CreatePath(NavNode startNode, NavNode endNode, NavNode lastNode1, NavNode lastNode2)
        {
            if (startNode == null || endNode == null || startNode.IsIgnorable() || endNode.IsIgnorable())
            {
                return false;
            }

            ClearDebugMarkers();
            stack.Clear();
            cameFrom.Clear();
            pathPoints.Clear();
            pathFound = false;

            stack.Push(startNode);
            cameFrom[startNode] = null;

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (current == endNode)
                {
                    pathFound = true;
                    break;
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (!cameFrom.ContainsKey(neighbor) && neighbor != lastNode1 && neighbor != lastNode2)
                    {
                        stack.Push(neighbor);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            if (pathFound)
            {
                UnpackPath(endNode);
                AddDebugMarkers(Color.green);
                return true;
            }
            return false;
        }

        private IEnumerable<NavNode> GetNeighbors(NavNode node)
        {
            var directions = new DiagonalDirectionEnum[]
            {
                DiagonalDirectionEnum.Up,
                DiagonalDirectionEnum.Down,
                DiagonalDirectionEnum.Left,
                DiagonalDirectionEnum.Right
            };

            foreach (var dir in directions)
            {
                int offsetX = 0, offsetY = 0;
                Direction.GetOffset(dir, ref offsetX, ref offsetY);
                var neighborPos = new Vector3Int(node.GetIndex().x + offsetX, node.GetIndex().y + offsetY, 0);
                var neighbor = pf.GetNode(neighborPos);

                if (neighbor != null && !neighbor.IsIgnorable())
                {
                    yield return neighbor;
                }
            }
        }

        private void UnpackPath(NavNode endNode)
        {
            var current = endNode;
            while (current != null)
            {
                pathPoints.Add(current.GetIndex());
                current = cameFrom[current];
            }
            pathPoints.Reverse();
        }

        public List<Vector3Int> GetPathPoints()
        {
            return pathPoints;
        }

        public SimplePathFinding2D GetPathFinding()
        {
            return pf;
        }

        private void AddDebugMarkers(Color color)
        {
            foreach (var point in pathPoints)
            {
                GameObject marker = Object.Instantiate(debugMarkerPrefab, pf.NavToWorld(point), Quaternion.identity);
                marker.GetComponent<SpriteRenderer>().color = color;
                debugMarkers.Add(marker);
            }
        }

        public void ClearDebugMarkers()
        {
            foreach (var marker in debugMarkers)
            {
                Object.Destroy(marker);
            }
            debugMarkers.Clear();
        }

        public void RemoveSingleDebugMarker(Vector3 position)
        {
            for (int i = 0; i < debugMarkers.Count; i++)
            {
                if (debugMarkers[i].transform.position == position)
                {
                    Object.Destroy(debugMarkers[i]);
                    debugMarkers.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
