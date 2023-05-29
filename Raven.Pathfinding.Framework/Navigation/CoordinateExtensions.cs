using Raven.Util;

namespace Raven.Pathfinding.Framework.Navigation {
    public static class CoordinateExtensions {
        public static Direction ToDirection (this Coordinate vec) => vec switch {
            Coordinate v when vec.Equals(Direction.North.ToCoordinate()) => Direction.North,
            Coordinate v when vec.Equals(Direction.South.ToCoordinate()) => Direction.South,
            Coordinate v when vec.Equals(Direction.East.ToCoordinate()) => Direction.East,
            Coordinate v when vec.Equals(Direction.West.ToCoordinate()) => Direction.West,
            Coordinate v when vec.Equals(Direction.NorthEast.ToCoordinate()) => Direction.NorthEast,
            Coordinate v when vec.Equals(Direction.NorthWest.ToCoordinate()) => Direction.NorthWest,
            Coordinate v when vec.Equals(Direction.SouthEast.ToCoordinate()) => Direction.SouthEast,
            Coordinate v when vec.Equals(Direction.SouthWest.ToCoordinate()) => Direction.SouthWest,
            _ => 0
        };
    }
}