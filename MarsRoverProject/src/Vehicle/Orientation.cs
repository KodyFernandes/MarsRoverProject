using MarsRoverProject.Enums;

namespace MarsRoverProject.Vehicle
{
    public class Orientation : IOrientation
    {
        public CompassHeading Heading { get; private set; }

        public Orientation(CompassHeading heading)
        {
            Heading = heading;
        }

        public void Right()
        {
            Heading = Heading switch
            {
                CompassHeading.N => CompassHeading.E,
                CompassHeading.S => CompassHeading.W,
                CompassHeading.E => CompassHeading.S,
                CompassHeading.W => CompassHeading.N,
                _ => Heading
            };
        }

        public void Left()
        {
            Heading = Heading switch
            {
                CompassHeading.N => CompassHeading.W,
                CompassHeading.S => CompassHeading.E,
                CompassHeading.E => CompassHeading.N,
                CompassHeading.W => CompassHeading.S,
                _ => Heading
            };
        }

        public override string ToString()
        {
            return Heading.ToString();
        }
    }
}