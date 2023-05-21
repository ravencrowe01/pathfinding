using Raven.Pathfinding.Util.Nodes;

namespace Raven.Pathfinding.Framework {
    public interface IPathfinder {
        IEnumerable<Node> BuildPath ( );
        IEnumerable<Node> GeneratePath ( );
        PathfindingStatus Step ( );
    }
}
