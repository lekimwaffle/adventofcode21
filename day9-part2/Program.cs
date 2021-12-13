// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var points = new int[lines.Length, lines[0].Length];
for (int i = 0; i < lines.Length; i++)
{
    for (int p = 0; p < lines[i].Length; p++)
    {
        points[i, p] = int.Parse(lines[i][p].ToString());
    }
}

var lowPoints = new List<(int X, int Y)>();
for (int i = 0; i < lines.Length; i++)
{
    for (int p = 0; p < lines[i].Length; p++)
    {
        int basinSize;
        if (i > 0 && points[i - 1, p] <= points[i, p] ||
            i < lines.Length - 1 && points[i + 1, p] <= points[i, p] ||
            p > 0 && points[i, p - 1] <= points[i, p] ||
            p < lines[i].Length - 1 && points[i, p + 1] <= points[i, p])
            continue;

        lowPoints.Add((p, i));
    }
}

var basins = new List<int>();
foreach(var lowPoint in lowPoints)
{
    var positions = new List<(int X, int Y)>();
    CheckNeighbours(points, positions, lowPoint, lines[0].Length - 1, lines.Length - 1);
    basins.Add(positions.Count());
}

Debug.WriteLine($"The answer is {basins.OrderBy(x => x).Reverse().Take(3).Aggregate((x ,y) => x * y)}");

void CheckNeighbours(int[,] points, ICollection<(int X, int Y)> cumulativePositions, (int X, int Y) startPosition, int rightBounds, int lowerBounds)
{
    if(cumulativePositions.Contains(startPosition))
        return;

    cumulativePositions.Add(startPosition);
    int currentHeight = points[startPosition.Y, startPosition.X];
    if (startPosition.X > 0)
    {
        var height = points[startPosition.Y, startPosition.X - 1];
        if(height > currentHeight && height != 9)
            CheckNeighbours(points, cumulativePositions, (startPosition.X - 1, startPosition.Y), rightBounds, lowerBounds);
    }

    if (startPosition.X < rightBounds)
    {
        var height = points[startPosition.Y, startPosition.X + 1];
        if (height > currentHeight && height != 9)
            CheckNeighbours(points, cumulativePositions, (startPosition.X + 1, startPosition.Y), rightBounds, lowerBounds);
    }

    if (startPosition.Y > 0)
    {
        var height = points[startPosition.Y - 1, startPosition.X];
        if (height > currentHeight && height != 9)
            CheckNeighbours(points, cumulativePositions, (startPosition.X, startPosition.Y - 1), rightBounds, lowerBounds);
    }

    if (startPosition.Y < lowerBounds)
    {
        var height = points[startPosition.Y + 1, startPosition.X];
        if (height > currentHeight && height != 9)
            CheckNeighbours(points, cumulativePositions, (startPosition.X, startPosition.Y + 1), rightBounds, lowerBounds);
    }
}