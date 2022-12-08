using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;
using System.Diagnostics;

namespace Day7
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
            var commandProcessor = new CommandProcessor();
            foreach(var item in input)
            {
                commandProcessor.ProcessCommand(item);
            }
            var result = new List<(string path, int size)>();
            //Calculate the size
            foreach (var dir in commandProcessor.Directories)
            {
                result.Add((dir.Path, commandProcessor.Directories.Where(i => i.Path.StartsWith(dir.Path)).Sum(i => i.Size)));
            }
            return result.Where(i => i.size <= 100000).Sum(i => i.size);
        }

        private static int Test2(List<string> input)
        {
            var commandProcessor = new CommandProcessor();
            foreach (var item in input)
            {
                commandProcessor.ProcessCommand(item);
            }
            var result = new List<(string path, int size)>();
            //Calculate the size
            foreach (var dir in commandProcessor.Directories)
            {
                result.Add((dir.Path, commandProcessor.Directories.Where(i => i.Path.StartsWith(dir.Path)).Sum(i => i.Size)));
            }
            var total = 70000000;
            var used = result.First(i => i.path == "/").size;
            var required = 30000000;
            var needed = required - (total - used);
            return result.Where(i => i.size > needed).OrderBy(i => i.size).First().size;
        }
    }

    public class CommandProcessor
    {
        public Stack<string> Wd { get; set; } = new();
        public List<Directory> Directories { get; set; } = new() { new("/")};
        public bool InListingMode { get; set; }
        public void ProcessCommand(string command)
        {
            switch(command)
            {
                case string cd when cd.StartsWith("$ cd"):
                    InListingMode = false;
                    Console.WriteLine($"Changing directory to {command.Substring(5).Pastel(Color.Green)}");
                    ChangeDirectory(command.Substring(5));
                    break;
                case string ls when ls == "$ ls":
                    Console.WriteLine($"Listing files");
                    InListingMode = true;
                    break;
                case string dir when dir.StartsWith("dir "):
                    Console.WriteLine($"Adding directory to {command.Substring(4).Pastel(Color.Green)}");
                    AddFolder(dir.Substring(4));
                    break;
                default:
                    var file = command.Split(" ");
                    Console.WriteLine($"Adding File {file[1].Pastel(Color.Green)} with size {file[0].Pastel(Color.Green)}");

                    AddFile(file[1], int.Parse(file[0]));
                    break;
            }
        }

        public void AddFile(string fileName, int size)
        {
            var dir = Directories.Single(i => i.Path == $"/{string.Join("/", Wd.ToArray().Reverse())}");
            dir.Files.Add(new(fileName,size));
        }

        public void AddFolder(string folder)
        {
            if (!FolderExists(folder))
                Directories.Add(new(GetFullPath(folder)));
        }

        public bool FolderExists(string folder)
        {
            return Directories.Any(i => i.Path == GetFullPath(folder));
        }

        public string GetFullPath(string folder)
        {
            return $"/{string.Join("/", Wd.ToArray().Reverse().Append(folder).ToArray())}";
        }

        public void ChangeDirectory(string path)
        {
            switch(path)
            {
                case "/":
                    Wd.Clear();
                    //AddFolder(path);
                    break;
                case "..":
                    Wd.Pop();
                    break;
                default:
                    Wd.Push(path);
                    break;
            }
        }
    }

    [DebuggerDisplay("Directory {Path}, Files Size {Size}")]
    public class Directory
    {
        public string Path { get; set; }
        public List<File> Files { get; set; } = new();
        public int Size => Files.Sum(i => i.Size);
        public Directory(string path)
        {
            Path = path;
        }
    }

    [DebuggerDisplay("File {FileName}, Size {Size}")]

    public class File
    {
        public string FileName { get; set; }
        public int Size { get; set; }
        public File(string fileName, int size)
        {
            FileName = fileName;
            Size = size;
        }
    }
}