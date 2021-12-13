// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var configurations = new Dictionary<ChunkType, (char Open, char Close)>
{
    [ChunkType.Curly] = ('(', ')'),
    [ChunkType.Square] = ('[', ']'),
    [ChunkType.Accolade] = ('{', '}'),
    [ChunkType.Comparer] = ('<', '>')
};

var illegalCounter = new Dictionary<ChunkType, int>
{
    [ChunkType.Curly] = 0,
    [ChunkType.Square] = 0,
    [ChunkType.Accolade] = 0,
    [ChunkType.Comparer] = 0
};

var lines = await File.ReadAllLinesAsync("input.txt");
foreach(var line in lines)
{
    var chunks = new LinkedList<ChunkType>();
    foreach(var character in line)
    {
        if (configurations.Any(x => x.Value.Open == character))
            chunks.AddLast(configurations.First(x => x.Value.Open == character).Key);
        else if (chunks.Last.Value == configurations.FirstOrDefault(x => x.Value.Close == character).Key)
            chunks.RemoveLast();
        else
        {
            illegalCounter[configurations.FirstOrDefault(x => x.Value.Close == character).Key]++;
            break;
        }
    }
}

Debug.WriteLine($"The answer is {illegalCounter[ChunkType.Curly] * 3 + illegalCounter[ChunkType.Square] * 57 + illegalCounter[ChunkType.Accolade] * 1197 + illegalCounter[ChunkType.Comparer] * 25137}");

enum ChunkType
{
    Curly,
    Square,
    Accolade,
    Comparer
}