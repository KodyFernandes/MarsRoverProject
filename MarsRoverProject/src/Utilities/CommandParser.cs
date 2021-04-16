using System;
using System.Collections.Generic;
using MarsRoverProject.Controls;

namespace MarsRoverProject.Utilities
{
    public static class CommandParser
    {
        private static readonly Dictionary<string, IControl> StringToCommandDict = new Dictionary<string, IControl> {
            {"L", new RotateLeftControl()},
            {"R", new RotateRightControl()},
            {"M", new MoveControl()}
        };

        public static IEnumerable<IControl> ParseCommands(string commandString)
        {
            return string.IsNullOrEmpty(commandString) ? new List<IControl>() : PopulateCommandsList(commandString);
        }

        private static List<IControl> PopulateCommandsList(string commandString) {
            var commands = new List<IControl>();
            foreach (var commandCharacter in commandString.ToCharArray())
            {
                try
                {
                    var mappedCommand = GetCommand(commandCharacter.ToString().ToUpper());
                    commands.Add(mappedCommand);
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine("Command line \"" + commandString + "\" contains invalid characters.");
                }
            }

            return commands;
        }

        private static IControl GetCommand(string commandCharacter)
        {
            if (StringToCommandDict.TryGetValue(commandCharacter, out var command)) {
                return command;
            }

            throw new InvalidOperationException();
        }
    }
}