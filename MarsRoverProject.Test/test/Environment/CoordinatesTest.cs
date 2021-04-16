using MarsRoverProject.Environment;
using Xunit;

namespace MarsRoverProject.Test.test.Environment
{
    public class CoordinatesTest
    {
        [Fact]
        public void CoordinatesAfterMove_CalculatesCoordinatesGivenMoveVector()
        {
            const int xAxisMovementVector = -1;
            const int yAxisMovementVector = 0;
            ICoordinates coordinates = new Coordinates(1, 2);

            var newCoordinates = coordinates.CoordinatesAfterMove(xAxisMovementVector, yAxisMovementVector);

            Assert.Equal(0, newCoordinates.XCoordinate);
            Assert.Equal(2, newCoordinates.YCoordinate);
        }
        
        [Fact]
        public void ToString_ReturnsParsedCoordinateString()
        {
            ICoordinates coordinates = new Coordinates(1, 2);

            Assert.Equal("1 2", coordinates.ToString());
        }
    }
}