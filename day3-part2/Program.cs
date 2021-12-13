// See https://aka.ms/new-console-template for more information
var lines = await File.ReadAllLinesAsync("input.txt");
var middle = lines.Length / 2;
string oxygenGenerator = Calculate(lines, 0, true); ;
string co2Scrubber = Calculate(lines, 0, false); ;

int oxygenGeneratorRate = Convert.ToInt32(oxygenGenerator.ToString(), 2);
int co2ScrubberRate = Convert.ToInt32(co2Scrubber.ToString(), 2);

Console.WriteLine($"The answer is {oxygenGeneratorRate * co2ScrubberRate}");

string Calculate(IEnumerable<string> values, int position, bool takeGreatest)
{
    var amount = values.Count();
    if(amount == 1)
        return values.First();

    var hits = values.Select(x => x[position]).Count(x => x == '1');
    if ((hits * 2 >= amount && takeGreatest) || (hits * 2 < amount && !takeGreatest))
        return Calculate(values.Where(x => x[position] == '1'), position + 1, takeGreatest);
    else
        return Calculate(values.Where(x => x[position] == '0'), position + 1, takeGreatest);
}

//var result = File.ReadAllLines("input.txt")
//    .Select(x => x.Chunk(1).Select(a => a.First() == '1'))
//    .SelectMany(x => x.Select((y, z) => (z, y)))
//    .GroupBy(x => x.z, x => x.y)
//    .SelectSource()
//    .Select(x => (Calculate2(x, 0, true), Calculate2(x, 0, false)))
//    .Aggregate(0, (x, y) => x += Convert.ToInt32(y.Item1, 2) * Convert.ToInt32(y.Item2, 2));

//Console.WriteLine($"The answer is {result}");

//string Calculate2(IEnumerable<IEnumerable<bool>> values, int position, bool takeGreatest)
//{
//    string result = string.Empty;
//    var amount = values.Count();
//    if (amount == 1)
//        return values.First().Select(x => Convert.ToByte(x) + "").Aggregate((x, y) => $"{x}{y}");

//    var hits = values.Select(x => x.ElementAt(position)).Count(x => x);
//    if ((hits * 2 >= amount && takeGreatest) || (hits * 2 < amount && !takeGreatest))
//        return Calculate2(values.Where(x => x.ElementAt(position)), position + 1, takeGreatest);
//    else
//        return Calculate2(values.Where(x => !x.ElementAt(position)), position + 1, takeGreatest);
//}

//static class Extensions
//{
//    public static IEnumerable<TSource> SelectWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource> selector, Func<IEnumerable<TSource>, bool> condition)
//    {
//        var selected = source.Select(selector);
//        if (!condition(selected))
//            return source.Select(selector);

//        return selected.SelectWhile(selector, condition);
//    }

//    public static IEnumerable<IEnumerable<T>> SelectSource<T>(this IEnumerable<T> list) => list.Select(x => list);
//}