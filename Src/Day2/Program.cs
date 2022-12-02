using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day2
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
            var games = input.Select(Rps.Parse1);

            return games.Sum(i => i.Points);
        }

        private static int Test2(List<string> input)
        {
            var games = input.Select(Rps.Parse2);

            return games.Sum(i => i.Points);
        }
    }

    public class Rps
    {
        public Hand MyHand { get; set; }
        public Hand HisHand { get; set; }
        public static Rps Parse1(string input)
        {
            var game = new Rps();
            var gameInput = input.Split(' ');
            game.HisHand = ParseHand(gameInput[0]);
            game.MyHand = ParseHand(gameInput[1]);
            return game;
        }

        public static Rps Parse2(string input)
        {
            var game = new Rps();
            var gameInput = input.Split(' ');
            game.HisHand = ParseHand(gameInput[0]);
            switch(gameInput[1])
            {
                case "X":
                    game.Lose();
                    break;
                case "Y":
                    game.Draw();
                    break;
                case "Z":
                    game.Won();
                    break;
            }
            return game;
        }
        public void Lose()
        {
            switch(HisHand)
            {
                case Hand.Rock:
                    MyHand = Hand.Scissor;
                    break;
                case Hand.Paper:
                    MyHand = Hand.Rock;
                    break;
                case Hand.Scissor:
                    MyHand = Hand.Paper;
                    break;
            }
        }
        public void Won()
        {
            switch (HisHand)
            {
                case Hand.Rock:
                    MyHand = Hand.Paper;
                    break;
                case Hand.Paper:
                    MyHand = Hand.Scissor;
                    break;
                case Hand.Scissor:
                    MyHand = Hand.Rock;
                    break;
            }
        }

        public void Draw()
        {
            MyHand = HisHand;
        }

        public bool IsWon
        {
            get
            {
                return MyHand == Hand.Paper && HisHand == Hand.Rock ||
                       MyHand == Hand.Rock && HisHand == Hand.Scissor ||
                       MyHand == Hand.Scissor && HisHand == Hand.Paper;
            }
        }
        public bool IsDraw
        {
            get
            {
                return MyHand == HisHand;
            }
        }
        public int Points
        {
            get
            {
                var points = (IsWon ? 6 : 0) + (IsDraw ? 3 : 0) + (int)MyHand;
                Console.WriteLine($"{MyHand.ToString().Pastel(Color.Red)} vs {HisHand.ToString().Pastel(Color.Red)} results in {IsWon.ToString().Pastel(Color.Green)} + {((int)MyHand).ToString().Pastel(Color.Green)} = {(points).ToString().Pastel(Color.Green)}");
                return points;
            }
        }

        public enum Hand
        {
            None,
            Rock = 1,
            Paper = 2,
            Scissor = 3
        }
        public static Hand ParseHand(string input)
        {
            switch(input)
            {
                case "A":
                case "X":
                    return Hand.Rock;
                case "B":
                case "Y":
                    return Hand.Paper;
                case "C":
                case "Z":
                    return Hand.Scissor;
            }
            return Hand.None;
        }
    }
}
