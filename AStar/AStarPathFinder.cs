using Raven.Pathfinding.Framework;
using Raven.Pathfinding.Framework.Config;
using Raven.Pathfinding.Framework.Navigation;
using Raven.Pathfinding.Util.Nodes;
using Raven.Util;

namespace Raven.Pathfinding.AStar {
    public class AStarPathfinder : IPathfinder {
        private PathNode _start;
        private Node _target;
        private Node[ , ] _map;

        private PathNode _current;

        private PriorityQueue<PathNode, float> _open;
        private IList<PathNode> _closed;

        private PathfindingStatus _status = PathfindingStatus.Waiting;

        private bool _valid = false;
        private bool _finished = false;

        public AStarPathfinder (IPathfinderConfig config) {
            _start = new PathNode(config.Start, null, 0f, 0f);
            _target = config.Target;
            _map = config.Map;

            _current = _start;

            _open = new PriorityQueue<PathNode, float>();
            _closed = new List<PathNode>();
        }

        public PathfindingStatus Step ( ) {
            if (!_valid) {
                return PathfindingStatus.Invalid;
            }

            if (_finished) {
                return PathfindingStatus.Finished;
            }

            var currentCords = _current.Node.Coordinates;

            foreach (Direction dir in Enum.GetValues(typeof(Direction))) {
                var neighbor = currentCords + dir.ToCoordinate();

                var cord = new Coordinate(0, 0);

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
                _valid = false;
                return PathfindingStatus.Invalid;
            }

            _closed.Add(_current);

            _current = current;

            if (_current.Node == _target) {
                _finished = true;
                return PathfindingStatus.Finished;
            }

            return PathfindingStatus.Searching;
        }

        private bool IsCoordinateValid (Coordinate cord) => cord.X >= 0 && cord.X < _map.GetLength(0) && cord.Y >= 0 && cord.Y < _map.GetLength(1);

        private bool IsNodeQueueable (Node node) => !IsNodeInOpen(node) && !IsNodeInClosed(node) && node.Open;

        private bool IsNodeInOpen (Node node) => _open.UnorderedItems.Any(i => i.Element.Node == node);

        private bool IsNodeInClosed (Node node) => _closed.Any(n => n.Node == node);

        private float CalculateHValue (Coordinate cord)
            => MathF.Sqrt(MathF.Pow(cord.X - _target.Coordinates.X, 2) * MathF.Pow(cord.Y - _target.Coordinates.Y, 2));

        public IEnumerable<Node> GeneratePath ( ) {
            if (_status == PathfindingStatus.Finished) {
                var path = new List<Node>();
                var head = _current;

                do {
                    path.Add(head.Node);
                    head = head.Parent;
                } while (head != null);

                return path;
            }

            return new List<Node>();
        }

        public IEnumerable<Node> BuildPath ( ) {
            var path = new List<Node>();

            if (_status == PathfindingStatus.Finished) {
                var head = _current;

                do {
                    path.Add(head.Node);
                    head = head.Parent;
                } while (head != null);
            }

            return path;
        }
    }
}