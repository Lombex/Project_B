using System;
using System.Text.RegularExpressions;

namespace Project.Presentation
{
    public static class SetConsoleColor
    {
        private static void WriteLine(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = Console.ForegroundColor;
                if (color == oldColor) Console.Out.Write(text + '\n');
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.Out.Write(text + '\n');
                    Console.ForegroundColor = oldColor;
                }
            }
            else Console.Out.Write(text + '\n');
        }
        public static void WriteLine(string text, string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                WriteLine(text);
                return;
            }
            if (!Enum.TryParse(color, true, out ConsoleColor col)) WriteLine(text);
            else WriteLine(text, col);
        }
        private static void Write(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = Console.ForegroundColor;
                if (color == oldColor) Console.Out.Write(text);
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.Out.Write(text);
                    Console.ForegroundColor = oldColor;
                }
            }
            else Console.Out.Write(text + '\n');
        }
        private static void Write(string text, string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                Write(text);
                return;
            }

            if (!Enum.TryParse(color, true, out ConsoleColor col)) Write(text);
            else Write(text, col);
        }

        #region Wrappers and Templates
        public static void WriteWrappedHeader(string headerText, char wrapperChar = '-', ConsoleColor headerColor = ConsoleColor.Yellow, ConsoleColor dashColor = ConsoleColor.DarkGray)
        {
            if (string.IsNullOrEmpty(headerText)) return;
            string line = new string(wrapperChar, headerText.Length);
            WriteLine(line, dashColor);
            WriteLine(headerText, headerColor);
            WriteLine(line, dashColor);
        }

        private static readonly Lazy<Regex> ColorBlockRegEx = new Lazy<Regex>(() => new Regex("\\[(?<color>.*?)\\](?<text>[^[]*)\\[/\\k<color>\\]", RegexOptions.IgnoreCase), isThreadSafe: true);

        /// <summary>
        /// This method uses a way that looks simular as html but insted using <> use []
        /// </summary>
        /// <param name="text">use [red]Test[/red] to make "Test" appear red for example</param>
        /// <param name="baseTextColor">The entire color of the line [White on default]</param>
        public static void WriteEmbeddedColorLine(string text, ConsoleColor? baseTextColor = ConsoleColor.White)
        {
            if (baseTextColor == null) baseTextColor = Console.ForegroundColor;

            if (string.IsNullOrEmpty(text))
            {
                WriteLine(string.Empty);
                return;
            }
            int at = text.IndexOf('[');
            int at2 = text.IndexOf(']');
            if (at == -1 || at2 <= at)
            {
                WriteLine(text, baseTextColor);
                return;
            }
            while (true)
            {
                var match = ColorBlockRegEx.Value.Match(text);
                if (match.Length < 1)
                {
                    Write(text, baseTextColor);
                    break;
                }
                Write(text.Substring(0, match.Index), baseTextColor);
                string highlightText = match.Groups["text"].Value;
                string colorVal = match.Groups["color"].Value;
                Write(highlightText, colorVal);
                text = text.Substring(match.Index + match.Value.Length);
            }
            Console.Out.Write('\n');
        }
        #endregion

        #region Success, Error, Info, Warning Wrappers
        public static void WriteSuccess(string text) => WriteLine(text, ConsoleColor.Green);
        public static void WriteError(string text) => WriteLine(text, ConsoleColor.Red);
        public static void WriteWarning(string text) => WriteLine(text, ConsoleColor.DarkYellow);
        public static void WriteInfo(string text) => WriteLine(text, ConsoleColor.DarkCyan);
        #endregion
    }
}
