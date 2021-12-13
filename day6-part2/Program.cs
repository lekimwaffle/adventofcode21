// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
int days = 256;
var ages = Enumerable.Range(0, 9).ToDictionary(x => x, y => 0l);
lines[0].Split(',').Select(x => int.Parse(x)).ToList().ForEach(x => ages[x] += 1);
for(int day = 0; day < days; day++)
{     
    var happyFishes = ages[0];
    for(int i = 1; i < ages.Count; i++)
    {
        ages[i - 1] = ages[i];
    }

    ages[8] = happyFishes;
    ages[6] += happyFishes;
}

Debug.WriteLine($"The answer is {ages.Sum(x => x.Value)}");