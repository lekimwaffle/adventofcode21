// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

var lines = await File.ReadAllLinesAsync("input.txt");
Dictionary<string, Cave> caves = new Dictionary<string, Cave>();
foreach (var line in lines)
{
    var nodes = line.Split('-');
    var secondCave = GetOrCreate(nodes[1]);
    var firstCave = GetOrCreate(nodes[0]);
    firstCave.ConnectsTo.Add(secondCave);
    secondCave.ConnectsTo.Add(firstCave);
}

Debug.WriteLine($"The answer is {FindPaths(caves["start"], new List<string>(), false)}");
int FindPaths(Cave start, ICollection<string> visitedSmallCaves, bool hasDoubleVisitedCave)
{
    if (start.Id == "end")
        return 1;

    if (!start.Big)
    {
        if (visitedSmallCaves.Contains(start.Id))
        {
            if (hasDoubleVisitedCave || start.Id == "start")
                return 0;

            hasDoubleVisitedCave = true;
        }

        visitedSmallCaves.Add(start.Id);
    }

    int paths = 0;
    foreach (var connectedCave in start.ConnectsTo)
    {
        paths += FindPaths(connectedCave, new List<string>(visitedSmallCaves), hasDoubleVisitedCave);
    }

    return paths;
}

Cave GetOrCreate(string id)
{
    if (!caves.ContainsKey(id))
        caves.Add(id, new Cave(id, char.IsUpper(id[0]), new List<Cave>()));

    return caves[id];
}

public record Cave(string Id, bool Big, ICollection<Cave> ConnectsTo);