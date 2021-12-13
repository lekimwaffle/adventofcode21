// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var horizontalPositions = lines[0].Split(',').Select(int.Parse);
var testPositions = Enumerable.Range(0, horizontalPositions.Max() + 1);
int fuelConsumption = int.MaxValue;
foreach(var testPosition in testPositions)
{
    var totalFuelNeeded = horizontalPositions.Sum(x => Math.Abs(x - testPosition));
    if (fuelConsumption > totalFuelNeeded)
        fuelConsumption = totalFuelNeeded;
}

Debug.WriteLine($"The answer is {fuelConsumption}");