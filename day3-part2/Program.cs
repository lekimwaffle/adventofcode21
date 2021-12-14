var result = File.ReadAllLines("input.txt")
    .Select(x => x.Chunk(1).Select(a => a.First() == '1'))
    .SelectSource()
    .Select(x => (Oxygen: Calculate2(x, 0, true), Co2: Calculate2(x, 0, false)))
    .Aggregate(0, (x, y) => x += Convert.ToInt32(y.Item1, 2) * Convert.ToInt32(y.Item2, 2));

Console.WriteLine($"The answer is {result}");

string Calculate2(IEnumerable<IEnumerable<bool>> values, int position, bool compare)
{
    return values.Count() switch
    {
        1 => values.First().Select(x => x ? '1' : '0').Aggregate(string.Empty, (x, y) => x + y),
        var count when (double)count / values.Count(x => x.ElementAt(position) == compare) == 2 => Calculate2(values.Where(x => x.ElementAt(position) == compare), position + 1, compare),
        var oxy when compare => Calculate2(values.GroupBy(y => y.ElementAt(position)).OrderByDescending(x => x.Count()).First(), position + 1, compare),
        var co2 when !compare => Calculate2(values.GroupBy(y => y.ElementAt(position)).OrderBy(x => x.Count()).First(), position + 1, compare)
    };
}

static class Extensions
{
    public static IEnumerable<IEnumerable<T>> SelectSource<T>(this IEnumerable<T> list) => list.Select(x => list).Take(1);
}