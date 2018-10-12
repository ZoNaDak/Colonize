using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Utility.Algorithm.AStar {
	public class Node {
		public readonly int x;
		public readonly int y;

		public float gCost;
		public float hCost;
		public float fCost { get { return gCost + hCost; } }

		public Node parentNode;

        public Node(int _x, int _y) {
            this.x = _x;
            this.y = _y;
        }
	}

	public struct Grid {
		public int x;
		public int y;

		public Grid(int _x, int _y) {
			this.x = _x;
			this.y = _y;
		}
	}
}