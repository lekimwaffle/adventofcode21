// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var connections = new Dictionary<char, Connection>
{
    ['a'] = new Connection('a'),
    ['b'] = new Connection('b'),
    ['c'] = new Connection('c'),
    ['d'] = new Connection('d'),
    ['f'] = new Connection('f'),
    ['e'] = new Connection('e'),
    ['g'] = new Connection('g'),
};

var segments = new Dictionary<int, IEnumerable<Connection>>()
{
    [0] = new[] { connections['a'], connections['b'], connections['c'], connections['e'], connections['f'], connections['g'] },
    [1] = new[] { connections['c'], connections['f'] },
    [2] = new[] { connections['a'], connections['c'], connections['d'], connections['e'], connections['g'] },
    [3] = new[] { connections['a'], connections['c'], connections['d'], connections['f'], connections['g'] },
    [4] = new[] { connections['b'], connections['c'], connections['d'], connections['f'] },
    [5] = new[] { connections['a'], connections['b'], connections['d'], connections['f'], connections['g'] },
    [6] = new[] { connections['a'], connections['b'], connections['d'], connections['e'], connections['f'], connections['g'] },
    [7] = new[] { connections['a'], connections['c'], connections['f'] },
    [8] = new[] { connections['a'], connections['b'], connections['c'], connections['d'], connections['e'], connections['f'], connections['g'] },
    [9] = new[] { connections['a'], connections['b'], connections['c'], connections['d'], connections['f'], connections['g'] }
};

var lines = await File.ReadAllLinesAsync("input.txt");
var entries = lines.Select(line => {
    var splitted = line.Split(new char[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    return new Entry { SignalPatterns = splitted.Take(10), OutputValues = splitted.Skip(10) };
});

Debug.WriteLine($"The answer is { entries.Sum(x => x.OutputValues.Count(y => y.Length == segments[1].Count() || y.Length == segments[4].Count() || y.Length == segments[7].Count() || y.Length == segments[8].Count())) }");

public class Connection
{
    public Connection(char segment)
    {
        Segment = segment;
    }

    public char Segment { get; }

    public char Wire { get; }
}

public class Entry
{
    public IEnumerable<string> SignalPatterns { get; set; }

    public IEnumerable<string> OutputValues { get; set; }
}