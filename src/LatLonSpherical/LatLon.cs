using System;
using System.Diagnostics;
using System.Linq;

namespace LatLonSpherical
{
    public class LatLon
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public LatLon(double lat, double lon)
        {
            Latitude = Dms.Wrap90(lat);
            Longitude = Dms.Wrap180(lon);
        }

        /// <summary>
        /// Calculates the distance along the surface of the earth from 'this' point to destination point.
        /// Uses haversine formula: a = sin²(Δφ/2) + cosφ1·cosφ2 · sin²(Δλ/2); d = 2 · atan2(√a, √(a-1)).
        /// </summary>
        /// <param name="point">Destination point (latitude/longitude)</param>
        /// <param name="radius">Radius of earth (defaults to mean radius in metres).</param>
        /// <returns>Distance between this point and destination point, in same units as {radius}</returns>
        public double DistanceTo(LatLon point, double radius = Constants.RadiusOfEarth_Meters)
        {
            if (radius <= 0d)
                throw new ArgumentException("Invalid radius", nameof(radius));

            // a = sin²(Δφ/2) + cos(φ1)⋅cos(φ2)⋅sin²(Δλ/2)
            // δ = 2·atan2(√(a), √(1−a))
            // see mathforum.org/library/drmath/view/51879.html for derivation

            double φ1 = Latitude.ToRadians(), λ1 = Longitude.ToRadians();
            double φ2 = point.Latitude.ToRadians(), λ2 = point.Longitude.ToRadians();
            double Δφ = φ2 - φ1;
            double Δλ = λ2 - λ1;

            double a = Math.Sin(Δφ / 2d) * Math.Sin(Δφ / 2d) + Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(Δλ / 2d) * Math.Sin(Δλ / 2d);
            double c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            return radius * c;
        }

        public double DistanceTo(double lat, double lon, double radius = Constants.RadiusOfEarth_Meters)
        {
            return DistanceTo(new LatLon(lat, lon), radius);


        }
    }
}
