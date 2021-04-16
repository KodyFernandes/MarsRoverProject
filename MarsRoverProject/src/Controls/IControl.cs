using MarsRoverProject.Vehicle;

namespace MarsRoverProject.Controls
{
    public interface IControl
    {
        public void Execute(IVehicle rover);
    }
}