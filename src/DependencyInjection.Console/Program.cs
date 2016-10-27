using DependencyInjection.Console.CharacterWriters;
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
            var pattern = "circle"; // TODO: Hook this up

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
            var patternGenerator = new PatternGenerator();
            var app = new PatternApp(patternWriter, patternGenerator);
            app.Run(width, height);
        }
    }
}
