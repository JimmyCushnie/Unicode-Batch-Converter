using System;
using System.Collections.Generic;
using System.Windows;

namespace UnicodeConverter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Unicode Batch Converter";
            WriteLineColor("Welcome to the converter.");

            start:

            WriteLineColor("Please enter a list of unicode hex codes, one on each line, in the format U+4E00 or just 4E00.");
            WriteLineColor("Then, enter 'done' to start the conversion.");

            var codes = new List<string>();

            string line = Console.ReadLine();
            while (line != "done")
            {
                codes.Add(line);
                //WriteLineColor("added " + codes[codes.Count - 1], ConsoleColor.Magenta);
                line = Console.ReadLine();
            }

            WriteLineColor("Converting...");

            string final = "";
            foreach(var code in codes)
            {
                try
                {
                    int intcode = int.Parse(code.Replace("U+", "").Replace("u+", ""), System.Globalization.NumberStyles.HexNumber);
                    final += char.ConvertFromUtf32(intcode);
                }
                catch
                {
                    WriteLineColor($"error: could not convert code '{code}' to unicode character", ConsoleColor.Red);
                }
            }

            Clipboard.SetText(final);
            WriteLineColor($"Done. All the valid unicode characters you entered have been copied to your clipboard ({final.Length} characters).", ConsoleColor.Green);

            Console.WriteLine();
            goto start;
        }

        static void WriteLineColor(string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
