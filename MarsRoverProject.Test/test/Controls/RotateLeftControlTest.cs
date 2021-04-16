using MarsRoverProject.Controls;
using MarsRoverProject.Enums;
using MarsRoverProject.Environment;
using MarsRoverProject.Vehicle;
using Xunit;

namespace MarsRoverProject.Test.test.Controls
{
    public class RotateLeftControlTest
    {
        [Fact]
        public void Execute_RotatesVehicleLeft()
        {
            IPlateau plateau = new Plateau(10, 10);
            ICoordinates coordinates = new Coordinates(1, 2);
            IOrientation orientation = new Orientation(CompassHeading.N);
            IVehicle rover = new MarsRover(1, plateau, orientation, coordinates);
            IControl control = new RotateLeftControl();

            control.Execute(rover);
            
            Assert.Equal("1 2 W", rover.CurrentLocationAndOrientation());
        }
    }
}