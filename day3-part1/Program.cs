const string Gamma = "";
const string Epsilon = "";
var result = File.ReadAllLines("input.txt")
    .Select(x => x.Chunk(1).Select(a => a.First() == '1'))
    .SelectMany(x => x.Select((y, z) => (z, y)))
    .GroupBy(x => x.z, x => x.y)
    .Select(x => x.OrderBy(y => y).ElementAt(x.Count() / 2))
    .Aggregate((Gamma, Epsilon), (x, y) => (x.Gamma += Convert.ToByte(y), x.Epsilon += Convert.ToByte(!y)), x => Convert.ToInt32(x.Gamma, 2) * Convert.ToInt32(x.Epsilon, 2));

Console.WriteLine($"The answer is {result}");