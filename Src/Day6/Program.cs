using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helper.WriteResult(Test1, FileType.Test1Sample);
            Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static int Test1(List<string> input)
        {
            foreach(var item in input)
            {
                var result = IndexOfUnique(Parse(item), 4);
                Console.WriteLine($"Found a unique sequence on {(result + 4).ToString().Pastel(Color.Green)} on value {string.Join("", item.Skip(result).Take(4)).Pastel(Color.Green)}");
            }
            return 0;
        }

        private static int Test2(List<string> input)
        {
            foreach (var item in input)
            {
                var result = IndexOfUnique(Parse(item), 14);
                Console.WriteLine($"Found a unique sequence on {(result + 14).ToString().Pastel(Color.Green)} on value {string.Join("", item.Skip(result).Take(4)).Pastel(Color.Green)}");
            }
            return 0;
        }
        public static List<char> Parse(string input)
        {
            return input.ToList();
        }
        public static int IndexOfUnique(List<char> input, int size)
        {
            foreach(var i in Enumerable.Range(0, input.Count - size))
            {
                if (input.Skip(i).Take(size).Distinct().Count() == size)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
