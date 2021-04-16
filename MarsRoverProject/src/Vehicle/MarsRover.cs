using System;
using System.Collections.Generic;
using MarsRoverProject.Controls;
using MarsRoverProject.Enums;
using MarsRoverProject.Environment;

namespace MarsRoverProject.Vehicle
{
    public class MarsRover : IVehicle
    {
        public ICoordinates CurrentCoordinates { get; private set; }
        
        private readonly int _designation;
        private readonly IOrientation _currentOrientation;
        private readonly IPlateau _plateau;
        private int XAxisMovementVector { get; set; }
        private int YAxisMovementVector { get; set; }


        public MarsRover(int designation, IPlateau plateau, IOrientation orientation, ICoordinates coordinates)
        {
            _designation = designation;
            _plateau = plateau;
            _currentOrientation = orientation;
            if (_plateau.IsWithinBounds(coordinates))
            {
                CurrentCoordinates = coordinates;
                GetMovementVectors(_currentOrientation.Heading);
            }
            else
            {
                throw new Exception("Rover cannot be initialized outside of plateau.");

            }
        }
        
        public void RunCommands(IEnumerable<IControl> roverCommands) {
            foreach (var command in roverCommands) {
                command.Execute(this);
            }
        }

        public string CurrentLocationAndOrientation() {
            return $"{CurrentCoordinates.ToString()} {_currentOrientation.ToString()}";
        }

        public void RotateRight() {
            _currentOrientation.Right();
            GetMovementVectors(_currentOrientation.Heading);
        }

        public void RotateLeft() {
            _currentOrientation.Left();
            GetMovementVectors(_currentOrientation.Heading);
        }

        public void Move() {
            var positionAfterMove = CurrentCoordinates.CoordinatesAfterMove(XAxisMovementVector, YAxisMovementVector);

            // Ignores the command if rover would be driven off plateau or would collide with an existing rover
            if (_plateau.IsWithinBounds(positionAfterMove) && !_plateau.IsOccupiedCoordinate(positionAfterMove))
            {
                CurrentCoordinates = positionAfterMove;
            }
            else if (!_plateau.IsWithinBounds(positionAfterMove))
            {
                throw new Exception($"Command would cause rover to leave plateau. Coordinates can not be beyond boundaries (0,0) and ({_plateau.TopRightCoordinates.XCoordinate},{_plateau.TopRightCoordinates.YCoordinate}).");
            }
        }
        
        private void GetMovementVectors(CompassHeading heading)
        {
            switch(heading) 
            {
                case CompassHeading.N:
                    XAxisMovementVector = 0;
                    YAxisMovementVector = 1;
                    break;
                case CompassHeading.S:
                    XAxisMovementVector = 0;
                    YAxisMovementVector = -1;
                    break;
                case CompassHeading.E:
                    XAxisMovementVector = 1;
                    YAxisMovementVector = 0;
                    break;
                case CompassHeading.W:
                    XAxisMovementVector = -1;
                    YAxisMovementVector = 0;
                    break;
                default:
                    throw new ArgumentException($"Invalid heading: {heading}");
            }
        }
    }
}