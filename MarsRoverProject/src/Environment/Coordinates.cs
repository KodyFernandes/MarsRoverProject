namespace MarsRoverProject.Environment
{
    public class Coordinates : ICoordinates
    {
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }

        public Coordinates(int xCoordinate, int yCoordinate) {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public Coordinates CoordinatesAfterMove(int xAxisMovementVector, int yAxisMovementVector)
        {
            return new Coordinates(XCoordinate + xAxisMovementVector, YCoordinate + yAxisMovementVector);
        }
        
        public override string ToString()
        {
            return new string(XCoordinate + " " + YCoordinate);
        }
    }
}