using Raven.Util;

namespace Raven.Pathfinding.Framework.Nodes {
    public class Node {
        public bool Open { get; private set; }

        public Coordinate Coordinates { get; private set; }

        public Node (bool open, Coordinate cords) {
            Open = open;
            Coordinates = cords;
        }

        public static bool operator == (Node a, Node b) => a.Open == b.Open && a.Coordinates == b.Coordinates;
        public static bool operator != (Node a, Node b) => !(a == b);

        public override bool Equals (object? obj) {
            if(obj is not Node) {
                return false;
            }

            var o = obj as Node;

            return o?.Open == Open && o.Coordinates == Coordinates;
        }

        public override int GetHashCode ( ) {
            return base.GetHashCode();
        }

        public override string? ToString ( ) {
            return base.ToString();
        }
    }
}