using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MisterJack.UnitTests
{
  public class Class1
  {
    public class UnitTest1
    {
      //[Fact]
      //public void Board_Create_Has_All_Pieces()
      //{
      //  Assert.Equal(9, _board.Pieces.Length);
      //}

      [Fact]
      public void Board_Player_Can_Move()
      {
        // Arrange
        var map = new[,] {
                { Direction.W, Direction.N, Direction.N },
                { Direction.S, Direction.N, Direction.N },
                { Direction.N, Direction.N, Direction.N }
            };
        var game = new Game(map);

        // Act
        game.Move(Direction.S);

        // Assert
        Assert.Equal(0, game.Detective.X);
        Assert.Equal(1, game.Detective.Y);
      }

      /*
      [TestMethod]
      public void Board_Player_Cannot_Move_Out_Of_Board()
      {
        // Arrange
        var firstPiece = new Piece(Orientation.West);
        var southPiece = new Piece(Orientation.South);

        _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { southPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

        _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);

        // Act
        _board.MovePlayer(Orientation.North);

        // Assert
        Assert.AreEqual(0, _board.PlayerCurrentPiece.Item1);
        Assert.AreEqual(0, _board.PlayerCurrentPiece.Item2);
      }

      [TestMethod]
      public void Board_Player_Cannot_Cross_Wall()
      {
        // Arrange
        var firstPiece = new Piece(Orientation.East);

        _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

        _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);

        // Act
        _board.MovePlayer(Orientation.East);

        // Assert
        Assert.AreEqual(0, _board.PlayerCurrentPiece.Item1);
        Assert.AreEqual(0, _board.PlayerCurrentPiece.Item2);
      }

      [TestMethod]
      public void Board_Player_Can_Rotate_ClockWise()
      {
        // Arrange
        var firstPiece = new Piece(Orientation.West);

        _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

        _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);

        // Act
        _board.Rotate(Direction.ClockWise, new Tuple<int, int>(0, 0));

        // Assert
        Assert.AreEqual(Orientation.North, firstPiece.Wall);
      }

      [TestMethod]
      public void Board_Player_Can_Rotate_AntiClockWise()
      {
        // Arrange
        var firstPiece = new Piece(Orientation.North);

        _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

        _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);

        // Act
        _board.Rotate(Direction.ClockWise, new Tuple<int, int>(0, 0));

        // Assert
        Assert.AreEqual(Orientation.East, firstPiece.Wall);
      }

      [TestMethod]
      public void Board_Player_Can_Find_Culprit()
      {
        // Arrange
        var firstPiece = new Piece(Orientation.North);

        _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

        _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);
        _board.Culprit = new Tuple<int, int>(0, 0);

        // Act
        var gameOver = _board.IsGameOver();

        // Assert
        Assert.AreEqual(true, gameOver);
      }

      [TestMethod]
      public void Board_Player_Cannot_Find_Culprit()
      {
        // Arrange
        var firstPiece = new Piece(Orientation.North);

        _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

        _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);
        _board.Culprit = new Tuple<int, int>(3, 3);

        // Act
        var gameOver = _board.IsGameOver();

        // Assert
        Assert.AreEqual(false, gameOver);
      }
      */
    }
  }
}
