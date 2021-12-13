// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var configurations = new Dictionary<ChunkType, (char Open, char Close)>
{
    [ChunkType.Curly] = ('(', ')'),
    [ChunkType.Square] = ('[', ']'),
    [ChunkType.Accolade] = ('{', '}'),
    [ChunkType.Comparer] = ('<', '>')
};

var totalScores = new List<long>();
var lines = await File.ReadAllLinesAsync("input.txt");
foreach (var line in lines)
{
    var chunks = new LinkedList<ChunkType>();
    foreach (var character in line)
    {
        if (configurations.Any(x => x.Value.Open == character))
            chunks.AddLast(configurations.First(x => x.Value.Open == character).Key);
        else if (chunks.Last.Value == configurations.FirstOrDefault(x => x.Value.Close == character).Key)
            chunks.RemoveLast();
        else
        {
            chunks.Clear();
            break;
        }
            
    }

    if (chunks.Count == 0)
        continue;

    long totalScore = 0;
    foreach (var chunk in chunks.Reverse())
    {
        totalScore *= 5;
        switch (chunk)
        {
            case ChunkType.Curly: totalScore += 1; break;
            case ChunkType.Square: totalScore += 2; break;
            case ChunkType.Accolade: totalScore += 3; break;
            case ChunkType.Comparer: totalScore += 4; break;
        }
    }

    totalScores.Add(totalScore);
}

var sorted = totalScores.OrderBy(x => x);
var middle = (int)Math.Round(totalScores.Count() / 2d);
Debug.WriteLine($"The answer is {sorted.ElementAt(middle)}");

enum ChunkType
{
    Curly,
    Square,
    Accolade,
    Comparer
}