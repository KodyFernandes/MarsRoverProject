using MarsRoverProject.Environment;
using Xunit;

namespace MarsRoverProject.Test.test.Environment
{
    public class PlateauTest
    {
        private const int UpperXBounds = 10;
        private const int UpperYBounds = 10;

        private readonly IPlateau _plateau;

        public PlateauTest()
        {
            _plateau = new Plateau(UpperXBounds, UpperYBounds);
        }
        
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(2, 3, true)]
        [InlineData(-1, 3, false)]
        [InlineData(12, 2, false)]
        public void IsWithinBounds_DeterminesIfCoordinatesAreWithinPlateauBounds(int xCoordinate, int yCoordinate, bool expectedResult)
        {
            ICoordinates coordinates = new Coordinates(xCoordinate, yCoordinate);

            var actualResult = _plateau.IsWithinBounds(coordinates);

            Assert.Equal(expectedResult, actualResult);
        }
        
        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(2, 3, false)]
        [InlineData(7, 3, true)]
        [InlineData(2, 9, true)]
        public void IsOccupiedCoordinate_DeterminesIfCoordinatesAreOccupied(int xCoordinate, int yCoordinate, bool expectedResult)
        {
            ICoordinates coordinates = new Coordinates(xCoordinate, yCoordinate);
            _plateau.AddOccupiedCoordinates(new Coordinates(7, 3));
            _plateau.AddOccupiedCoordinates(new Coordinates(2, 9));
            
            var actualResult = _plateau.IsOccupiedCoordinate(coordinates);

            Assert.Equal(expectedResult, actualResult);
        }
        
        [Fact]
        public void AddOccupiedCoordinates_AddsCoordinatesToOccupiedCoordinatesList()
        {
            ICoordinates occupiedCoordinates1 = new Coordinates(7, 3);
            ICoordinates occupiedCoordinates2 = new Coordinates(2, 9);
            
            _plateau.AddOccupiedCoordinates(occupiedCoordinates1);
            _plateau.AddOccupiedCoordinates(occupiedCoordinates2);

            Assert.True(_plateau.IsOccupiedCoordinate(occupiedCoordinates1));
            Assert.True(_plateau.IsOccupiedCoordinate(occupiedCoordinates2));
        }
    }
}