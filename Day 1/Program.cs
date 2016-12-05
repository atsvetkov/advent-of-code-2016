using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;
using static System.String;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "R3, R1, R4, L4, R3, R1, R1, L3, L5, L5, L3, R1, R4, L2, L1, R3, L3, R2, R1, R1, L5, L2, L1, R2, L4, R1, L2, L4, R2, R2, L2, L4, L3, R1, R4, R3, L1, R1, L5, R4, L2, R185, L2, R4, R49, L3, L4, R5, R1, R1, L1, L1, R2, L1, L4, R4, R5, R4, L3, L5, R1, R71, L1, R1, R186, L5, L2, R5, R4, R1, L5, L2, R3, R2, R5, R5, R4, R1, R4, R2, L1, R4, L1, L4, L5, L4, R4, R5, R1, L2, L4, L1, L5, L3, L5, R2, L5, R4, L4, R3, R3, R1, R4, L1, L2, R2, L1, R4, R2, R2, R5, R2, R5, L1, R1, L4, R5, R4, R2, R4, L5, R3, R2, R5, R3, L3, L5, L4, L3, L2, L2, R3, R2, L1, L1, L5, R1, L3, R3, R4, R5, L3, L5, R1, L3, L5, L5, L2, R1, L3, L1, L3, R4, L1, R3, L2, L2, R3, R3, R4, R4, R1, L4, R1, L5";

            var position = new Position();
            foreach (var moveString in input.Split(',').Select(s => s.Trim()))
            {
                position.Move(Move.Parse(moveString));
            }

            Console.WriteLine($"Total taxicab distance: {position.GetTaxicabDistance()}");
        }
    }

    public class Position
    {
        private Point _current;
        private Point _previous;

        private HashSet<Point> _path = new HashSet<Point>();

        public int GetTaxicabDistance()
        {
            return _current.GetTaxicabDistance();
        }

        public void Move(Move move)
        {
            Move(move.Turn, move.Steps);
        }

        private void Move(Turn turn, int steps)
        {
            var direction = GetDirection(turn);

            _previous = _current;
                        
            switch (direction)
            {
                case Direction.Up:
                    for (var i = 1; i <= steps; i++)
                    {
                        AddPathPoint(new Point(_current.X, _current.Y + i));
                    }
                    
                    _current.Y += steps;

                    break;
                case Direction.Right:
                    for (var i = 1; i <= steps; i++)
                    {
                        AddPathPoint(new Point(_current.X + i, _current.Y));
                    }

                    _current.X += steps;

                    break;
                case Direction.Down:
                    for (var i = 1; i <= steps; i++)
                    {
                        AddPathPoint(new Point(_current.X, _current.Y - i));
                    }

                    _current.Y -= steps;

                    break;
                case Direction.Left:
                    for (var i = 1; i <= steps; i++)
                    {
                        AddPathPoint(new Point(_current.X - i, _current.Y));
                    }

                    _current.X -= steps;

                    break;
                default:
                    throw new NotSupportedException($"Cannot move to '{direction}' direction");
            }
        }

        private void AddPathPoint(Point point)
        {
            if (!_path.Add(point))
            {
                Console.WriteLine($"Already been at point ({point.X},{point.Y}), which has a taxicab distance of {point.GetTaxicabDistance()}");
            }
        }

        private Direction GetDirection(Turn turn)
        {
            var xDelta = _current.X - _previous.X;
            var yDelta = _current.Y - _previous.Y;

            if (xDelta == 0 && yDelta == 0)
            {
                return turn == Turn.Left ? Direction.Left : Direction.Right;
            }

            if (xDelta > 0)
            {
                return turn == Turn.Left ? Direction.Up : Direction.Down;
            }
            else if (xDelta < 0)
            {
                return turn == Turn.Left ? Direction.Down : Direction.Up;
            }

            if (yDelta > 0)
            {
                return turn == Turn.Left ? Direction.Left : Direction.Right;
            }
            else if (yDelta < 0)
            {
                return turn == Turn.Left ? Direction.Right : Direction.Left;
            }

            return Direction.None;
        }
    }

    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        public int GetTaxicabDistance()
        {
            return Abs(X) + Abs(Y);
        }
    }

    public struct Move
    {
        public Turn Turn;
        public int Steps;

        public static Move Parse(string moveString)
        {
            if (IsNullOrEmpty(moveString))
            {
                throw new ArgumentException("Cannot parse move string: {moveString}");
            }

            var result = new Move();

            var directionChar = moveString[0];
            if (directionChar == 'R')
            {
                result.Turn = Turn.Right;
            }
            else if (directionChar == 'L')
            {
                result.Turn = Turn.Left;
            }

            result.Steps = int.Parse(moveString.Substring(1));

            return result;
        }
    }

    public enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    public enum Turn
    {
        Left,
        Right
    }
}
