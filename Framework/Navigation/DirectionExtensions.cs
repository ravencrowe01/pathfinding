using Raven.Util;

namespace Raven.Pathfinding.Framework.Navigation  {
    public static class DirectionExtensions {
        public static Coordinate ToCoordinate (this Direction dir) => dir switch {
            Direction.North => new Coordinate(0, 1),
            Direction.South => new Coordinate(0, -1),
            Direction.East => new Coordinate(1, 0),
            Direction.West => new Coordinate(-1, 0),
            Direction.NorthEast => new Coordinate(1, 1),
            Direction.NorthWest => new Coordinate(-1, 1),
            Direction.SouthEast => new Coordinate(1, 1),
            Direction.SouthWest => new Coordinate(-1, -1),
            _ => new Coordinate(0, 0)
        };
    }
}