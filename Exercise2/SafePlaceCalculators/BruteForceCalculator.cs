using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise2.SafePlaceCalculators
{
    /// <summary>
    /// Safe but too slow. There's no time to run this while there are bombs outside ticking away
    /// </summary>
    public class BruteForceCalculator : SafePlaceCalculator
    {
        public BruteForceCalculator(List<int[]> bombs) : base(bombs)
        {

        }

        public override void CalculateSafestPlaceToHide()
        {

            int[] pointToCheck = new int[3];
            for (int x = 0; x <= DimensionSize; x++)
            {
                for (int y = 0; y <= DimensionSize; y++)
                {
                    for (int z = 0; z <= DimensionSize; z++)
                    {
                        pointToCheck[0] = x;
                        pointToCheck[1] = y;
                        pointToCheck[2] = z;
                        CheckClosestDistanceToBomb(pointToCheck);
                    }
                }
            }
        }

    }
}

