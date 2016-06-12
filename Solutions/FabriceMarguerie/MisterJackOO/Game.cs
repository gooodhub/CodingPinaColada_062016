using System;
using System.Drawing;
using System.Linq;

// https://github.com/gooodhub/CodingPinaColada_062016

namespace MisterJack
{
  public enum Direction { N, S, W, E }
  public enum Rotation { C, A }

  public class Game
  {
    private const String InvalidInput = "Invalid input";
    private const String ImpossibleMove = "Impossible move";

    private readonly Direction[,] _Map;
    private readonly Point _Jack;
    private readonly Random _Random;

    public Point Detective { get; private set; }

    public Game(Direction[,] map = null)
    {
      _Random = new Random();

      _Map = map ?? CreateMap();
      Detective = new Point(_Map.GetLowerBound(0), _Map.GetLowerBound(1));
      _Jack = GetRandomPoint();

      DrawMap();
    }

    public void Run()
    {
      while (Detective != _Jack)
      {
        var input = Console.ReadLine();
        if (input.Length != 1)
        {
          WriteError(InvalidInput);
          continue;
        }

        Direction direction;
        if (Enum.TryParse(input, ignoreCase: true, result: out direction))
        {
          Move(direction);
          DrawMap();
          continue;
        }

        Rotation rotation;
        if (Enum.TryParse(input, ignoreCase: true, result: out rotation))
        {
          Rotate(rotation);
          DrawMap();
          continue;
        }

        WriteError(InvalidInput);
      }

      WriteSuccess("You found Jack!");
      Console.ReadLine();
    }

    #region Private methods

    private Direction[,] CreateMap()
    {
      var maxTile = Enum.GetValues(typeof(Direction)).Cast<Int32>().Last();
      var map = new Direction[3, 3];
      for (var y = map.GetLowerBound(1); y <= map.GetUpperBound(1); y++)
      {
        for (var x = map.GetLowerBound(0); x <= map.GetUpperBound(0); x++)
        {
          var tile = (Direction) _Random.Next(maxTile + 1);
          map[x, y] = tile;
        }
      }
      return map;
    }

    private void DrawMap()
    {
      Console.WriteLine();

      for (var y = _Map.GetLowerBound(1); y <= _Map.GetUpperBound(1); y++)
      {
        for (var x = _Map.GetLowerBound(0); x <= _Map.GetUpperBound(0); x++)
        {
          var point = new Point(x, y);
          var isDetectiveOnPoint = point == Detective;
          var isJackOnPoint = point == _Jack;
          var color = isDetectiveOnPoint ? ConsoleColor.DarkGreen : isJackOnPoint ? ConsoleColor.Red : ConsoleColor.White;
          Write(_Map[x, y], color);
        }
        Console.WriteLine();
      }

      Console.WriteLine();

      Write("Jack: ", ConsoleColor.Red);
      Write(_Jack, ConsoleColor.White);
      Console.WriteLine();

      Write("Detective: ", ConsoleColor.DarkGreen);
      Write(Detective, ConsoleColor.White);
      Console.WriteLine();

      Console.WriteLine();
    }

    public void Move(Direction direction)
    {
      if (_Map[Detective.X, Detective.Y] == direction)
      {
        WriteError(ImpossibleMove);
        return;
      }

      switch (direction)
      {
        case Direction.N:
          if (Detective.Y <= _Map.GetLowerBound(1))
          {
            WriteError(ImpossibleMove);
            return;
          }
          Detective = new Point(Detective.X, Detective.Y - 1);
          return;
        case Direction.S:
          if (Detective.Y >= _Map.GetUpperBound(1))
          {
            WriteError(ImpossibleMove);
            return;
          }
          Detective = new Point(Detective.X, Detective.Y + 1);
          return;
        case Direction.W:
          if (Detective.X <= _Map.GetLowerBound(0))
          {
            WriteError(ImpossibleMove);
            return;
          }
          Detective = new Point(Detective.X - 1, Detective.Y);
          return;
        case Direction.E:
          if (Detective.X >= _Map.GetUpperBound(0))
          {
            WriteError(ImpossibleMove);
            return;
          }
          Detective = new Point(Detective.X + 1, Detective.Y);
          return;
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.");
      }
    }

    private void Rotate(Rotation rotation)
    {
      var direction = _Map[Detective.X, Detective.Y];

      switch (direction)
      {
        case Direction.N:
          switch (rotation)
          {
            case Rotation.C:
              direction = Direction.E;
              break;
            case Rotation.A:
              direction = Direction.W;
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof(rotation), rotation, "Invalid rotation.");
          }
          break;
        case Direction.S:
          switch (rotation)
          {
            case Rotation.C:
              direction = Direction.W;
              break;
            case Rotation.A:
              direction = Direction.E;
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof(rotation), rotation, "Invalid rotation.");
          }
          break;
        case Direction.W:
          switch (rotation)
          {
            case Rotation.C:
              direction = Direction.N;
              break;
            case Rotation.A:
              direction = Direction.S;
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof(rotation), rotation, "Invalid rotation.");
          }
          break;
        case Direction.E:
          switch (rotation)
          {
            case Rotation.C:
              direction = Direction.S;
              break;
            case Rotation.A:
              direction = Direction.N;
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof(rotation), rotation, "Invalid rotation.");
          }
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.");
      }

      _Map[Detective.X, Detective.Y] = direction;
    }

    private Point GetRandomPoint()
    {
      return new Point(
        x: _Random.Next(_Map.GetLowerBound(0), _Map.GetUpperBound(0) + 1),
        y: _Random.Next(_Map.GetLowerBound(1), _Map.GetUpperBound(1) + 1)
      );
    }

    private static void WriteError(Object text)
    {
      Write(text, ConsoleColor.Red);
      Console.WriteLine();
    }

    private static void WriteSuccess(Object text)
    {
      Write(text, ConsoleColor.Green);
      Console.WriteLine();
    }

    private static void Write(Object text, ConsoleColor color)
    {
      Console.ForegroundColor = color;
      try
      {
        Console.Write(text);
      }
      finally
      {
        Console.ResetColor();
      }
    }
 
    #endregion Private methods
  }
}