var result = File.ReadAllLines("input.txt")
    .SelectMany(line => line.Split(' '))
    .Chunk(2)
    .Select(x => new Command(x[0], int.Parse(x[1])))
    .Aggregate(new Position(), (x, y) => x + ProcessCommand(y));

Console.WriteLine($"The answer is {result.Horizontal * result.Vertical}");

Position ProcessCommand(Command command) => command.Direction switch
{
    "forward" => new Position(Horizontal: command.Length),
    "down" => new Position(Vertical: command.Length),
    "up" => new Position(Vertical: -command.Length),
};
public record Command(string Direction, int Length);
public record Position(int Horizontal = 0, int Vertical = 0)
{
    public static Position operator +(Position a, Position b) => 
        new(a.Horizontal + b.Horizontal, a.Vertical + b.Vertical);
}