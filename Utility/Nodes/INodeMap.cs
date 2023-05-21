namespace Raven.Pathfinding.Util.Nodes {
    public interface INodeMap {
        Node[ , ] Map { get; }

        void SetNode (Node node);
    }
}