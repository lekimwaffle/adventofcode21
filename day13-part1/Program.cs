// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
ICollection<(int X, int Y)> dots = lines.TakeWhile(x => !string.IsNullOrEmpty(x)).Select(x =>
{
    var splitted = x.Split(',').Select(x => int.Parse(x)).ToArray();
    return (splitted[0], splitted[1]);
}).ToList();

IEnumerable<(string Axis, int Line)> foldInstructions = lines.SkipWhile(x => !string.IsNullOrEmpty(x)).Skip(1).Select(x =>
{
    var splitted = x.Substring(11).Split('=');
    return (splitted[0], int.Parse(splitted[1]));
});

foreach(var foldInstruction in foldInstructions.Take(1))
{
    if(foldInstruction.Axis == "x")
    {
        var dotsToFold = dots.Where(x => x.X > foldInstruction.Line).ToArray();
        for(int i = 0; i < dotsToFold.Length; i++)
        {
            dots.Remove(dotsToFold[i]);
            dotsToFold[i].X = foldInstruction.Line - (dotsToFold[i].X - foldInstruction.Line);
            if (!dots.Contains(dotsToFold[i]))
                dots.Add(dotsToFold[i]);
        }
    } 
    else if (foldInstruction.Axis == "y")
    {
        var dotsToFold = dots.Where(x => x.Y > foldInstruction.Line).ToArray();
        for (int i = 0; i < dotsToFold.Length; i++)
        {
            dots.Remove(dotsToFold[i]);
            dotsToFold[i].Y = foldInstruction.Line - (dotsToFold[i].Y - foldInstruction.Line);
            if (!dots.Contains(dotsToFold[i]))
                dots.Add(dotsToFold[i]);
        }
    }
}

Debug.WriteLine($"The answer is { dots.Count }");