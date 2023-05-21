using Raven.Util;

namespace Raven.Pathfinding.Util.Nodes {
    public class Node {
        public bool Open { get; private set; }

        public Coordinate Coordinates { get; private set; }

        public Node (bool open, Coordinate cords) {
            Open = open;
            Coordinates = cords;
        }
    }
}