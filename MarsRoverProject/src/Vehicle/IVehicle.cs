using System.Collections.Generic;
using MarsRoverProject.Controls;
using MarsRoverProject.Environment;

namespace MarsRoverProject.Vehicle
{
    public interface IVehicle
    {
        ICoordinates CurrentCoordinates { get; }
        
        void RunCommands(IEnumerable<IControl> roverCommands);

        string CurrentLocationAndOrientation();

        void RotateRight();

        void RotateLeft();

        void Move();
    }
}