
using Raven.Pathfinding.Framework.Nodes;

namespace Raven.Pathfinding.Framework.Config {
    public interface IPathfinderConfig {
        public Node Start { get; set; }
        public Node Target { get; set; }
        public Node[ , ] Map { get; set; }
    }
}