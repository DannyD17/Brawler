using UnityEngine;

namespace Brawler
{
    public static class Utils
    {
        /// Sinusoidal ease function.
        public static float EaseInOut(float time, float duration)
        {
            return -0.5f * (Mathf.Cos(Mathf.PI * time / duration) - 1.0f);
        }


        /// Wrap an angle in degrees around 360 degrees.
        public static float WrapAngle(float degrees)
        {
            if (degrees > 360.0f)
                degrees -= 360.0f;
            else if (degrees < 0.0f)
                degrees += 360.0f;

            return degrees;
        }
    }
}