namespace Raven.Pathfinding.Util.Nodes {
    public class NodeMap : INodeMap {
        public Node[ , ] Map { get; private set; }

        public NodeMap (int size) {
            Map = new Node[size, size];
        }

        public void SetNode (Node node) => Map[(int) node.Coordinates.X, (int) node.Coordinates.Y] = node;
    }
}