// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var lineCoordinates = lines.Select(x => x
                            .Split(new[] { ',', ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(y => int.Parse(y)).ToArray())
                            .Select(x => new LineCoordinate(x[0], x[1], x[2], x[3]));

var coordinates = new Dictionary<(int X, int Y), int>();
foreach (var lineCoordinate in lineCoordinates)
{
    IEnumerable<(int X, int Y)> coordinateRange;
    if (lineCoordinate.X1 == lineCoordinate.X2)
    {
        var startY = Math.Min(lineCoordinate.Y1, lineCoordinate.Y2);
        var endY = Math.Max(lineCoordinate.Y1, lineCoordinate.Y2) - startY;
        coordinateRange = Enumerable.Range(startY, endY + 1).Select(y => (lineCoordinate.X1, y));
    }
    else if (lineCoordinate.Y1 == lineCoordinate.Y2)
    {
        var startX = Math.Min(lineCoordinate.X1, lineCoordinate.X2);
        var endX = Math.Max(lineCoordinate.X1, lineCoordinate.X2) - startX;
        coordinateRange = Enumerable.Range(startX, endX + 1).Select(x => (x, lineCoordinate.Y1));
    }
    else
    {
        var startY = Math.Min(lineCoordinate.Y1, lineCoordinate.Y2);
        var endY = Math.Max(lineCoordinate.Y1, lineCoordinate.Y2) - startY;
        var yRange = Enumerable.Range(startY, endY + 1);
        if (lineCoordinate.Y2 < lineCoordinate.Y1)
            yRange = yRange.Reverse();

        var startX = Math.Min(lineCoordinate.X1, lineCoordinate.X2);
        var endX = Math.Max(lineCoordinate.X1, lineCoordinate.X2) - startX;
        var xRange = Enumerable.Range(startX, endX + 1);
        if (lineCoordinate.X2 < lineCoordinate.X1)
            xRange = xRange.Reverse();
        coordinateRange = xRange.Zip(yRange).Select(x => (x.First, x.Second));
    }

    foreach (var coordinate in coordinateRange)
    {
        if (coordinates.ContainsKey((coordinate.X, coordinate.Y)))
            coordinates[(coordinate.X, coordinate.Y)]++;
        else
            coordinates.Add((coordinate.X, coordinate.Y), 1);
    }
}

Debug.WriteLine($"The answer is {coordinates.Count(x => x.Value > 1)}");

public record LineCoordinate(int X1, int Y1, int X2, int Y2);