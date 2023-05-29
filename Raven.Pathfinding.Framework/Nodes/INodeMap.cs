namespace Raven.Pathfinding.Framework.Nodes {
    public interface INodeMap {
        Node[ , ] Map { get; }

        void SetNode (Node node);
    }
}