using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"C:\Users\admin\source\repos\Neq.Design\Neq.Design.WPF\Themes\DarkTheme.xaml");

            var lines = text.Replace("\r", string.Empty).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();

            foreach (var line in lines)
            {
                var key = Regex.Match(line, @"x:Key=""(.*?)""").Groups[1].Value;
                //var value = Regex.Match(line, @"Color=""(.*?)""").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                string privateName = $"_{char.ToLower(key[0])}{key.Substring(1)}";

                sb.Append($"private Brush {privateName};" +
                    $"\n\n[ThemeResource]" +
                    $"\npublic Brush {key}" +
                    $"\n{{" +
                    $"\n\tget => {privateName};" +
                    $"\n\tset {{ {privateName} = value; Notify(); }}" +
                    $"\n}}\n\n");
            }

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}
