const int Count = 0;
const int LastRead = int.MaxValue;

var count = File.ReadAllLines("input.txt")
    .Select(int.Parse)
    .SelectSource()
    .Select((x, y) => x.Skip(y).Take(3).Sum())
    .Aggregate((Count, LastRead), (x, y) => (x.Count += y > x.LastRead ? 1 : 0, y));
Console.WriteLine($"The answer is {count.Count}");

static class Extensions
{
    public static IEnumerable<IEnumerable<T>> SelectSource<T>(this IEnumerable<T> list) => list.Select(x => list);
}