using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string input = @"URULLLLLRLDDUURRRULLLDURRDRDRDLURURURLDLLLLRUDDRRLUDDDDDDLRLRDDDUUDUDLDULUDLDURDULLRDDURLLLRRRLLRURLLUDRDLLRRLDDRUDULRRDDLUUUDRLDLURRRULURRDLLLDDDLUDURDDRLDDDLLRULDRUDDDLUDLURUDLLRURRUURUDLLLUUUUDDURDRDDDLDRRUDURDLLLULUDURURDUUULRULUDRUUUUDLRLUUUUUDDRRDDDURULLLRRLDURLDLDRDLLLUULLRRLLLLDRLRDRRDRRUDDLULUUDDDDRRUUDDLURLRDUUDRRLDUDLRRRLRRUUDURDRULULRDURDRRRDLDUUULRDDLRLRDLUUDDUDDRLRRULLLULULLDDDRRDUUUDDRURDDURDRLRDLDRDRULRLUURUDRLULRLURLRRULDRLRDUDLDURLLRLUDLUDDURDUURLUDRLUL
LLLUUURUULDDDULRRDLRLLLLLLLLRURRDLURLUDRRDDULDRRRRRRLDURRULDDULLDDDRUUDLUDULLDLRRLUULULRULURDURLLDULURDUDLRRLRLLDULLRLDURRUULDLDULLRDULULLLULDRLDLDLDLDDLULRLDUDRULUDDRDDRLRLURURRDULLUULLDRRDRRDLDLLRDLDDUUURLUULDDRRRUULDULDDRDDLULUDRURUULLUDRURDRULDRUULLRRDURUDDLDUULLDDRLRRDUDRLRRRLDRLRULDRDRRUDRLLLDDUDLULLURRURRLUURDRLLDLLDUDLUUURRLRDDUDRLUDLLRULLDUUURDLUUUDUDULRLDLDRUUDULRDRRUDLULRLRDLDRRDDDUDLDLDLRUURLDLLUURDLDLRDLDRUDDUURLLLRDRDRRULLRLRDULUDDDLUDURLDUDLLRULRDURDRDLLULRRDLLLDUURRDUDDLDDRULRRRRLRDDRURLLRRLLL
DRURLDDDDRLUDRDURUDDULLRRLLRLDDRLULURLDURRLDRRLRLUURDDRRDLRDLDLULDURUDRLRUDULRURURLRUDRLLDDUDDRDLDRLLDDLRRDRUUULDUUDRUULRLLDLLULLLRRDRURDLDDRRDDUDDULLDUUULDRUDLDLURLDRURUDLRDDDURRLRDDUDLLLRRUDRULRULRRLLUUULDRLRRRLLLDLLDUDDUUDRURLDLRRUUURLUDDDRRDDLDDDDLUURDDULDRLRURLULLURRDRLLURLLLURDURLDLUDUUDUULLRLDLLLLULRDDLDUDUDDDUULURRLULDLDRLRDRLULLUDDUUUUURDRURLDUULDRRDULUDUDLDDRDLUDDURUDURLDULRUDRRDLRLRDRRURLDLURLULULDDUUDLRLLLLURRURULDDRUUULLDULDRDULDDDLLLRLULDDUDLRUDUDUDURLURLDDLRULDLURD
DRUDRDURUURDLRLUUUUURUDLRDUURLLDUULDUULDLURDDUULDRDDRDULUDDDRRRRLDDUURLRDLLRLRURDRRRDURDULRLDRDURUDLLDDULRDUDULRRLLUDLLUUURDULRDDLURULRURDDLRLLULUDURDRRUDLULLRLDUDLURUDRUULDUDLRDUDRRDULDDLDRLRRULURULUURDULRRLDLDULULRUUUUULUURLURLRDLLRRRRLURRUDLRLDDDLDRDRURLULRDUDLRLURRDRRLRLLDLDDLLRRULRLRLRUDRUUULLDUULLDDRLUDDRURLRLDLULDURLLRRLDLLRDDDUDDUULLUDRUDURLLRDRUDLUDLLUDRUUDLRUURRRLLUULLUUURLLLRURUULLDLLDURUUUULDDDLRLURDRLRRRRRRUDLLLRUUULDRRDLRDLLDRDLDDLDLRDUDLDDRDDDDRULRRLRDULLDULULULRULLRRLLUURUUUDLDLUDUDDDLUUDDDDUDDDUURUUDRDURRLUULRRDUUDDUDRRRDLRDRLDLRRURUUDRRRUUDLDRLRDURD
DDDLRURUDRRRURUUDLRLRDULDRDUULRURRRUULUDULDDLRRLLRLDDLURLRUDRLRRLRDLRLLDDLULDLRRURDDRDLLDDRUDRRRURRDUDULUDDULRRDRLDUULDLLLDRLUDRDURDRRDLLLLRRLRLLULRURUUDDRULDLLRULDRDLUDLULDDDLLUULRRLDDUURDLULUULULRDDDLDUDDLLLRRLLLDULRDDLRRUDDRDDLLLLDLDLULRRRDUDURRLUUDLLLLDUUULDULRDRULLRDRUDULRUUDULULDRDLDUDRRLRRDRLDUDLULLUDDLURLUUUDRDUDRULULDRDLRDRRLDDRRLUURDRULDLRRLLRRLDLRRLDLDRULDDRLURDULRRUDURRUURDUUURULUUUDLRRLDRDLULDURUDUDLUDDDULULRULDRRRLRURLRLRLUDDLUUDRRRLUUUDURLDRLRRDRRDURLLL";

            Console.WriteLine($"First bathroom code: {KeypadDecoder.Decode(KeypadFactory.CreateFirstBathroomKeypad(), input)}");
            Console.WriteLine($"Second bathroom code: {KeypadDecoder.Decode(KeypadFactory.CreateSecondBathroomKeypad(), input)}");
        }
    }

    public static class KeypadDecoder
    {
        public static string Decode(Keypad keypad, string input)
        {
            var result = new StringBuilder();
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var directions = ParseLine(line);
                foreach (var direction in directions)
                {
                    keypad.Move(direction);
                }

                result.Append(keypad.Current);
            }

            return result.ToString();
        }

        private static IEnumerable<Direction> ParseLine(string commandLine)
        {
            if (string.IsNullOrEmpty(commandLine))
            {
                yield break;
            }

            foreach (var symbol in commandLine)
            {
                yield return (Direction)Enum.Parse(typeof(Direction), symbol.ToString());
            }
        }
    }

    public static class KeypadFactory
    {
        public static Keypad CreateFirstBathroomKeypad()
        {
            var one = new Key("1");
            var two = new Key("2");
            var three = new Key("3");
            var four = new Key("4");
            var five = new Key("5");
            var six = new Key("6");
            var seven = new Key("7");
            var eight = new Key("8");
            var nine = new Key("9");

            one.Right = two;
            one.Down = four;

            two.Left = one;
            two.Right = three;
            two.Down = five;

            three.Left = two;
            three.Down = six;

            four.Up = one;
            four.Right = five;
            four.Down = seven;

            five.Left = four;
            five.Up = two;
            five.Right = six;
            five.Down = eight;

            six.Left = five;
            six.Up = three;
            six.Down = nine;

            seven.Up = four;
            seven.Right = eight;

            eight.Left = seven;
            eight.Up = five;
            eight.Right = nine;

            nine.Left = eight;
            nine.Up = six;

            return new Keypad(five);
        }

        public static Keypad CreateSecondBathroomKeypad()
        {
            var one = new Key("1");
            var two = new Key("2");
            var three = new Key("3");
            var four = new Key("4");
            var five = new Key("5");
            var six = new Key("6");
            var seven = new Key("7");
            var eight = new Key("8");
            var nine = new Key("9");
            var a = new Key("A");
            var b = new Key("B");
            var c = new Key("C");
            var d = new Key("D");

            one.Down = three;

            two.Right = three;
            two.Down = six;

            three.Left = two;
            three.Right = four;
            three.Up = one;
            three.Down = seven;

            four.Left = three;
            four.Down = eight;

            five.Right = six;
            
            six.Left = five;
            six.Up = two;
            six.Down = a;
            six.Right = seven;

            seven.Up = three;
            seven.Right = eight;
            seven.Down = b;
            seven.Left = six;

            eight.Left = seven;
            eight.Up = four;
            eight.Right = nine;
            eight.Down = c;

            nine.Left = eight;
            
            a.Up = six;
            a.Right = b;

            b.Left = a;
            b.Up = seven;
            b.Right = c;
            b.Down = d;

            c.Left = b;
            c.Up = eight;

            d.Up = b;

            return new Keypad(five);
        }
    }

    public class Keypad
    {
         public Keypad(Key startKey)
         {
             Current = startKey;
         }

         public Key Current { get; private set;}

         public void Move(Direction direction)
         {
             switch (direction)
             {
                 case Direction.U:
                    TryMoveTo(Current.Up);
                    break;
                 case Direction.R:
                    TryMoveTo(Current.Right);
                    break;
                 case Direction.D:
                    TryMoveTo(Current.Down);
                    break;
                 case Direction.L:
                    TryMoveTo(Current.Left);
                    break;
                 default:
                    throw new NotSupportedException($"Moving to '{direction}' direction is not supported");
             }
         }

         private void TryMoveTo(Key key)
         {
             if (key == null)
             {
                 return;
             }

             Current = key;
         }
    }

    public class Key
    {
        public Key(string label)
        {
            Label = label;
        }

        public string Label {get;}

        public Key Up {get;set;}
        public Key Right {get;set;}
        public Key Down {get;set;}
        public Key Left {get;set;}

        public override string ToString()
        {
            return Label;
        }
    }

    public enum Direction
    {
        None,
        U,
        R,
        D,
        L
    }
}
