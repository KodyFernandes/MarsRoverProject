using System;
using System.Collections.Generic;
using System.Linq;
using MarsRoverProject.Controls;
using MarsRoverProject.Enums;
using MarsRoverProject.Environment;
using MarsRoverProject.Utilities;
using MarsRoverProject.Vehicle;

namespace MarsRoverProject
{
    public class MissionControl
    {
        private static readonly List<IVehicle> Rovers = new List<IVehicle>();
        private static IPlateau _plateau;

        public static void Main(string[] args)
        {
            var plateauBounds = Console.ReadLine()?.Trim();
            InitializePlateau(plateauBounds);

            var inputLine = Console.ReadLine()?.Trim();
            var roverIterator = 1;
            while (!string.IsNullOrWhiteSpace(inputLine)) {
                var initialRoverParams = inputLine.Trim();
                var rover = InitializeRover(roverIterator, initialRoverParams);

                inputLine = Console.ReadLine()?.Trim();
                var commands = inputLine?.ToUpper();

                try
                {
                    LaunchRover(rover, commands);
                    _plateau.AddOccupiedCoordinates(rover.CurrentCoordinates);
                    Rovers.Add(rover);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                roverIterator++;
                inputLine = Console.ReadLine();
            }
            foreach (var vehicle in Rovers)
            {
                Console.WriteLine($"{vehicle.CurrentLocationAndOrientation()}");
            }
        }
        
        private static void LaunchRover(IVehicle rover, string commandString) {
            IEnumerable<IControl> roverCommands = CommandParser.ParseCommands(commandString);
            rover.RunCommands(roverCommands);
        }
        
        private static void InitializePlateau(string plateauData)
        {
            var plateauBounds = plateauData.Trim().Split(' ').Select(int.Parse).ToList();
            if (plateauBounds.Count == 2 && plateauBounds.TrueForAll(bound => bound > 0))
            {
                _plateau = new Plateau(plateauBounds[0], plateauBounds[1]);
            }
            else
            {
                throw new Exception($"Invalid parameters provided for plateau: \"{plateauData}\"");
            }
        }
        
        private static MarsRover InitializeRover(int roverDesignation, string roverData)
        {
            var initialRoverParams = roverData.Split(' ');
            if (initialRoverParams.Length != 3)
            {
                throw new Exception($"Invalid parameters provided for rover {roverDesignation}: \"{roverData}\"");
            }
                
            ICoordinates initialRoverCoordinates = new Coordinates(Convert.ToInt32(initialRoverParams[0]),
                Convert.ToInt32(initialRoverParams[1]));
            IOrientation initialRoverOrientation = new Orientation((CompassHeading) Enum.Parse(typeof(CompassHeading), initialRoverParams[2]));
                
            return new MarsRover(roverDesignation, _plateau, initialRoverOrientation, initialRoverCoordinates);
        }
    }
}