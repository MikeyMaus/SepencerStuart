using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise2.SafePlaceCalculators
{
    /// <summary>
    /// This looks like the way to do this properly, but I don't have time to build a Voronai solution for this
    /// https://www.youtube.com/watch?v=Y5X1TvN9TpM
    /// https://www.youtube.com/watch?v=z6oxjdrB6ok
    /// 
    /// </summary>
    public class VoronoiCalculator : SafePlaceCalculator
    {
        public VoronoiCalculator(List<int[]> bombs) : base(bombs)
        {
            throw new NotImplementedException("");
        }

        public override void CalculateSafestPlaceToHide()
        {
            throw new NotImplementedException("");
        }
    }
}
