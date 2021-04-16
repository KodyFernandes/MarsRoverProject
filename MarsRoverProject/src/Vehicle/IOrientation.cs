using MarsRoverProject.Enums;

namespace MarsRoverProject.Vehicle
{
    public interface IOrientation
    {
        CompassHeading Heading { get; }
        
        void Right();

        void Left();

        string ToString();
    }
}