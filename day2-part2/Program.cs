var result = File.ReadAllLines("input.txt")
    .SelectMany(line => line.Split(' '))
    .Chunk(2)
    .Select(x => new Command(x[0], int.Parse(x[1])))
    .Aggregate(new Position(), (x, y) => x + ProcessCommand(y));

Console.WriteLine($"The answer is {result.Horizontal * result.Vertical}");

Position ProcessCommand(Command command) => command.Direction switch
{
    "forward" => new Position(Horizontal: command.Length, Vertical: command.Length),
    "down" => new Position(Aim: command.Length),
    "up" => new Position(Aim: -command.Length),
};
public record Command(string Direction, int Length);
public record Position(int Horizontal = 0, int Vertical = 0, int Aim = 0)
{
    public static Position operator +(Position a, Position b) =>
        new(a.Horizontal + b.Horizontal, a.Vertical + b.Vertical * a.Aim, a.Aim + b.Aim);
}