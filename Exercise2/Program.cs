using Exercise2.SafePlaceCalculators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Exercise2
{
    public class Program
    {
        public const int DimensionSize = 1000;
        
        private struct TestCase
        {
            public List<int[]> Bombs;
        }

        public static void Main(string[] args)
        {
            List<string> linesOfText = null;
            try
            {
                linesOfText = File.ReadAllLines(args[0]).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Please pass a URL to a text file with containing test cases. Commas between the coordinates and spaces between the positions");
                /* The requirements on this were a bit vague, I'm expecting an input like this
                 * 2
                 * 3 0,0,0 50,50,50 1000,1000,1000
                 * 1 500,500,500
                 */
            }

            //var testCases = ExtractTestCasesFromText(linesOfText);
            var testCases = SimulateSomeRandomBombs();

            foreach (var tc in testCases)
            {
                var calculator = new RandomCalculator(tc.Bombs, 10000);           //Raise or lower the iterations depending on how until the bombs go off
                //var calculator = new BruteForceCalculator(bombPositions);       //Only works well in dimensions of 300 or less
                calculator.CalculateSafestPlaceToHide();

                //Console.WriteLine(
                //    $"Safest Position {calculator.SafestPosition[0]},{calculator.SafestPosition[1]},{calculator.SafestPosition[2]} " +
                //    $"- Distance {calculator.SafestDistance}");
                Console.WriteLine(calculator.SafestDistanceAsInt);
            }
            Console.ReadLine();
        }

        private static List<TestCase> ExtractTestCasesFromText(List<string> inputBombs)
        {
            var numberOfTestCases = int.Parse(inputBombs.FirstOrDefault() ?? "0");
            var returned = new List<TestCase>();
            if (numberOfTestCases == 0) return returned;

            var expectedTestCases = int.Parse(inputBombs[0]);

            foreach (var input in inputBombs.Skip(1))       //Skip the number of test cases
            {
                var separatedInput = input.Split(' ');
                var expectedCoordsInRow = int.Parse(separatedInput[0]);
                var testCaseCordinates = new List<int[]>();
                foreach (var coord in separatedInput.Skip(1))       //Skip the number of Bombs
                {
                    var splitCoords = coord.Split(',');
                    var newCoords = splitCoords.Select(int.Parse).ToArray();
                    testCaseCordinates.Add(newCoords);
                }
                if (testCaseCordinates.Count != expectedCoordsInRow) throw new InvalidDataException("Bomb counter number doesn't match the Coordinates input");
                returned.Add(new TestCase { Bombs = testCaseCordinates });
            }

            if (returned.Count != expectedTestCases) throw new InvalidDataException("Test cases number doesn't match the cases input");
            return returned;
        }

        private static List<TestCase> SimulateSomeBombs()
        {
            var positions = new List<int[]>
            {
                new[] {1000, 1000, 1000},
                new[] {50, 50, 50},
                new[] {492, 318, 200},
                new[] {1, 4, 94},
                new[] {1, 900, 800}
            };
            return new List<TestCase> { new TestCase { Bombs = positions } };
        }

        private static List<TestCase> SimulateSomeRandomBombs()
        {
            var positions = new List<int[]>();
            var rand = new Random(1);
            for (int i = 0; i < 200; i++)
            {
                positions.Add(new[] { rand.Next(0, DimensionSize), rand.Next(0, DimensionSize), rand.Next(0, DimensionSize) });
            }
            return new List<TestCase>() { new TestCase { Bombs = positions } };
        }
    }

}