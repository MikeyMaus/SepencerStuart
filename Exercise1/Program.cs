using System;
using System.Linq;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            int stepsPerStride;
            int[] steps;
            try
            {
                stepsPerStride = int.Parse(args[0]);
                steps = args[1].Split(',').Select(Int32.Parse).ToArray();
            }
            catch (Exception)
            {
                throw new Exception("StepsPerStride as the first argument and a comma seperated list of Integers second please.");
            }
            

            //var steps = new int[] { 5,11,9,13,8,30,14};
            //var stepsPerStride = 3;

            var calculator = new StepsCalculator(steps, stepsPerStride);
            var strides = calculator.Calculate();
            Console.WriteLine(strides);

        }
    }
}