using UnityEditor;
using UnityEngine;


namespace Brawler
{

    public class GroundDetectionEditor : Editor
    {
        [DrawGizmo(GizmoType.Selected)]
        static void DrawGroundDetectionGizmos(GroundDetection groundDetection, GizmoType gizmosType)
        {
            if (!Application.isPlaying)
                return;

            var groundPoint = groundDetection.groundPoint;
            var groundNormal = groundDetection.groundNormal;

            // Ground Normal

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(groundPoint, groundNormal);

            // Ground point

            var r = Vector3.ProjectOnPlane(Vector3.right, groundNormal);
            var f = Vector3.ProjectOnPlane(Vector3.forward, groundNormal);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(groundPoint - r * 0.25f, groundPoint + r * 0.25f);
            Gizmos.DrawLine(groundPoint - f * 0.25f, groundPoint + f * 0.25f);
        }

        [DrawGizmo(GizmoType.Selected)]
        static void DrawSphereGroundDetectionGizmos(SphereGroundDetection sphereGroundDetection, GizmoType gizmoType)
        {
            // SphereCast (origin and end point)

            Gizmos.color = !Application.isPlaying
                ? new Color(1.0f, 0.7f, 0.0f)
                : sphereGroundDetection.isGrounded ? Color.green : Color.red;

            var transform = sphereGroundDetection.transform;

            var o = transform.TransformPoint(sphereGroundDetection.center);
            var d = sphereGroundDetection.distance - sphereGroundDetection.radius;

            // SphereCast (origin and end point)

            Gizmos.DrawWireSphere(o, 0.05f);
            Gizmos.DrawWireSphere(o - transform.up * d, sphereGroundDetection.radius);
       }

   }
}
