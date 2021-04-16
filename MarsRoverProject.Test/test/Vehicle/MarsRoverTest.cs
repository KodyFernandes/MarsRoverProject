using System;
using System.Collections.Generic;
using MarsRoverProject.Controls;
using MarsRoverProject.Enums;
using MarsRoverProject.Environment;
using MarsRoverProject.Vehicle;
using Xunit;

namespace MarsRoverProject.Test.test.Vehicle
{
    public class MarsRoverTest
    {
        
        private readonly IOrientation _orientation;
        private readonly IPlateau _plateau;
        private readonly ICoordinates _coordinates;
        private IVehicle _rover;

        public MarsRoverTest()
        {
            _plateau = new Plateau(10, 10);
            _coordinates = new Coordinates(5, 5);
            _orientation = new Orientation(CompassHeading.N);
            
        }
        
        [Fact]
        public void RunCommands_ManipulatesRover()
        {
            var commandList = new List<IControl>
            {
                new MoveControl(),
                new MoveControl(),
                new RotateRightControl(),
                new MoveControl(),
                new MoveControl(),
                new RotateLeftControl(),
                new MoveControl(),
                new RotateLeftControl()
            };

            _rover = new MarsRover(1, _plateau, _orientation, _coordinates);
            
            _rover.RunCommands(commandList);
            
            var roverOutput = _rover.CurrentLocationAndOrientation();
                
            Assert.Equal("7 8 W", roverOutput);
            Assert.Equal(7, _rover.CurrentCoordinates.XCoordinate);
            Assert.Equal(8, _rover.CurrentCoordinates.YCoordinate);
        }
        
        [Fact]
        public void CurrentLocationAndOrientation_ReturnsRoverCoordinatesAndOrientation()
        {
            _rover = new MarsRover(1, _plateau, _orientation, _coordinates);

            var roverOutput = _rover.CurrentLocationAndOrientation();
                
            Assert.Equal("5 5 N", roverOutput);
        }
        
        [Fact]
        public void RotateRight_RotatesRoverRight()
        {
            _rover = new MarsRover(1, _plateau, _orientation, _coordinates);

            _rover.RotateRight();
            var roverOutput = _rover.CurrentLocationAndOrientation();
                
            Assert.Equal("5 5 E", roverOutput);
        }
        
        [Fact]
        public void RotateLeft_RotatesRoverLeft()
        {
            _rover = new MarsRover(1, _plateau, _orientation, _coordinates);

            _rover.RotateLeft();
            var roverOutput = _rover.CurrentLocationAndOrientation();
                
            Assert.Equal("5 5 W", roverOutput);
        }
        
        [Theory]
        [InlineData(CompassHeading.N, 5, 6)]
        [InlineData(CompassHeading.E, 6, 5)]
        [InlineData(CompassHeading.S, 5, 4)]
        [InlineData(CompassHeading.W, 4, 5)]
        public void Move_MovesRoverCorrectlyForEachOrientation(CompassHeading roverHeading, int xCoordinateAfterMove, int yCoordinateAfterMove)
        {
            var orientation = new Orientation(roverHeading);
            _rover = new MarsRover(1, _plateau, orientation, _coordinates);

            _rover.Move();
            var roverOutput = _rover.CurrentLocationAndOrientation();
                
            Assert.Equal(xCoordinateAfterMove, _rover.CurrentCoordinates.XCoordinate);
            Assert.Equal(yCoordinateAfterMove, _rover.CurrentCoordinates.YCoordinate);
            Assert.Equal($"{xCoordinateAfterMove} {yCoordinateAfterMove} {roverHeading}", roverOutput);
        }

        [Fact]
        public void Move_ThrowsOutOfBoundsException()
        {
            var orientation = new Orientation(CompassHeading.W);
            var coordinates = new Coordinates(0, 0);
            _rover = new MarsRover(1, _plateau, orientation, coordinates);

            var ex = Assert.Throws<Exception>(() => _rover.Move());
            
            var exceptionString =
                $"Command would cause rover to leave plateau. Coordinates can not be beyond boundaries (0,0) and ({_plateau.TopRightCoordinates.XCoordinate},{_plateau.TopRightCoordinates.YCoordinate}).";
            
            Assert.Equal(exceptionString, ex.Message);
            
            // Verify rover remains in same location on failed move
            Assert.Equal(0, _rover.CurrentCoordinates.XCoordinate);
            Assert.Equal(0, _rover.CurrentCoordinates.YCoordinate);
        }
        
        [Fact]
        public void Move_ThrowsCollisionException()
        {
            ICoordinates occupiedCoordinates = new Coordinates(5, 6);
            _plateau.AddOccupiedCoordinates(occupiedCoordinates);
            _rover = new MarsRover(1, _plateau, _orientation, _coordinates);

            var ex = Assert.Throws<Exception>(() => _rover.Move());

            const string exceptionString = "Command would cause rover 1 to collide with another rover.";
            
            Assert.Equal(exceptionString, ex.Message);
            
            // Verify rover remains in same location on failed move
            Assert.Equal(5, _rover.CurrentCoordinates.XCoordinate);
            Assert.Equal(5, _rover.CurrentCoordinates.YCoordinate);
        }
    }
}