﻿using UnityEngine;

namespace Brawler
{
    public sealed class SphereGroundDetection : GroundDetection
    {

        /// Performs ground detection using SphereCast.


        public override bool DetectGround()
        {
            var o = transform.TransformPoint(center);
            var d = distance - radius;

            isGrounded = Physics.SphereCast(o, radius, -transform.up, out _hitInfo, d, groundMask.value,
                QueryTriggerInteraction.Ignore);

            if (isGrounded)
                return isGrounded;

            // If not grounded, reset info
            // This is important in order to ensure continuity even when the character is in air

            groundNormal = Vector3.up;
            groundPoint = o - transform.up * distance;

            return isGrounded;
        }
    }
}
