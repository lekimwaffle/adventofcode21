var lines = await File.ReadAllLinesAsync("input.txt");
var map = new Node[lines[0].Length, lines.Length];
for(int y = 0; y < lines.Length; y++)
    for(int x = 0; x < lines[0].Length; x++)
        map[x, y] = new Node(x, y, int.Parse(lines[y][x].ToString()));

var start = new PathNode(map[0, 0], 1, GetDistance(map[0, 0], map[lines[0].Length - 1, lines.Length - 1]));
var end = new PathNode(map[lines[0].Length - 1, lines.Length - 1], 1, 0);

var activeNodes = new List<PathNode>();
activeNodes.Add(start);
var visitedNodes = new List<PathNode>();

while (activeNodes.Any())
{
    var checkNode = activeNodes.OrderByDescending(x => x.Cost + x.Distance).Last();
    if (checkNode.Node == end.Node)
    {
        var pathTaken = new List<PathNode>();
        var tile = checkNode;
        while (tile.Parent != null)
        {
            pathTaken.Add(tile);
            tile = tile.Parent;
        }

        pathTaken.Reverse();
        Console.WriteLine($"Path taken is {pathTaken.Aggregate(string.Empty, (x, y) => x + y.Node.Risk)}");
        Console.WriteLine($"Total risk count is {pathTaken.Sum(x => x.Node.Risk)}");
        return;
    }
       
    visitedNodes.Add(checkNode);
    activeNodes.Remove(checkNode);

    var walkableNodes = GetWalkableNodes(checkNode, end);
    foreach(var walkableNode in walkableNodes)
    {
        if (visitedNodes.Any(x => x.Node == walkableNode.Node))
            continue;

        if(!activeNodes.Any(x => x.Node == walkableNode.Node))
        {
            activeNodes.Add(walkableNode);
            continue;
        }

        var existingNode = activeNodes.First(x => x.Node == walkableNode.Node);
        if (!((existingNode.Cost + existingNode.Distance) > (walkableNode.Cost + walkableNode.Distance)))
            continue;

        activeNodes.Remove(existingNode);
        activeNodes.Add(walkableNode);
    }
}

IEnumerable<PathNode> GetWalkableNodes(PathNode current, PathNode target)
{
    var nodes = new List<PathNode>();
    if(current.Node.X > 0)
        nodes.Add(new PathNode(map[current.Node.X - 1, current.Node.Y], current.Cost + map[current.Node.X - 1, current.Node.Y].Risk, GetDistance(map[current.Node.X - 1, current.Node.Y], target.Node)) { Parent = current });

    if(current.Node.X < lines[0].Length - 1)
        nodes.Add(new PathNode(map[current.Node.X + 1, current.Node.Y], current.Cost + map[current.Node.X + 1, current.Node.Y].Risk, GetDistance(map[current.Node.X + 1, current.Node.Y], target.Node)) { Parent = current });

    if(current.Node.Y > 0)
        nodes.Add(new PathNode(map[current.Node.X, current.Node.Y - 1], current.Cost + map[current.Node.X, current.Node.Y - 1].Risk, GetDistance(map[current.Node.X, current.Node.Y - 1], target.Node)) { Parent = current });

    if(current.Node.Y < lines.Length - 1)
        nodes.Add(new PathNode(map[current.Node.X, current.Node.Y + 1], current.Cost + map[current.Node.X, current.Node.Y + 1].Risk, GetDistance(map[current.Node.X, current.Node.Y + 1], target.Node)) { Parent = current });

    return nodes;
}

int GetDistance(Node start, Node target) => Math.Abs(target.X - start.X) + Math.Abs(target.Y - start.Y);

record Node(int X, int Y, int Risk);
record PathNode(Node Node, int Cost, int Distance)
{
    public PathNode? Parent { get; init; }
}