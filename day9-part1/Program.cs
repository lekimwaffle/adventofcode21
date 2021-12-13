// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var points = new int[lines.Length, lines[0].Length];
for(int i = 0; i < lines.Length; i++)
{
    for (int p = 0; p < lines[i].Length; p++)
    {
        points[i,p] = int.Parse(lines[i][p].ToString());
    }
}

int lowPointRisk = 0;
for (int i = 0; i < lines.Length; i++)
{
    for (int p = 0; p < lines[i].Length; p++)
    {
        if (i > 0 && points[i - 1, p] <= points[i, p] || 
            i < lines.Length - 1 && points[i + 1, p] <= points[i, p] || 
            p > 0 && points[i, p - 1] <= points[i, p] || 
            p < lines[i].Length - 1 && points[i, p + 1] <= points[i, p])
            continue;

        lowPointRisk += points[i, p] + 1;
    }
}

Debug.WriteLine($"The answer is {lowPointRisk}");