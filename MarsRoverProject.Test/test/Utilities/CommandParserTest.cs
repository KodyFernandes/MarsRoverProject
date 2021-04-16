using System.Collections.Generic;
using System.Linq;
using MarsRoverProject.Controls;
using MarsRoverProject.Utilities;
using Xunit;

namespace MarsRoverProject.Test.test.Utilities
{
    public class CommandParserTest
    {
        [Fact]
        public void ParseCommands()
        {
            var commandList = new List<IControl>
            {
                new MoveControl(),
                new MoveControl(),
                new RotateRightControl(),
                new MoveControl(),
                new MoveControl(),
                new RotateLeftControl(),
                new MoveControl(),
                new RotateLeftControl()
            };

            const string commandString = "MMRMMLML";

            var commandOutput = CommandParser.ParseCommands(commandString).ToList();

            Assert.Equal(8, commandOutput.Count);
            var i = 0;
            commandOutput.ForEach(command =>
            {
                Assert.True(command.GetType() == commandList[i].GetType());
                i++;
            });
        }
    }
}