using UnityEngine;

namespace Brawler
{
    public static class Extensions
    {

        /// Discards vector Y component.


        public static Vector3 onlyXZ(this Vector3 vector3)
        {
            vector3.y = 0.0f;
            return vector3;
        }
    }
}