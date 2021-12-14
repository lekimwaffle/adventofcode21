var lines = await File.ReadAllLinesAsync("input.txt");
var startingTemplate = lines[0];
var instructions = lines.Skip(2).Select(x =>
{
    var split = x.Split(" -> ");
    return new Instruction(split[0], split[1][0]);
}).ToDictionary(x => x.Match, x => x.Insert);

var pairValues = new Dictionary<string, long>();
foreach(var pair in GetPairs(startingTemplate))
{
    if (!pairValues.ContainsKey(pair))
        pairValues.Add(pair, 0);

    pairValues[pair]++;
}

for(int step = 0; step < 40; step++)
{
    var newPairValues = new Dictionary<string, long>();
    foreach (var pairValue in pairValues)
    {
        var insertChar = instructions[pairValue.Key];
        var leftPair = $"{pairValue.Key[0]}{insertChar}";
        var rightPair = $"{insertChar}{pairValue.Key[1]}";
        foreach(var newPair in new[] { leftPair, rightPair })
        {
            if(!newPairValues.ContainsKey(newPair))
                newPairValues.Add(newPair, 0);

            newPairValues[newPair] += pairValue.Value;
        }
    }

    pairValues = newPairValues;
}

var uniqueChars = pairValues.Keys.SelectMany(x => x).Distinct().ToDictionary(x => x, y => pairValues.Keys.Where(x => x[0] == y).Select(z => pairValues[z]).Sum());

uniqueChars[startingTemplate.Last()]++;
var result = uniqueChars.Max(x => x.Value) - uniqueChars.Min(x => x.Value);
Console.WriteLine(result);

IEnumerable<string> GetPairs(string template)
{
    for (int i = 0; i < template.Length - 1; i++)
        yield return template.Substring(i, 2);
}

public record Instruction(string Match, char Insert);