using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Utility.Algorithm.AStar {
	public static class AStarManager {
		private static Grid grid;
		private static List<List<Node>> nodeMap = new List<List<Node>>();

		public static void Awake(int _nodeNumX, int _nodeNumY) {
			grid = new Grid(_nodeNumX, _nodeNumY);
			for(int i = 0; i < _nodeNumX; ++i) {
				nodeMap.Add(new List<Node>());
				for(int j = 0; j < _nodeNumY; ++j) {
					nodeMap[i].Add(new Node(i, j));
				}
			}
			GetNeighbourNodes(nodeMap[2][3]);
		}

		private static List<Node> GetNeighbourNodes(Node _node) {
			//Four Direction
			List<Node> returnNodeList = new List<Node>();

			int posX;
			int posY;
			for(int x = -1; x <= 1; ++x) {
				posX = _node.x + x;
				if(posX < 0 || posX >= grid.x) {
					//OutXRange
					continue;
				}
				if(posX == _node.x ) {
					//CurrentNode;
					continue;
				}
				returnNodeList.Add(nodeMap[posX][_node.y]);
			}
			for(int y = -1; y <= 1; ++y) {
				posY = _node.y + y;
				if(posY == _node.y) {
					//CurrentNode;
					continue;
				}
				if(posY < 0 || posY >= grid.y) {
					//OutYRange
					continue;
				}
				returnNodeList.Add(nodeMap[_node.x][posY]);
			}

			return returnNodeList;
		}

		private static List<Node> RetraceNode(Node startNode, Node destNode) {
			List<Node> returnPath = new List<Node>();
			Node currentNode = destNode;

			while(currentNode != startNode) {
				returnPath.Add(currentNode);
				currentNode = currentNode.parentNode;
			}
			returnPath.Reverse();

			return returnPath;
		}

		private static float GetHCost(Node _nodeA, Node _nodeB) {
			return Vector2.Distance(new Vector2(_nodeA.x,_nodeB.y), new Vector2(_nodeB.x, _nodeB.y));
		}

		public static List<Node> FindPath(Vector2Int _start, Vector2Int _dest) {
			List<Node> foundNodes = new List<Node>();

			List<Node> openNode = new List<Node>();
			HashSet<Node> closedNode = new HashSet<Node>();

			Node startNode = nodeMap[_start.x][_start.y];
			Node destNode = nodeMap[_dest.x][_dest.y];

			openNode.Add(startNode);

			while(openNode.Count > 0) {
				Node currentNode = openNode[0];

				for(int i = 0; i < openNode.Count; ++i) {
					if(openNode[i].fCost < currentNode.fCost ||
					(openNode[i].fCost == currentNode.fCost && openNode[i].hCost < currentNode.hCost)) {
						if(!currentNode.Equals(openNode[i])) {
							currentNode = openNode[i];
						}
					}
				}

				openNode.Remove(currentNode);
				closedNode.Add(currentNode);

				if(currentNode.Equals(destNode)) {
					foundNodes = RetraceNode(startNode, destNode);
					break;
				}

				List<Node> neighbourList = GetNeighbourNodes(currentNode);
				for(int i = 0; i < neighbourList.Count; ++i) {
					if(!closedNode.Contains(neighbourList[i])) {
						float moveCost = currentNode.gCost + GetHCost(currentNode, neighbourList[i]);

						if(moveCost < neighbourList[i].gCost || !openNode.Contains(neighbourList[i])) {
							neighbourList[i].gCost = moveCost;
							neighbourList[i].hCost = GetHCost(neighbourList[i], destNode);

							neighbourList[i].parentNode = currentNode;

							if(!openNode.Contains(neighbourList[i])) {
								openNode.Add(neighbourList[i]);
							}
						}
					}
				}
			}

			return foundNodes;
		}
	}
}
