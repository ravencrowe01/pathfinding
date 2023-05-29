namespace Raven.Pathfinding.Framework.Nodes {
    public class PathNode {
        public Node Node { get; private set; }
        public PathNode? Parent { get; private set; }
        public float g { get; private set; }
        public float h { get; private set; }
        public float f => g + h;

        public PathNode (Node node, PathNode? parent, float g, float h) {
            Node = node;
            Parent = parent;
            this.g = g;
            this.h = h;
        }
    }
}