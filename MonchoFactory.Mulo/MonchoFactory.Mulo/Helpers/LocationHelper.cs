using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonchoFactory.Mulo.WebApi.Helpers
{
    public class LocationHelper
    {

        /// <summary>
        /// Radius of the Earth in Kilometers.
        /// </summary>
        private const double EARTH_RADIUS_KM = 6371;

        /// <summary>
        /// Converts an angle to a radian.
        /// </summary>
        /// <param name="input">The angle that is to be converted.</param>
        /// <returns>The angle in radians.</returns>
        private static double ToRad(double input)
        {
            return input * (Math.PI / 180);
        }

        /// <summary>
        /// Calculates the distance between two geo-points in kilometers using the Haversine algorithm.
        /// </summary>
        public static double GetDistanceKM(double lat1, double lng1, double lat2, double lng2)
        {
            double dLat = ToRad(lat2 - lat1);
            double dLon = ToRad(lng2 - lng1);

            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                       Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EARTH_RADIUS_KM * c;
            return distance;
        }

        public static double GetDistanceInBlocks(double lat1, double lng1, double lat2, double lng2)
        {
            return (GetDistanceKM(lat1, lng1, lat2, lng2) * 10);
        }
    }
}