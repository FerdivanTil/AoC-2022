using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.WriteResult(Test1, FileType.Test1Sample);
            Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static int Test1(List<string> input)
        {
            var result = Grouping(input);
            return result.Max();
        }

        private static int Test2(List<string> input)
        {
            var result = Grouping(input);
            return result.OrderByDescending(i => i).Take(3).Sum();
        }

        public static IEnumerable<int> Grouping(IEnumerable<string> source)
        {
            return source.Aggregate(new List<int> { default },
                                    (list, value) =>
                                    {
                                        if (value == string.Empty)
                                        {
                                            list.Add(0);
                                            return list;
                                        }
                                        list[list.Count()-1] = list.Last() + int.Parse(value);
                                        return list;
                                    });
        }
    }
}
