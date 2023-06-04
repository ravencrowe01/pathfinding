using Raven.Pathfinding.Framework;
using Raven.Pathfinding.Framework.Config;
using Raven.Pathfinding.Framework.Navigation;
using Raven.Pathfinding.Framework.Nodes;
using Raven.Util;

namespace Raven.Pathfinding.AStar {
    public class AStarPathfinder : IPathfinder {
        private PathNode _start;
        private Node _target;
        private Node[ , ] _map;
        private bool _checkDiags;

        private PathNode _current;

        private PriorityQueue<PathNode, float> _open;
        private IList<PathNode> _closed;

        public PathfindingStatus Status { get; private set; }

        public AStarPathfinder (IPathfinderConfig config) {
            _start = new PathNode(config.Start, null, 0f, 0f);
            _target = config.Target;
            _map = config.Map;

            _current = _start;

            _open = new PriorityQueue<PathNode, float>();
            _closed = new List<PathNode>();

            Status = PathfindingStatus.Searching;
        }

        public PathfindingStatus Step ( ) {
            if (Status != PathfindingStatus.Searching) {
                return Status;
            }

            var currentCords = _current.Node.Coordinates;
            for (int i = 0; i < (_checkDiags ? 8 : 4); i++)
                foreach (Direction dir in Enum.GetValues(typeof(Direction))) {
                    var neighbor = currentCords + dir.ToCoordinate();

                    if (IsCoordinateValid(neighbor)) {
                        var node = _map[neighbor.X, neighbor.Y];

                        if (IsNodeQueueable(node)) {
                            var pathNode = new PathNode(node, _current, CalculateHValue(_current.Node.Coordinates), _current.g + 1f);

                            _open.Enqueue(pathNode, pathNode.f);
                        }
                    }
                }

            PathNode? current;

            if (!_open.TryDequeue(out current, out _)) {
                Status = PathfindingStatus.Invalid;
                return Status;
            }

            _closed.Add(_current);

            if (current.Node.Coordinates.X == 4 && +current.Node.Coordinates.Y == 4) {
                var temp = current.Node == _target;
            }

            _current = current;

            if (_current.Node == _target) {
                Status = PathfindingStatus.Finished;
            }

            return Status;
        }

        public IEnumerable<Node> BuildPath ( ) {
            var path = new List<Node>();

            if (Status == PathfindingStatus.Finished) {
                var head = _current;

                do {
                    path.Add(head.Node);
                    head = head.Parent;
                } while (head != null);
            }

            return path;
        }

        #region Private functions
        private bool IsCoordinateValid (Coordinate cord) => cord.X >= 0 && cord.X < _map.GetLength(0) && cord.Y >= 0 && cord.Y < _map.GetLength(1);

        private bool IsNodeQueueable (Node node) => !IsNodeInOpen(node) && !IsNodeInClosed(node) && node.Open;

        private bool IsNodeInOpen (Node node) => _open.UnorderedItems.Any(i => i.Element.Node == node);

        private bool IsNodeInClosed (Node node) => _closed.Any(n => n.Node == node);

        private float CalculateHValue (Coordinate cord) => MathF.Sqrt(MathF.Pow(cord.X - _target.Coordinates.X, 2) * MathF.Pow(cord.Y - _target.Coordinates.Y, 2));
        #endregion
    }
}