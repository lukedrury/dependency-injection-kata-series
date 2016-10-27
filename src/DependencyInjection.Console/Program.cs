﻿using System;
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
            var chosenPainter = CreatePainter(pattern);
            var patternGenerator = new PatternGenerator(chosenPainter);
            var app = new PatternApp(patternWriter, patternGenerator);
            app.Run(width, height);
        }

        private static ISquarePainter CreatePainter(string pattern)
        {
            ISquarePainter chosenPainter;
            switch (pattern)
            {
                case "circle":
                    chosenPainter = new CircleSquarePainter();
                    break;
                case "oddeven":
                    chosenPainter = new OddEvenSquarePainter();
                    break;
                case "whitesquare":
                    chosenPainter = new WhiteSquarePainter();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("pattern");
            }
            return chosenPainter;
        }
    }
}
