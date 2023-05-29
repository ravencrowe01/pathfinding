namespace Raven.Pathfinding.Tests;

using Raven.Pathfinding.AStar;
using Raven.Pathfinding.Framework;
using Raven.Pathfinding.Framework.Config;
using Raven.Pathfinding.Framework.Nodes;
using Raven.Util;

public class Tests {
    private IPathfinderConfig _configA;

    private IEnumerable<Node> _configAPath;

    [SetUp]
    public void Setup ( ) {
        _configA = new PathfinderConfig {
            Start = new Node(true, new Coordinate(0, 0)),
            Target = new Node(true, new Coordinate(4, 4)),
            Map = BuildNodeMapA()
        };

        _configAPath = new List<Node> {
            new Node(true, new Coordinate(0, 0)),
            new Node(true, new Coordinate(1, 0)),
            new Node(true, new Coordinate(1, 1)),
            new Node(true, new Coordinate(1, 2)),
            new Node(true, new Coordinate(1, 3)),
            new Node(true, new Coordinate(2, 3)),
            new Node(true, new Coordinate(2, 2))
        };
    }

    [Test]
    public void AStar_MapA_ValidPath ( ) {
        var pathfinder = new AStarPathfinder(_configA);

        do {
            pathfinder.Step();
        } while (pathfinder.Status == PathfindingStatus.Searching);

        var maze = "Map A\n---------------\n";

        for (int y = 6; y >=0; y--) {
            for (int x = 0; x < 7; x++) {
                maze += _configA.Map[x, y].Open ? "|O" : "|X";
            }
            maze += "|\n---------------\n";
        }

        System.Console.WriteLine(maze);

        Assert.That(pathfinder.Status, Is.EqualTo(PathfindingStatus.Finished));
    }

    private Node[,] BuildNodeMapA () {
        var map = new Node[7, 7];

        AddNode(map, 0, 0, true);
        AddNode(map, 0, 1, true);
        AddNode(map, 0, 2, true);
        AddNode(map, 0, 3, false);
        AddNode(map, 0, 4, true);
        AddNode(map, 0, 5, false);
        AddNode(map, 0, 6, true);

        AddNode(map, 1, 0, true);
        AddNode(map, 1, 1, false);
        AddNode(map, 1, 2, true);
        AddNode(map, 1, 3, false);
        AddNode(map, 1, 4, true);
        AddNode(map, 1, 5, false);
        AddNode(map, 1, 6, true);

        AddNode(map, 2, 0, true);
        AddNode(map, 2, 1, false);
        AddNode(map, 2, 2, true);
        AddNode(map, 2, 3, true);
        AddNode(map, 2, 4, true);
        AddNode(map, 2, 5, true);
        AddNode(map, 2, 6, true);

        AddNode(map, 3, 0, false);
        AddNode(map, 3, 1, false);
        AddNode(map, 3, 2, true);
        AddNode(map, 3, 3, false);
        AddNode(map, 3, 4, false);
        AddNode(map, 3, 5, false);
        AddNode(map, 3, 6, false);
        
        AddNode(map, 4, 0, true);
        AddNode(map, 4, 1, true);
        AddNode(map, 4, 2, true);
        AddNode(map, 4, 3, false);
        AddNode(map, 4, 4, true);
        AddNode(map, 4, 5, true);
        AddNode(map, 4, 6, true);
        
        AddNode(map, 5, 0, false);
        AddNode(map, 5, 1, false);
        AddNode(map, 5, 2, true);
        AddNode(map, 5, 3, false);
        AddNode(map, 5, 4, true);
        AddNode(map, 5, 5, false);
        AddNode(map, 5, 6, true);

        AddNode(map, 6, 0, true);
        AddNode(map, 6, 1, true);
        AddNode(map, 6, 2, true);
        AddNode(map, 6, 3, true);
        AddNode(map, 6, 4, true);
        AddNode(map, 6, 5, false);
        AddNode(map, 6, 6, true);

        return map;
    }

    private void AddNode (Node[ , ] map, int x, int y, bool state) => map[x, y] = new Node(state, new Coordinate(x, y));

    private class PathfinderConfig : IPathfinderConfig {
        public Node Start { get; set; }
        public Node Target { get; set; }
        public Node[ , ] Map { get; set; }
    }
}