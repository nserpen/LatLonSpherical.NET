using System;
using System.Linq;

namespace LatLonSpherical
{
    internal static class Extensions
    {
        internal static double ToRadians(this double degree)
        {
            return degree * Math.PI / 180d;
        }

        internal static double ToDegrees(this double radians)
        {
            return radians * 180d / Math.PI;
        }
    }
}
