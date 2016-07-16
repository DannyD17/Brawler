using UnityEngine;

namespace Brawler
{
    public abstract class GroundDetection : MonoBehaviour
    {


        [SerializeField]
        private LayerMask _groundMask = 1; // Default layer

        [SerializeField]
        private Vector3 _center;

        [SerializeField]
        private float _radius = 0.45f;

        [SerializeField]
        private float _distance = 1.1f;


        protected RaycastHit _hitInfo;


        // The character's center position in object’s local space (relative to its pivot).
        // E.g. If your character's height is 2 units, set this to 0.0f, 1.0f, 0.0f.
        
        // This determines the cast starting point.


        public Vector3 center
        {
            get { return _center; }
            set { _center = value; }
        }

 
        // The bounding volume radius.
        // This value should be the radius of the “feet” or bottom of the actor,
        // a typical value should be about 90-95% of your collider's radius.
        //
        // E.g. If your character collider's radius is 0.5 units, set this to ~ 0.45f to 0.475f.


        public float radius
        {
            get { return _radius; }
            set { _radius = Mathf.Max(0.0f, value); }
        }


        // Determines the cast distance.
        // A typical value should be about ~10% more than your collider's half height.
        // E.g. If your character's height is 2 units, you should set this to ~1.1 units.
        // 
        // Increase it if you loose ground often.


        public float distance
        {
            get { return _distance; }
            set { _distance = Mathf.Max(0.0f, value); }
        }


        // Layers to be considered as ground (walkables).


        public LayerMask groundMask
        {
            get { return _groundMask; }
            set { _groundMask = value; }
        }


        // Is this character standing on "ground".


        public bool isGrounded { get; protected set; }


        // The impact point in world space where the cast hit the collider.
        // If the character is not on ground, it represent a point at character's base.


        public Vector3 groundPoint
        {
            get { return _hitInfo.point; }
            protected set { _hitInfo.point = value; }
        }


        // The normal of the surface the cast hit.
        // If the character is not grounded, it will point up (Vector3.up).


        public Vector3 groundNormal
        {
            get { return _hitInfo.normal; }
            protected set { _hitInfo.normal = value; }
        }


        // The distance from the ray's origin to the impact point.
 

        public float groundDistance
        {
            get { return _hitInfo.distance; }
            protected set { _hitInfo.distance = value; }
        }


        // The Collider that was hit.
        // This property is null if the cast hit nothing (not grounded) and not-null if it hit a Collider.


        public Collider groundCollider
        {
            get { return _hitInfo.collider; }
        }


        // Abstract method used to perform ground detection.
        // Returns true if grounded, false if not.


        public abstract bool DetectGround();



        // Validate this fields.
        // If you overrides it, be sure to call base.OnValidate to fully initialize base class.


        public virtual void OnValidate()
        {
            radius = _radius;
            distance = _distance;
        }

    }
}
