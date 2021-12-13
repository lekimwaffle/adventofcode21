const int Count = 0;
const int LastRead = int.MaxValue;
var count = File.ReadAllLines("input.txt")
    .Select(int.Parse)
    .Aggregate((Count, LastRead), (x, y) => (x.Count += y > x.LastRead ? 1 : 0, y));
Console.WriteLine($"The answer is {count.Count}");