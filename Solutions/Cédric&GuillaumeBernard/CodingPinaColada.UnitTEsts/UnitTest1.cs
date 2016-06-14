using System;
using System.Collections.Generic;
using HideAndSeek.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingPinaColada.UnitTEsts
{
    [TestClass]
    public class UnitTest1
    {
        private Board _board;

        [TestInitialize]
        public void TestInit()
        {
            _board = new Board();
        }

        [TestMethod]
        public void Board_Create_Has_All_Pieces()
        {
            Assert.AreEqual(9, _board.Pieces.Length);
        }

        [TestMethod]
        public void Board_Player_Can_Move()
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
            _board.MovePlayer(Orientation.South);

            // Assert
            Assert.AreEqual(1, _board.PlayerCurrentPiece.Item1);
            Assert.AreEqual(0, _board.PlayerCurrentPiece.Item2);
        }

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

        [TestMethod]
        public void Board_Player_Can_Find_The_Ripper_In_3_0()
        {
            // Arrange
            var firstPiece = new Piece(Orientation.West);

            _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

            _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);
            _board.Culprit = new Tuple<int, int>(2,0);

            _board.MovePlayerUntilFind();

            Assert.AreEqual(true,_board.IsGameOver());
        }

        [TestMethod]
        public void Board_Player_Can_Find_The_Ripper_In_2_2()
        {
            // Arrange
            var firstPiece = new Piece(Orientation.West);

            _board.Pieces = new[,] {
                { firstPiece, new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };

            _board.PlayerCurrentPiece = new Tuple<int, int>(0, 0);
            _board.Culprit = new Tuple<int, int>(2,2);

            _board.MovePlayerUntilFind();

            Assert.AreEqual(true, _board.IsGameOver());
        }
    }
}
