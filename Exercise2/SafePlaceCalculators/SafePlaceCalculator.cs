using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise2.SafePlaceCalculators
{
    public abstract class SafePlaceCalculator
    {
        protected List<int[]> Bombs;

        protected int DimensionSize;
        public decimal SafestDistance { get; protected set; }

        public int SafestDistanceAsInt => (int) SafestDistance;

        protected int RadiusSize => DimensionSize / 2;

        public int[] SafestPosition { get; protected set; }


        public SafePlaceCalculator(List<int[]> bombs)
        {

            if (bombs == null || !bombs.Any()) throw new Exception("There are no bombs, hide where ever you want");
            Bombs = bombs;
            DimensionSize = Program.DimensionSize;
        }

        public abstract void CalculateSafestPlaceToHide();

        protected static decimal DistanceBetween(int[] point1, int[] point2)
        {
            var dist = (decimal) Math.Sqrt(point1.Zip(point2, (a, b) => (a - b) * (a - b)).Sum());
            return dist;
        }

        protected void CheckClosestDistanceToBomb(int[] point)
        {
            decimal cloestDistanceToBomb = decimal.MaxValue;
            foreach (var bomb in Bombs)
            {
                var distanceToBomb = DistanceBetween(point, bomb);
                if(distanceToBomb < cloestDistanceToBomb)
                {
                    cloestDistanceToBomb = distanceToBomb;
                }
            }

            if(cloestDistanceToBomb > SafestDistance)
            {
                SafestDistance = cloestDistanceToBomb;
                SafestPosition = point;
            }
            
            
        }
    }
}
