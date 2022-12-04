using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helper.WriteResult(Test1, FileType.Test1Sample);
            //Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static int Test1(List<string> input)
        {
            var output = 0;
            var prios = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            foreach (var item in input)
            {
                var comps = item.Split(item.Length/2).ToList();
                var incorrect = comps[0].Intersect(comps[1]).Single();
                var prio = prios.IndexOf(incorrect) + 1;
                output += prio;
                Console.WriteLine($"Incorrect is: {incorrect.ToString().Pastel(Color.Red)} with prio: {prio.ToString().Pastel(Color.Red)}");
            }
            return output;
        }

        private static int Test2(List<string> input)
        {
            var output = 0;
            var prios = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            foreach (var item in input.Batch(3))
            {
                var incorrect = item.First().Intersect(item.Skip(1).First()).Intersect(item.Last()).Single();
                var prio = prios.IndexOf(incorrect) + 1;
                output += prio;
                Console.WriteLine($"Incorrect is: {incorrect.ToString().Pastel(Color.Red)} with prio: {prio.ToString().Pastel(Color.Red)}");
            }
            return output;
        }
    }
}
