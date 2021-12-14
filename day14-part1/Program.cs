var lines = await File.ReadAllLinesAsync("input.txt");
var template = lines[0];
var instructions = lines.Skip(2).Select(x =>
{
    var split = x.Split(" -> ");
    return new Instruction(split[0], split[1]);
});

int steps = 10;
for(int step = 0; step < steps; step++)
{
    var pairs = GetPairs(template);
    foreach(var pair in pairs.Reverse())
    {
        var instruction = instructions.First(x => x.Match == pair.Pair);
        template = template.Insert(pair.Index + 1, instruction.Insert);
    }
}

var occurances = template.GroupBy(x => x).Select(x => (x.Key, x.Count())).OrderBy(x => x.Item2);
var result = occurances.Last().Item2 - occurances.First().Item2;
Console.WriteLine(result);

IEnumerable<(int Index, string Pair)> GetPairs(string template)
{
    for(int i = 0; i < template.Length - 1; i++)
        yield return (i, template.Substring(i, 2));
}

public record Instruction(string Match, string Insert);