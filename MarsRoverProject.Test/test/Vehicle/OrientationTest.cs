using MarsRoverProject.Enums;
using MarsRoverProject.Vehicle;
using Xunit;

namespace MarsRoverProject.Test.test.Vehicle
{
    public class OrientationTest
    {
        private readonly IOrientation _orientation;

        public OrientationTest()
        {
            _orientation = new Orientation(CompassHeading.N);
        }

        [Fact]
        public void Right_TurnsHeadingRight()
        {
            _orientation.Right();
            
            Assert.Equal(CompassHeading.E, _orientation.Heading);
        }
        
        [Fact]
        public void Left_TurnsHeadingLeft()
        {
            _orientation.Left();
            
            Assert.Equal(CompassHeading.W, _orientation.Heading);
        }
        
        [Fact]
        public void ToString_ReturnHeadingAsStringValue()
        {
            Assert.Equal("N", _orientation.ToString());
        }
    }
}