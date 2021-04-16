namespace MarsRoverProject.Environment
{
    public interface ICoordinates
    {
        int XCoordinate { get; }
        
        int YCoordinate { get; }
        
        Coordinates CoordinatesAfterMove(int xAxisMovementVector, int yAxisMovementVector);

        string ToString();
    }
}