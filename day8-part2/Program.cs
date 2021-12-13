// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var entries = lines.Select(line => {
    var splitted = line.Split(new char[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    return new Entry(splitted.Take(10), splitted.Skip(10));
});

int totalOutput = 0;
foreach(var entry in entries)
{
    var connections = "abcdefg".ToDictionary(x => x, x => new Connection(x));
    var segments = new Dictionary<int, IEnumerable<Connection>>()
    {
        [0] = GetConnections("abcefg"),
        [1] = GetConnections("cf"),
        [2] = GetConnections("acdeg"),
        [3] = GetConnections("acdfg"),
        [4] = GetConnections("bcdf"),
        [5] = GetConnections("abdfg"),
        [6] = GetConnections("abdefg"),
        [7] = GetConnections("acf"),
        [8] = GetConnections("abcdefg"),
        [9] = GetConnections("abcdfg")
    };

    IEnumerable<Connection> GetConnections(string segments) => segments.Select(x => connections[x]);

    var one = entry.SignalPatterns.First(x => x.Length == segments[1].Count());
    var seven = entry.SignalPatterns.First(x => x.Length == segments[7].Count());
    connections['a'].Wire = seven.Except(one).First();
    var unique = connections.Select(x => x.Key).ToDictionary(x => x, x => entry.SignalPatterns.Count(s => s.Contains(x)));
    connections['b'].Wire = unique.First(x => x.Value == 6).Key;
    connections['c'].Wire = unique.First(x => x.Value == 8 && x.Key != connections['a'].Wire).Key;
    var four = entry.SignalPatterns.First(x => x.Length == segments[4].Count());
    connections['e'].Wire = unique.First(x => x.Value == 4).Key;
    connections['f'].Wire = unique.First(x => x.Value == 9).Key;
    connections['d'].Wire = four.First(x => !connections.Values.Select(y => y.Wire).Contains(x));
    connections['g'].Wire = "abcdefg".First(x => !connections.Values.Select(y => y.Wire).Contains(x));

    string output = string.Empty;
    foreach (var outputValue in entry.OutputValues)
    {
        foreach (var segment in segments)
        {
            if (segment.Value.Count() != outputValue.Length)
                continue;

            if (!outputValue.All(x => segment.Value.Select(x => x.Wire).Contains(x)))
                continue;

            output += segment.Key;
            break;
        }
    }

    totalOutput += int.Parse(output);
}

Debug.WriteLine($"The answer is { totalOutput }");

public class Connection
{
    public Connection(char segment)
    {
        Segment = segment;
    }

    public char Segment { get; }

    public char Wire { get; set; }
}

public class Entry
{
    public Entry(IEnumerable<string> signalPatterns, IEnumerable<string> outputValues)
    {
        SignalPatterns = signalPatterns;
        OutputValues = outputValues;
    }

    public IEnumerable<string> SignalPatterns { get; set; }

    public IEnumerable<string> OutputValues { get; set; }
}