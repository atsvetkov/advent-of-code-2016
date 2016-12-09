using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cracker = new PasswordCracker();
            Console.WriteLine($"Input: {Puzzle.Input}");
            Console.WriteLine($"Cracked password (part 1): {cracker.Crack1(Puzzle.Input)}");
            Console.WriteLine($"Cracked password (part 2): {cracker.Crack2(Puzzle.Input)}");
        }
    }

    public class PasswordCracker
    {
        private const int NumberOfPasswordCharacters = 8;
        private const char Empty = ' '; 

        private MD5 _md5 = MD5.Create();

        public string Crack1(string doorId)
        {
            var result = new StringBuilder();
            var number = -1;
            while (result.Length < NumberOfPasswordCharacters)
            {
                number++;
                var hash = CalculateMD5Hash(doorId + number);
                if (!IsHashApplicable(hash))
                {
                    continue;
                }

                result.Append(hash[5]);
            }

            return result.ToString();
        }

        public string Crack2(string doorId)
        {
            var result = new char[8] { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty };
            var number = -1;
            while (!IsPasswordCracked(result))
            {
                number++;
                var hash = CalculateMD5Hash(doorId + number);
                if (!IsHashApplicable2(hash))
                {
                    continue;
                }

                var position = int.Parse(hash[5].ToString());
                var character = hash[6];
                if (result[position] != Empty)
                {
                    continue;
                }

                result[position] = character;
            }

            return new String(result);
        }

        private bool IsPasswordCracked(char[] chars)
        {
            return chars.All(c => c != Empty);
        }

        private bool IsHashApplicable(string hash)
        {
            return hash.Substring(0, 5) == "00000";
        }

        private bool IsHashApplicable2(string hash)
        {
            int position;
            if (!int.TryParse(hash[5].ToString(), out position))
            {
                return false;
            }

            return hash.Substring(0, 5) == "00000" && position < NumberOfPasswordCharacters;
        }

        private string CalculateMD5Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = _md5.ComputeHash(inputBytes);
            var builder = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }

            return builder.ToString();
        }
    }

    public static class Puzzle
    {
        public static string Input =
@"reyedfim";
    }
}