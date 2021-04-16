using MarsRoverProject.Vehicle;

namespace MarsRoverProject.Controls
{
    public class MoveControl : IControl
    {
        public void Execute(IVehicle rover) {
            rover.Move();
        }
    }
}