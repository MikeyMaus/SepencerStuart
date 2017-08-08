using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise2.SafePlaceCalculators
{
    /// <summary>
    /// This Calculator isn't guaranteed to find the furthest distance, but when there are bombs all around you
    /// Either you know when they go off and can tune this up to increase accuracy, or calculate for a few seconds and take your chances
    /// No time for Voronoi - Largest Sphere available calculations!
    /// "Never tell me the odds" - Han Solo
    /// </summary>
    public class RandomCalculator : SafePlaceCalculator
    {
        private readonly Random _generator;
        private readonly int _iterations;

        private const int ImprovementCycles = 10;
        public RandomCalculator(List<int[]> bombs, int iterations) : base(bombs)
        {
            _generator = new Random(1);
            _iterations = iterations;
        }

        public override void CalculateSafestPlaceToHide()
        {
            FindHidingSpots();
            TryToImproveOnHidingSpot(DimensionSize / (RadiusSize / 2));
        }

        private void FindHidingSpots()
        {
            for (int i = 0; i < _iterations; i++)
            {
                var potentialHidingSpot = GetRandomPosition();
                CheckClosestDistanceToBomb(potentialHidingSpot);
            }
        }

        private void TryToImproveOnHidingSpot(int reducedRadius)
        {
            if (reducedRadius == 1) return;
            for (int i = 0; i < ImprovementCycles; i++)
            {
                var maybeImprovedPosition = ImproveOnRandomPosition(SafestPosition, reducedRadius);
                CheckClosestDistanceToBomb(maybeImprovedPosition);
            }
            TryToImproveOnHidingSpot(reducedRadius / 2);
        }

        private int[] GetRandomPosition()
        {
            return new [] { _generator.Next(0, DimensionSize)
                        , _generator.Next(0, DimensionSize)
                        , _generator.Next(0, DimensionSize) };
        }

        private int[] ImproveOnRandomPosition(int[] p, int improvementRange)
        {
            var returned = new []{  _generator.Next(Math.Max( p[0] - improvementRange, 0), Math.Min(p[0] + improvementRange, DimensionSize))
                    ,  _generator.Next(Math.Max(p[1] -improvementRange, 0), Math.Min(p[1] + improvementRange, DimensionSize))
                    ,  + _generator.Next(Math.Max(p[2] -improvementRange, 0), Math.Min(p[2] + improvementRange, DimensionSize)) };
            return returned;
        }
    }
}
