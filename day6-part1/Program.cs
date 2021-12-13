// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var school = new List<LanternFish>(lines[0].Split(',').Select(x => new LanternFish(int.Parse(x))));
int days = 80;

for(int i = 0; i < days; i++)
{
    var babies = new List<LanternFish>();
    foreach(var fish in school)
    {
        if(fish.Timer == 0)
        {
            babies.Add(new LanternFish(8));
            fish.Timer = 6;
        }
        else
        {
            fish.Timer--;
        }
    }

    school.AddRange(babies);
}

Debug.WriteLine($"The answer is {school.Count}");

public class LanternFish
{
    public LanternFish(int timer)
    {
        Timer = timer;
    }

    public int Timer { get; set; }
}