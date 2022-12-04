using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day4
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
            foreach(var item in input)
            {
                var range = Parse(item);
                var isMatch = range.t1.Intersect(range.t2).Count() == range.t1.Count() || range.t2.Intersect(range.t1).Count() == range.t2.Count();
                if (isMatch)
                    output++;
                Console.WriteLine($"Parsing line gets {"MATCH".Pastel(isMatch? Color.Green:Color.Red)} {string.Join(',', range.t1.ToArray())} AND {string.Join(',', range.t2.ToArray())}");
            }

            return output;
        }

        private static int Test2(List<string> input)
        {
            var output = 0;
            foreach (var item in input)
            {
                var range = Parse(item);
                var isMatch = range.t1.Intersect(range.t2).Any() || range.t2.Intersect(range.t1).Any();
                if (isMatch)
                    output++;
                Console.WriteLine($"Parsing line gets {"MATCH".Pastel(isMatch ? Color.Green : Color.Red)} {string.Join(',', range.t1.ToArray())} AND {string.Join(',', range.t2.ToArray())}");
            }
            return output;
        }
        private static (IEnumerable<int> t1, IEnumerable<int> t2) Parse(string input)
        {
            var teams = input.Split(',').Select(i => i.Split("-").Select(int.Parse).ToList()).ToList();
            return (Enumerable.Range(teams[0][0], teams[0][1] - teams[0][0] + 1), Enumerable.Range(teams[1][0], teams[1][1] - teams[1][0] + 1));
        }
    }
}
