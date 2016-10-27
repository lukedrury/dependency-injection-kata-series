using System;
using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;
using NDesk.Options;

namespace DependencyInjection.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var useColors = false;
            var width = 25;
            var height = 15;
            var pattern = "circle";

            var optionSet = new OptionSet
            {
                {"c|colors", value => useColors = value != null},
                {"w|width=", value => width = int.Parse(value)},
                {"h|height=", value => height = int.Parse(value)},
                {"p|pattern=", value => pattern = value}
            };
            optionSet.Parse(args);

            var asciiWriter = new AsciiWriter();
            var characterWriter = useColors ? (ICharacterWriter) new ColorWriter(asciiWriter) : asciiWriter;
            var patternWriter = new PatternWriter(characterWriter);
            var chosenPainter = CreatePainter(pattern);
            var patternGenerator = new PatternGenerator(chosenPainter);
            var app = new PatternApp(patternWriter, patternGenerator);
            app.Run(width, height);
        }

        private static ISquarePainter CreatePainter(string pattern)
        {
            switch (pattern)
            {
                case "circle":
                    return new CircleSquarePainter();
                case "oddeven":
                    return new OddEvenSquarePainter();
                case "whitesquare":
                    return new WhiteSquarePainter();
                default:
                    throw new ArgumentOutOfRangeException("pattern");
            }
        }
    }
}
