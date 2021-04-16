using MarsRoverProject.Vehicle;

namespace MarsRoverProject.Controls
{
    public class RotateRightControl :IControl
    {
        public void Execute(IVehicle rover) {
            rover.RotateRight();
        }
    }
}