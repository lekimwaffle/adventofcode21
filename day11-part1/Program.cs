// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var points = new int[lines[0].Length, lines.Length];
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        points[x, y] = int.Parse(lines[y][x].ToString());
    }
}

int flashCounter = 0;
int steps = 100;
for(int step = 0; step < steps; step++)
{
    var tens = new List<(int X, int Y)>();
    for(int x = 0; x < lines[0].Length; x++)
    {
        for(int y = 0; y < lines.Length; y++)
        {
            points[x, y]++;
            if(points[x, y] > 9)
                tens.Add((x, y));
        }
    }

    var energizedPositions = new List<(int X, int Y)>();
    foreach(var ten in tens)
        ProcessEnergy(points, energizedPositions, ten, lines[0].Length - 1, lines.Length - 1);

    flashCounter += energizedPositions.Count;
    energizedPositions.ForEach(x => points[x.X, x.Y] = 0);
}

Debug.WriteLine($"The answer is {flashCounter}");

void ProcessEnergy(int[,] points, ICollection<(int X, int Y)> cumulativePositions, (int X, int Y) startPosition, int rightBound, int lowerBound)
{
    if (cumulativePositions.Contains(startPosition))
        return;

    cumulativePositions.Add(startPosition);

    (int X, int Y) left = (startPosition.X - 1, startPosition.Y);
    (int X, int Y) right = (startPosition.X + 1, startPosition.Y);
    (int X, int Y) up = (startPosition.X, startPosition.Y - 1);
    (int X, int Y) down = (startPosition.X, startPosition.Y + 1);
    if (startPosition.X > 0 && points[left.X, left.Y] < 10)
        ProcessEnergyPosition(left);

    if (startPosition.X < rightBound && points[right.X, right.Y] < 10)
        ProcessEnergyPosition(right);

    if (startPosition.Y > 0 && points[up.X, up.Y] < 10)
        ProcessEnergyPosition(up);
        
    if (startPosition.Y < lowerBound && points[down.X, down.Y] < 10)
        ProcessEnergyPosition(down);
        
    if (startPosition.X > 0 && startPosition.Y > 0 && points[left.X, up.Y] < 10)
        ProcessEnergyPosition((left.X, up.Y));
        
    if (startPosition.X < rightBound && startPosition.Y > 0 && points[right.X, up.Y] < 10)
        ProcessEnergyPosition((right.X, up.Y));
        
    if (startPosition.X > 0 && startPosition.Y < lowerBound && points[left.X, down.Y] < 10)
        ProcessEnergyPosition((left.X, down.Y ));
        
    if (startPosition.X < rightBound && startPosition.Y < lowerBound && points[right.X , down.Y] < 10)
        ProcessEnergyPosition((right.X , down.Y));

    void ProcessEnergyPosition((int X, int Y) position)
    {
        points[position.X, position.Y]++;
        if (points[position.X, position.Y] > 9)
            ProcessEnergy(points, cumulativePositions, position, rightBound, lowerBound);
    }
}