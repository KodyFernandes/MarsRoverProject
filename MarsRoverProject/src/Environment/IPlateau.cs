namespace MarsRoverProject.Environment
{
    public interface IPlateau
    {
        ICoordinates TopRightCoordinates { get; }
        
        bool IsWithinBounds(ICoordinates coordinates);

        bool IsOccupiedCoordinate(ICoordinates coordinates);

        void AddOccupiedCoordinates(ICoordinates coordinates);
    }
}