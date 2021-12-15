var lines = await File.ReadAllLinesAsync("input.txt");
var bottomBoundary = lines.Length * 5;
var rightBoundary = lines[0].Length * 5;
var map = new Node[rightBoundary, bottomBoundary];
for (int y = 0; y < lines.Length; y++)
    for (int x = 0; x < lines[0].Length; x++)
        map[x, y] = new Node(x, y, int.Parse(lines[y][x].ToString()));

for(int zy = 0; zy < 5; zy++)
    for(int zx = 0; zx < 5; zx++)
        for (int y = 0; y < lines.Length; y++)
            for (int x = 0; x < lines[0].Length; x++)
            {
                var risk = int.Parse(lines[y][x].ToString()) + zx + zy;
                map[x + (zx * lines[0].Length), y + (zy * lines.Length)] = new Node(x + (zx * lines[0].Length), y + (zy * lines.Length), risk > 9 ? risk % 10 + 1 : risk);
            }


var start = new PathNode(map[0, 0], 1, GetDistance(map[0, 0], map[rightBoundary - 1, bottomBoundary - 1]));
var end = new PathNode(map[rightBoundary - 1, bottomBoundary - 1], 1, 0);

var activeNodes = new Dictionary<Node, PathNode>();
activeNodes.Add(start.Node, start);
var visitedNodes = new Dictionary<Node, PathNode>();

while (activeNodes.Any())
{
    var checkNode = activeNodes.OrderBy(x => x.Value.CostDistance).First();
    if (checkNode.Value.Node == end.Node)
    {
        var pathTaken = new List<PathNode>();
        var tile = checkNode.Value;
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

    visitedNodes.Add(checkNode.Key, checkNode.Value);
    activeNodes.Remove(checkNode.Key);

    var walkableNodes = GetWalkableNodes(checkNode.Value, end);
    foreach (var walkableNode in walkableNodes)
    {
        if (visitedNodes.ContainsKey(walkableNode.Node))
            continue;

        if(!activeNodes.ContainsKey(walkableNode.Node))
        {
            activeNodes.Add(walkableNode.Node, walkableNode);
            continue;
        }

        if (activeNodes[walkableNode.Node].CostDistance <= walkableNode.CostDistance)
            continue;

        activeNodes.Remove(walkableNode.Node);
        activeNodes.Add(walkableNode.Node, walkableNode);
    }
}

List<PathNode> GetWalkableNodes(PathNode current, PathNode target)
{
    var nodes = new List<PathNode>();
    if (current.Node.X > 0)
    {
        var node = map[current.Node.X - 1, current.Node.Y];
        nodes.Add(new PathNode(node, current.Cost + node.Risk, GetDistance(node, target.Node)) { Parent = current });
    }
        
    if (current.Node.X < rightBoundary - 1)
    {
        var node = map[current.Node.X + 1, current.Node.Y];
        nodes.Add(new PathNode(node, current.Cost + node.Risk, GetDistance(node, target.Node)) { Parent = current });
    }
        
    if (current.Node.Y > 0)
    {
        var node = map[current.Node.X, current.Node.Y - 1];
        nodes.Add(new PathNode(node, current.Cost + node.Risk, GetDistance(node, target.Node)) { Parent = current });
    }
        
    if (current.Node.Y < bottomBoundary - 1)
    {
        var node = map[current.Node.X, current.Node.Y + 1];
        nodes.Add(new PathNode(node, current.Cost + node.Risk, GetDistance(node, target.Node)) { Parent = current });
    }
        
    return nodes;
}

int GetDistance(Node start, Node target) => Math.Abs(target.X - start.X) + Math.Abs(target.Y - start.Y);

record Node(int X, int Y, int Risk);
record PathNode(Node Node, int Cost, int Distance)
{
    public PathNode? Parent { get; init; }

    public int CostDistance = Cost + Distance;
}