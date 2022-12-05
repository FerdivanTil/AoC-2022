using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;
using System.Text.RegularExpressions;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helper.WriteResult(Test1Sample, FileType.Test1Sample);
            //Helper.WriteResult(Test1, FileType.Test1);
            //Helper.WriteResult(Test2Sample, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static string Test1Sample(List<string> input)
        {
            Cargo cargo = new(GetInput1Sample());
            Console.WriteLine(cargo.ToString());
            Console.WriteLine("--------------------------------------------------");
            var actions = input.Select(Parse).ToList();
            foreach(var action in actions)
            {
                cargo.MoveByOne(action.move, action.from, action.to);
                Console.WriteLine(cargo.ToString());
                Console.WriteLine("--------------------------------------------------");

            }
            return cargo.GetTop();
        }

        private static string Test1(List<string> input)
        {
            Cargo cargo = new(GetInput1());
            Console.WriteLine(cargo.ToString());
            Console.WriteLine("--------------------------------------------------");
            foreach (var action in input.Select(Parse))
            {
                cargo.MoveByOne(action.move, action.from, action.to);
                Console.WriteLine(cargo.ToString());
                Console.WriteLine("--------------------------------------------------");

            }
            return cargo.GetTop();
        }

        private static string Test2Sample(List<string> input)
        {
            Cargo cargo = new(GetInput1Sample());
            Console.WriteLine(cargo.ToString());
            Console.WriteLine("--------------------------------------------------");
            var actions = input.Select(Parse).ToList();
            foreach (var action in actions)
            {
                cargo.MoveByStack(action.move, action.from, action.to);
                Console.WriteLine(cargo.ToString());
                Console.WriteLine("--------------------------------------------------");

            }
            return cargo.GetTop();
        }

        private static string Test2(List<string> input)
        {
            Cargo cargo = new(GetInput1());
            Console.WriteLine(cargo.ToString());
            Console.WriteLine("--------------------------------------------------");
            var actions = input.Select(Parse).ToList();
            foreach (var action in actions)
            {
                cargo.MoveByStack(action.move, action.from, action.to);
                Console.WriteLine(cargo.ToString());
                Console.WriteLine("--------------------------------------------------");

            }
            return cargo.GetTop();
        }

        public static Regex LineRegex = new Regex("^move (?<move>\\d+) from (?<from>\\d+) to (?<to>\\d+)$", RegexOptions.Compiled);
        private static (int move, int from, int to) Parse(string input)
        {
            var matches = LineRegex.Matches(input);
            return (int.Parse(matches.First().Groups["move"].Value), int.Parse(matches.First().Groups["from"].Value), int.Parse(matches.First().Groups["to"].Value));
        }
        private static List<Stack<char>> GetInput1Sample()
        {
            return new()
            {
                new Stack<char>(new[] { 'Z', 'N' }),
                new Stack<char>(new[] { 'M', 'C', 'D' }),
                new Stack<char>(new[] { 'P' })
            };
        }

        private static List<Stack<char>> GetInput1()
        {
            return new()
            {
                new Stack<char>(new[] { 'Q','M','G','C','L' }),
                new Stack<char>(new[] { 'R','D','L','C','T','F','H','G' }),
                new Stack<char>(new[] { 'V','J','F','N','M','T','W','R' }),
                new Stack<char>(new[] { 'J','F','D','V','Q','P' }),
                new Stack<char>(new[] { 'N','F','M','S','L','B','T' }),
                new Stack<char>(new[] { 'R','N','V','H','C','D','P' }),
                new Stack<char>(new[] { 'H','C','T' }),
                new Stack<char>(new[] { 'G','S','J','V','Z','N','H','P' }),
                new Stack<char>(new[] { 'Z','F','H','G' }),
            };
        }
    }

    public class Cargo
    {
        protected List<Stack<char>> Deck { get; set; }
        public Cargo(List<Stack<char>> init)
        {
            Deck = init;
        }

        public string GetTop()
        {
            return new string(Deck.Select(i => i.Peek()).ToArray());
        }

        public override string ToString()
        {
            var max = Deck.Max(i => i.Count);
            var lines = new List<string>(); 
            foreach(var i in Enumerable.Range(0, max))
            {
                var line = Deck.Select(x => x.Count() > i ? $"[{x.Skip(x.Count() -1 -i).First()}]" : "   ");
                lines.Add(string.Join(" ", line));
            }
            lines.Reverse();
            return string.Join(Environment.NewLine, lines.ToArray());
        }
        public void MoveByStack(int amount, int from, int to)
        {
            var stack = new Stack<char>();
            foreach (var _ in Enumerable.Range(0, amount))
            {
                var box = Deck[from - 1].Pop();
                stack.Push(box);
                
            }
            foreach (var _ in Enumerable.Range(0, amount))
            {
                Deck[to - 1].Push(stack.Pop());
            }
        }
        public void Move(int from, int to)
        {
            var box = Deck[from -1].Pop();
            Deck[to -1].Push(box);
        }
        public void MoveByOne(int amount, int from, int to)
        {
            foreach (var _ in Enumerable.Range(0, amount))
            {
                Move(from, to);
            }
        }
    }
}
