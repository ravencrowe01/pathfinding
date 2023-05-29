using Raven.Pathfinding.Framework.Nodes;

namespace Raven.Pathfinding.Framework {
    public interface IPathfinder {
        PathfindingStatus Status { get; }
        IEnumerable<Node> BuildPath ( );
        PathfindingStatus Step ( );
    }
}
