using System;
using System.Drawing;
using System.Linq;

// https://github.com/gooodhub/CodingPinaColada_062016

namespace MisterJack
{
  class Program
  {
    private enum Direction { N, S, W, E }
    private enum Rotation { C, A }

    static void Main(string[] args)
    {
      var random = new Random();

      var map = CreateMap(random);
      var detective = new Point(map.GetLowerBound(0), map.GetLowerBound(1));
      var jack = GetRandomPoint(random, map);

      DrawMap(map, detective, jack);

      while (detective != jack)
      {
        var input = Console.ReadLine();
        if (input.Length != 1)
        {
          WriteError("Invalid input");
          continue;
        }

        Direction direction;
        if (Enum.TryParse(input, ignoreCase: true, result: out direction))
        {
          detective = Move(detective, direction, map);
          DrawMap(map, detective, jack);
          continue;
        }

        Rotation rotation;
        if (Enum.TryParse(input, ignoreCase: true, result: out rotation))
        {
          Rotate(map, detective, rotation);
          DrawMap(map, detective, jack);
          continue;
        }

        WriteError("Invalid input");
    }

      WriteSuccess("You found Jack!");
      Console.ReadLine();
    }

    private static Direction[,] CreateMap(Random random)
    {
      var maxTile = Enum.GetValues(typeof(Direction)).Cast<Int32>().Last();
      var map = new Direction[3, 3];
      for (var y = map.GetLowerBound(1); y <= map.GetUpperBound(1); y++)
      {
        for (var x = map.GetLowerBound(0); x <= map.GetUpperBound(0); x++)
        {
          var tile = (Direction) random.Next(maxTile + 1);
          map[x, y] = tile;
        }
      }
      return map;
    }

    private static void DrawMap(Direction[,] map, Point detective, Point jack)
    {
      Console.WriteLine();

      for (var y = map.GetLowerBound(1); y <= map.GetUpperBound(1); y++)
      {
        for (var x = map.GetLowerBound(0); x <= map.GetUpperBound(0); x++)
        {
          var point = new Point(x, y);
          var isDetectiveOnPoint = point == detective;
          var isJackOnPoint = point == jack;
          var color = isDetectiveOnPoint ? ConsoleColor.DarkGreen : isJackOnPoint ? ConsoleColor.Red : ConsoleColor.White;
          Write(map[x, y], color);
        }
        Console.WriteLine();
      }

      Console.WriteLine();

      Write("Jack: ", ConsoleColor.Red);
      Write(jack, ConsoleColor.White);
      Console.WriteLine();

      Write("Detective: ", ConsoleColor.DarkGreen);
      Write(detective, ConsoleColor.White);
      Console.WriteLine();

      Console.WriteLine();
    }

    private static Point Move(Point point, Direction direction, Direction[,] map)
    {
      if (map[point.X, point.Y] == direction)
      {
        WriteError("Impossible move");
        return point;
      }

      switch (direction)
      {
        case Direction.N:
          if (point.Y <= map.GetLowerBound(1))
          {
            WriteError("Impossible move");
            return point;
          }
          return new Point(point.X, point.Y-1);
        case Direction.S:
          if (point.Y >= map.GetUpperBound(1))
          {
            WriteError("Impossible move");
            return point;
          }
          return new Point(point.X, point.Y+1);
        case Direction.W:
          if (point.X <= map.GetLowerBound(0))
          {
            WriteError("Impossible move");
            return point;
          }
          return new Point(point.X-1, point.Y);
        case Direction.E:
          if (point.X >= map.GetUpperBound(0))
          {
            WriteError("Impossible move");
            return point;
          }
          return new Point(point.X+1, point.Y);
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.");
      }
    }

    private static void Rotate(Direction[,] map, Point detective, Rotation rotation)
    {
      var direction = map[detective.X, detective.Y];

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

      map[detective.X, detective.Y] = direction;
    }

    private static Point GetRandomPoint(Random random, Direction[,] map)
    {
      return new Point(
        x: random.Next(map.GetLowerBound(0), map.GetUpperBound(0) + 1),
        y: random.Next(map.GetLowerBound(1), map.GetUpperBound(1) + 1)
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
  }
}