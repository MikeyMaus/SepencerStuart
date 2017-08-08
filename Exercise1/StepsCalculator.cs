using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise1
{
    public class StepsCalculator
    {
        private readonly List<int> _flights;
        private readonly int _stepsPerStride;

        private const int TurnAroundSteps = 2;

        public StepsCalculator(int[] flights, int stepsPerStride)
        {
            if (flights == null || flights.Length == 0) throw new ArgumentException("There are no Stairs");
            if (flights.Length > 50) throw new ArgumentException("Hey, you said The Max was 50 flights of Stairs");
            if (stepsPerStride < 2 || stepsPerStride > 5) throw new ArgumentException("You're either too small or too large to be climbing these stairs");
            _flights = new List<int>(flights);
            _stepsPerStride = stepsPerStride;
        }

        public int Calculate()
        {
            int stepCounter = 0;
            foreach(var flight in _flights)
            {
                if(stepCounter != 0)
                {
                    stepCounter += TurnAroundSteps;
                }
                var stepsNeeded = (int) Math.Ceiling( (double)flight / _stepsPerStride);
                stepCounter += stepsNeeded;
            }
            return stepCounter;
        }
    }
}
