// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var lines = await File.ReadAllLinesAsync("input.txt");
var draws = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

var boards = new List<Board>();
var boardLines = lines.Skip(1).Chunk(6).Select(x => x.Skip(1));
foreach(var boardData in boardLines)
{
    var board = new Board();
    (int X, int Y) position = (0, 0);
    foreach(var line in boardData)
    {
        foreach(var number in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            board.Nodes.Add(new BoardNode(int.Parse(number), position));
            position.X++;
        }

        position.X = 0;
        position.Y--;
    }

    boards.Add(board);
}

Board winningBoard = null;
int lastDraw = 0;
foreach(var draw in draws)
{
    lastDraw = draw;
    foreach(var board in boards)
    {
        foreach(var node in board.Nodes)
        {
            if (node.Value != draw)
                continue;

            node.Drawn = true;

            if (board.Nodes.Where(x => x.Position.X == node.Position.X).Any(x => !x.Drawn) &&
                board.Nodes.Where(x => x.Position.Y == node.Position.Y).Any(x => !x.Drawn))
                continue;

            winningBoard = board;
            break;
        }
    }

    if (winningBoard != null)
        break;
}

var result = winningBoard.Nodes.Where(x => !x.Drawn).Sum(x => x.Value);
Debug.WriteLine($"The answer is {result * lastDraw}");

class Board
{
    public Board()
    {
        Nodes = new List<BoardNode>();
    }

    public ICollection<BoardNode> Nodes { get; set; }
}

class BoardNode
{
    public BoardNode(int value, (int X, int Y) position)
    {
        Value = value;
        Position = position;
    }

    public int Value { get; set; }

    public (int X, int Y) Position { get; set; }

    public bool Drawn { get; set; }
}