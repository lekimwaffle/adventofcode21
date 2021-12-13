// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var horizontalPositions = lines[0].Split(',').Select(int.Parse);
var testPositions = Enumerable.Range(0, horizontalPositions.Max() + 1);
int fuelConsumption = int.MaxValue;
foreach (var testPosition in testPositions)
{
    var totalFuelNeeded = horizontalPositions.Sum(x => Enumerable.Range(1, Math.Abs(x - testPosition)).Sum());
    if (fuelConsumption > totalFuelNeeded)
        fuelConsumption = totalFuelNeeded;
}

Debug.WriteLine($"The answer is {fuelConsumption}");