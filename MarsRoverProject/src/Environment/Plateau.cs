using System.Collections.Generic;
using System.Linq;

namespace MarsRoverProject.Environment
{
    public class Plateau : IPlateau
    {
        public ICoordinates TopRightCoordinates { get; }
        private readonly ICoordinates _bottomLeftCoordinates = new Coordinates(0, 0);
        private readonly ICollection<ICoordinates> _occupiedCoordinates = new List<ICoordinates>();
        
        public Plateau(int topRightXCoordinate, int topRightYCoordinate) {
            TopRightCoordinates = new Coordinates(topRightXCoordinate, topRightYCoordinate);
        }

        public bool IsWithinBounds(ICoordinates coordinates)
        {

            return IsXCoordinateWithinBounds(coordinates.XCoordinate) &&
                   IsYCoordinateWithinBounds(coordinates.YCoordinate);
        }

        public bool IsOccupiedCoordinate(ICoordinates coordinates)
        {
            return _occupiedCoordinates.Any(occupied => occupied.XCoordinate == coordinates.XCoordinate &&
                                                        occupied.YCoordinate == coordinates.YCoordinate);
        }

        public void AddOccupiedCoordinates(ICoordinates coordinates)
        {
            _occupiedCoordinates.Add(coordinates);
        }

        private bool IsXCoordinateWithinBounds(int xCoordinate) {
            return xCoordinate >= _bottomLeftCoordinates.XCoordinate && xCoordinate <= TopRightCoordinates.XCoordinate;
        }

        private bool IsYCoordinateWithinBounds(int yCoordinate) {
            return yCoordinate >= _bottomLeftCoordinates.YCoordinate && yCoordinate <= TopRightCoordinates.YCoordinate;
        }
    }
}