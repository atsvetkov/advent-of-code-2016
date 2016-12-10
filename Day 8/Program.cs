using System;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var screen = new Screen();
            
            foreach (var command in Puzzle.Input.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                screen.ExecuteCommand(command);
            }

            Console.WriteLine(screen);
            Console.WriteLine($"Pixels lit: {screen.GetNumberOfLitPixels()}");
        }
    }

    public class Screen
    {
        private const int RowsNumber = 6;
        private const int ColumnsNumber = 50;

        private bool[,] _pixels = new bool[RowsNumber, ColumnsNumber];

        public void ExecuteCommand(string command)
        {
            if (command.StartsWith("rect"))
            {
                var dimensionsPart = command.Substring(command.IndexOf(" ") + 1);
                var numbers = dimensionsPart.Split('x');
                var width = int.Parse(numbers[0]);
                var height = int.Parse(numbers[1]);

                Rect(width, height);
                
                return;
            }

            if (command.StartsWith("rotate column"))
            {
                var numbersPart = command.Substring(command.IndexOf("x"));
                var blocks = numbersPart.Split(new [] { " by " }, StringSplitOptions.RemoveEmptyEntries);
                var column = int.Parse(blocks[0].Substring(blocks[0].IndexOf("=") + 1));
                var shift = int.Parse(blocks[1]);

                RotateColumn(column, shift);
                return;
            }

            if (command.StartsWith("rotate row"))
            {
                var numbersPart = command.Substring(command.IndexOf("y"));
                var blocks = numbersPart.Split(new [] { " by " }, StringSplitOptions.RemoveEmptyEntries);
                var row = int.Parse(blocks[0].Substring(blocks[0].IndexOf("=") + 1));
                var shift = int.Parse(blocks[1]);

                RotateRow(row, shift);
                return;
            }

            Console.WriteLine($"Could not parse command: '{command}'");
        }

        public int GetNumberOfLitPixels()
        {
            var count = 0;
            for (var i = 0; i < RowsNumber; i++)
            {
                for (var j = 0; j < ColumnsNumber; j++)
                {
                    if (_pixels[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public void Rect(int width, int height)
        {
            if (width < 0 || width > ColumnsNumber || height < 0 || height > RowsNumber)
            {
                Console.WriteLine($"Incorrect size: ({width}, {height})");
                return;
            }
            
            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    TogglePixel(row, column, true);
                }
            }
        }

        public void RotateColumn(int column, int shift)
        {
            if (column < 0 || column >= ColumnsNumber)
            {
                Console.WriteLine($"Incorrect column: {column}");
                return;
            }

            var tempColumn = new bool[RowsNumber];
            for (var row = 0; row < RowsNumber; row++)
            {
                tempColumn[(row + shift) % RowsNumber] = _pixels[row, column];
            }

            SetColumn(column, tempColumn);
        }

        public void RotateRow(int row, int shift)
        {
            if (row < 0 || row >= RowsNumber)
            {
                Console.WriteLine($"Incorrect row: {row}");
                return;
            }

            var tempRow = new bool[ColumnsNumber];
            for (var column = 0; column < ColumnsNumber; column++)
            {
                tempRow[(column + shift) % ColumnsNumber] = _pixels[row, column];
            }

            SetRow(row, tempRow);
        }

        public void SetPixelOn(int row, int column)
        {
            TogglePixel(row, column, true);
        }

        public void SetPixelOff(int row, int column)
        {
            TogglePixel(row, column, false);
        }

        private void SetColumn(int columnIndex, bool[] columnValues)
        {
            for (var rowIndex = 0; rowIndex < RowsNumber; rowIndex++)
            {
                _pixels[rowIndex, columnIndex] = columnValues[rowIndex];
            }
        }

        private void SetRow(int rowIndex, bool[] rowValues)
        {
            for (var columnIndex = 0; columnIndex < ColumnsNumber; columnIndex++)
            {
                _pixels[rowIndex, columnIndex] = rowValues[columnIndex];
            }
        }

        private void TogglePixel(int row, int column, bool state)
        {
            if (row < 0 || row >= RowsNumber || column < 0 || column >= ColumnsNumber)
            {
                Console.WriteLine($"Incorrect coordinates: ({row}, {column})");
                return;
            }

            _pixels[row, column] = state;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < RowsNumber; i++)
            {
                for (var j = 0; j < ColumnsNumber; j++)
                {
                    builder.Append(_pixels[i, j] ? "#" : ".");
                }

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }

    public static class Puzzle
    {
        public static string Input =
@"rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 3
rect 1x1
rotate row y=0 by 3
rect 2x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 3
rect 2x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 3
rect 2x1
rotate row y=0 by 5
rect 4x1
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y=0 by 10
rotate column x=5 by 2
rotate column x=0 by 1
rect 9x1
rotate row y=2 by 5
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y=2 by 5
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate column x=40 by 1
rotate column x=27 by 1
rotate column x=22 by 1
rotate column x=17 by 1
rotate column x=12 by 1
rotate column x=7 by 1
rotate column x=2 by 1
rotate row y=2 by 5
rotate row y=1 by 3
rotate row y=0 by 5
rect 1x3
rotate row y=2 by 10
rotate row y=1 by 7
rotate row y=0 by 2
rotate column x=3 by 2
rotate column x=2 by 1
rotate column x=0 by 1
rect 4x1
rotate row y=2 by 5
rotate row y=1 by 3
rotate row y=0 by 3
rect 1x3
rotate column x=45 by 1
rotate row y=2 by 7
rotate row y=1 by 10
rotate row y=0 by 2
rotate column x=3 by 1
rotate column x=2 by 2
rotate column x=0 by 1
rect 4x1
rotate row y=2 by 13
rotate row y=0 by 5
rotate column x=3 by 1
rotate column x=0 by 1
rect 4x1
rotate row y=3 by 10
rotate row y=2 by 10
rotate row y=0 by 5
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=0 by 1
rect 4x1
rotate row y=3 by 8
rotate row y=0 by 5
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=0 by 1
rect 4x1
rotate row y=3 by 17
rotate row y=2 by 20
rotate row y=0 by 15
rotate column x=13 by 1
rotate column x=12 by 3
rotate column x=10 by 1
rotate column x=8 by 1
rotate column x=7 by 2
rotate column x=6 by 1
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 2
rotate column x=0 by 1
rect 14x1
rotate row y=1 by 47
rotate column x=9 by 1
rotate column x=4 by 1
rotate row y=3 by 3
rotate row y=2 by 10
rotate row y=1 by 8
rotate row y=0 by 5
rotate column x=2 by 2
rotate column x=0 by 2
rect 3x2
rotate row y=3 by 12
rotate row y=2 by 10
rotate row y=0 by 10
rotate column x=8 by 1
rotate column x=7 by 3
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=1 by 1
rotate column x=0 by 1
rect 9x1
rotate row y=0 by 20
rotate column x=46 by 1
rotate row y=4 by 17
rotate row y=3 by 10
rotate row y=2 by 10
rotate row y=1 by 5
rotate column x=8 by 1
rotate column x=7 by 1
rotate column x=6 by 1
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 2
rotate column x=1 by 1
rotate column x=0 by 1
rect 9x1
rotate column x=32 by 4
rotate row y=4 by 33
rotate row y=3 by 5
rotate row y=2 by 15
rotate row y=0 by 15
rotate column x=13 by 1
rotate column x=12 by 3
rotate column x=10 by 1
rotate column x=8 by 1
rotate column x=7 by 2
rotate column x=6 by 1
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=1 by 1
rotate column x=0 by 1
rect 14x1
rotate column x=39 by 3
rotate column x=35 by 4
rotate column x=20 by 4
rotate column x=19 by 3
rotate column x=10 by 4
rotate column x=9 by 3
rotate column x=8 by 3
rotate column x=5 by 4
rotate column x=4 by 3
rotate row y=5 by 5
rotate row y=4 by 5
rotate row y=3 by 33
rotate row y=1 by 30
rotate column x=48 by 1
rotate column x=47 by 5
rotate column x=46 by 5
rotate column x=45 by 1
rotate column x=43 by 1
rotate column x=38 by 3
rotate column x=37 by 3
rotate column x=36 by 5
rotate column x=35 by 1
rotate column x=33 by 1
rotate column x=32 by 5
rotate column x=31 by 5
rotate column x=30 by 1
rotate column x=23 by 4
rotate column x=22 by 3
rotate column x=21 by 3
rotate column x=20 by 1
rotate column x=12 by 2
rotate column x=11 by 2
rotate column x=3 by 5
rotate column x=2 by 5
rotate column x=1 by 3
rotate column x=0 by 4";
    }
}
