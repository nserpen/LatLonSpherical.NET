using System;
using System.Linq;

namespace LatLonSpherical
{
    public class Dms
    {
        /// <summary>
        /// Constrain degrees to range -90..+90 (e.g. for latitude); -91 => -89, 91 => 89.
        /// </summary>
        public static double Wrap90(double degrees)
        {
            if (-90d <= degrees && degrees <= 90d) return degrees;          // avoid rounding due to arithmetic ops if within range
            return Math.Abs((degrees % 360d + 270d) % 360d - 180d) - 90d;   // triangle wave p:360 a:±90 TODO: fix e.g. -315°
        }

        /// <summary>
        /// Constrain degrees to range -180..+180 (e.g. for longitude); -181 => 179, 181 => -179.
        /// </summary>
        public static double Wrap180(double degrees)
        {
            if (-180d < degrees && degrees <= 180d) return degrees;         // avoid rounding due to arithmetic ops if within range
            return (degrees + 540d) % 360d - 180d;                          // sawtooth wave p:180, a:±180
        }
        
        public static double Wrap360(double degrees)
        {
            if (0d <= degrees && degrees < 360d) return degrees;            // avoid rounding due to arithmetic ops if within range
            return (degrees % 360 + 360) % 360;                             // sawtooth wave p:360, a:360
        }
    }

}
