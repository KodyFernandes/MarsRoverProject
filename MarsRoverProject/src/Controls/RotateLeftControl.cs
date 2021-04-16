using MarsRoverProject.Vehicle;

namespace MarsRoverProject.Controls
{
    public class RotateLeftControl : IControl
    {
        public void Execute(IVehicle rover) {
            rover.RotateLeft();
        }
    }
}