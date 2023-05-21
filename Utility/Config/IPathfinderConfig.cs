using Raven.Pathfinding.Util.Nodes;

namespace Raven.Pathfinding.Util.Config {
    public interface IPathfinderConfig {
        public Node Start { get; set; }
        public Node Target { get; set; }
        public Node[ , ] Map { get; set; }
    }
}