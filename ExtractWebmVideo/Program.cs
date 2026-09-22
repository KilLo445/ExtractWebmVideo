using System;
using System.Diagnostics;
using System.IO;

namespace ExtractWebmVideo
{
    class Program
    {
        public static string version = "1.0";
        public static string github = "https://github.com/KilLo445/ExtractWebmVideo";

        public static string root = Directory.GetCurrentDirectory();
        public static string temp = Path.Combine(Path.GetTempPath(), "JustDanceAutoDanceExtractor");

        public static string ffmpegPath;

        public static bool isDirectory = false;
        public static bool ConvertToMP4 = false;
        public static bool DeleteOriginal = false;
        public static bool ffmpegInstalled = false;

        static void Main(string[] args)
        {
            Console.Title = "Just Dance AutoDance Extractor";

            if (args.Length == 0)
            {
                Console.WriteLine("Welcome to Just Dance AutoDance Extractor!");
                Console.WriteLine();
                Console.WriteLine("This is a fork of csabor/ExtractWebmVideo with some added features by KilLo");
                Console.WriteLine();
                Console.WriteLine("To get started, drag the raw AutoDance file onto this .exe or use some arguments for more advanced options!");
                Console.WriteLine();
                Console.WriteLine("Use -help to see available options!");
                PressAnyKeyToExit();
            }

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg == "-help") { Help(); Environment.Exit(0); }
                if (arg == "-mp4") { ConvertToMP4 = true; CheckForFFmpeg(); }
                if (arg == "-delete") { DeleteOriginal = true; }
                if (arg == "-github") { Console.WriteLine("Opening GitHub..."); Process.Start(github); Environment.Exit(0); }
                if (arg == "-version") { Console.WriteLine($"You have v{version}"); Environment.Exit(0); }
            }

            try
            {
                if (DeleteOriginal == true)
                {
                    Console.WriteLine("Are you sure you want to delete the original AutoDance file(s)? [Y/N]");
                    string Key = Console.ReadLine();

                    if (Key == "Y" || Key == "y" || Key == "N" || Key == "n")
                    {
                        if (Key == "Y" || Key == "y") { DeleteOriginal = true; }
                        else if (Key == "N" || Key == "n") { DeleteOriginal = false; Console.WriteLine(Environment.NewLine + "Continuing without deleting original file(s)"); }
                        else { Console.WriteLine(Environment.NewLine + "Y/N not entered. Exiting."); Environment.Exit(0); }
                    }
                }

                if (Directory.Exists(temp)) { Directory.Delete(temp, true); }
                Directory.CreateDirectory(temp);
                FileAttributes attr = File.GetAttributes(args[0]);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    isDirectory = true;
                    var files = Directory.GetFiles(args[0]);
                    foreach (var file in files) { ImportFile(file); }
                }
                else { ImportFile(args[0]); }

                if (ConvertToMP4 == true) { ConvertMP4(args[0]); }

                Console.WriteLine(Environment.NewLine + "Done!");
            }
            catch (Exception ex) { Console.WriteLine("An error occured." + Environment.NewLine + ex.Message); PressAnyKeyToExit(); }

            PressAnyKeyToExit();
        }

        private static void PressAnyKeyToExit()
        {
            Console.WriteLine(Environment.NewLine + "Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void Help()
        {
            Console.WriteLine("Usage: ExtractWebmVideo.exe <File or Directory> <Extra arguments (-mp4, etc)>");
            Console.WriteLine();
            Console.WriteLine("Command line arguments:");
            Console.WriteLine("-help - Opens this*");
            Console.WriteLine("-mp4 - Convert WebM to MP4 (FFmpeg required)");
            Console.WriteLine("-delete - Delete original AutoDance file");
            Console.WriteLine("-github - Opens GitHub page*");
            Console.WriteLine("-version - Shows installed version*");
            Console.WriteLine();
            Console.WriteLine("* = No file or directory required");
        }

        private static void ImportFile(string file)
        {
            try
            {
                byte[] inputBytes = File.ReadAllBytes(file);
                var start = FindStart(inputBytes);
                if (start == -1) { Console.WriteLine($"No start found for '{file}', no video could be extracted."); }
                else
                {
                    var fileNameOut = Path.Combine(temp, Path.GetFileNameWithoutExtension(file) + "-converted.webm");
                    var target = File.OpenWrite(Path.Combine(temp, Path.GetFileName(fileNameOut)));
                    target.Write(inputBytes, start, inputBytes.Length - start);
                    target.Close();
                    Console.WriteLine($"Extracted {file} -> {fileNameOut}");
                    if (DeleteOriginal == true)
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted {file}");
                    }

                    if (ConvertToMP4 != true) { Console.WriteLine($"Moving {Path.GetFileName(fileNameOut)}"); File.Move(fileNameOut, Path.Combine(Path.GetDirectoryName(file), Path.GetFileName(fileNameOut))); }
                }
            }
            catch (Exception ex) { Console.WriteLine("An error occured." + Environment.NewLine + ex.Message); PressAnyKeyToExit(); }
        }

        private static void ConvertMP4(string args)
        {
            Console.WriteLine("Converting to MP4 with FFmpeg");
            try
            {
                string outputDirectory;
                if (isDirectory == true) { outputDirectory = args; } else { outputDirectory = Path.GetDirectoryName(args); }

                    string[] ffmpegBat ={
                            "@echo off",
                            "title Converting to MP4 with FFmpeg",
                            $"cd \"{temp}\"",
                            $"for %%f in (*.webm) do (  \"{ffmpegPath}\" -i \"%%f\" -c:v libx264 -c:a aac \"%%f.mp4\" )",
                            "del /f /q *.webm",
                            $"move /y \"*.mp4\" \"{outputDirectory}\"",
                            "exit"
                          };
                File.WriteAllLines(Path.Combine(temp, "convert.bat"), ffmpegBat);
                var process = Process.Start(Path.Combine(temp, "convert.bat"));
                process.WaitForExit();
                return;
            }
            catch (Exception ex) { Console.WriteLine("An error occured." + Environment.NewLine + ex.Message); PressAnyKeyToExit(); }
        }

        private static void CheckForFFmpeg()
        {
            string pathEnv = Environment.GetEnvironmentVariable("PATH");
            ffmpegInstalled = false;

            if (pathEnv != null)
            {
                foreach (string pathDir in pathEnv.Split(Path.PathSeparator))
                {
                    ffmpegPath = Path.Combine(pathDir, "ffmpeg.exe");
                    if (File.Exists(ffmpegPath))
                    {
                        ffmpegInstalled = true;
                        break;
                    }
                    else
                    {
                        ffmpegPath = Path.Combine(root, "ffmpeg.exe");
                        if (File.Exists(ffmpegPath))
                        {
                            ffmpegInstalled = true;
                            break;
                        }
                    }
                }
            }

            if (ffmpegInstalled == false && ConvertToMP4 == true) { Console.WriteLine("FFmpeg is required to convert to MP4."); PressAnyKeyToExit(); }
        }

        private static int FindStart(byte[] inputBytes)
        {
            for (var i = 0; i < inputBytes.Length - 3; i++)
            {
                if (inputBytes[i] == 0x1A
                    && inputBytes[i + 1] == 0x45
                    && inputBytes[i + 2] == 0xDF
                    && inputBytes[i + 3] == 0xA3)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
