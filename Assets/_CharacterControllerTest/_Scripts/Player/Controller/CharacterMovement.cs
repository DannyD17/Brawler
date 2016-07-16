using Brawler;
using UnityEngine;
 
namespace Brawler
{

    // Character Movement
    // 
    // 'CharacterMovement' component do all the heavy work, such as apply forces, impulses,
    // platforms interaction, and more.
    // 
    // The controller determines how the Character should be moved, such as in response from user input,
    // AI, animation, etc. and pass this information to the CharacterMovement component, which do the movement.

    
    public sealed class CharacterMovement : MonoBehaviour
    {


        [SerializeField]
        private float _maxMoveSpeed = 8.0f;

        [SerializeField]
        private float _maxFallSpeed = 15.0f;

        [SerializeField]
        private bool _useGravity = true;
        
        [SerializeField]
        private float _gravity = 25.0f;
        
        [SerializeField]
        private float _slopeLimit = 45.0f;
        
        [SerializeField]
        private float _slideGravityMultiplier = 1.0f;

        private Rigidbody _rigidbody;

        private GroundDetection _groundDetection;
        
        private Vector3 _groundNormal = Vector3.up;



        // The maximum speed this character can move, including movement from external forces like sliding, collisions, etc.
        // Effective terminal velocity along XZ plane.


        public float maxMoveSpeed
        {
            get { return _maxMoveSpeed; }
            set { _maxMoveSpeed = Mathf.Max(0.0f, value); }
        }


        // The maximum falling speed.
        // Effective terminal velocity along Y axis.


        public float maxFallSpeed
        {
            get { return _maxFallSpeed; }
            set { _maxFallSpeed = Mathf.Max(0.0f, value); }
        }

 
        // Enable / disable character's custom gravity.
        // If enabled the character will be affected by its custom gravity force.


        public bool useGravity
        {
            get { return _useGravity; }
            set
            {
                _useGravity = value;

                if (!_useGravity)
                    return;



                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();


                // If our gravity is enabled,
                // make sure global gravity don't affect this rigidbody

                _rigidbody.useGravity = false;
            }
        }


        // The amount of gravity to be applied to this character.
        // We apply gravity manually for more tuning control.


        public float gravity
        {
            get { return _gravity; }
            set { _gravity = Mathf.Max(0.0f, value); }
        }


        // The maximum angle (in degrees) the slope (under the actor) needs to be before the character starts to slide. 


        public float slopeLimit
        {
            get { return _slopeLimit; }
            set { _slopeLimit = Mathf.Max(0.0f, value); }
        }


        // The percentage of gravity that will be applied to the slide.


        public float slideGravityMultiplier
        {
            get { return _slideGravityMultiplier; }
            set { _slideGravityMultiplier = Mathf.Max(0.0f, value); }
        }

        // The amount of gravity to apply when sliding.


        public float slideGravity
        {
            get { return gravity * slideGravityMultiplier; }
        }


        // The slope angle (in degrees) the character is standing on.
 

        public float slopeAngle { get; private set; }


        // Is a valid slope to walk without slide?


        public bool isValidSlope
        {
            get { return slopeAngle < slopeLimit; }
        }


        // The velocity of the platform the character is standing on,
        // zero (Vector3.zero) if not on a platform.


        public Vector3 platformVelocity { get; private set; }


        // Character's velocity vector.


        public Vector3 velocity
        {
            get { return _rigidbody.velocity - platformVelocity; }
            set { _rigidbody.velocity = value + platformVelocity; }
        }


        // The character forward speed (along its forward vector).


        public float forwardSpeed
        {
            get { return Vector3.Dot(velocity, transform.forward); }
        }


        // The character's current rotation.
        // Setting it comply with the Rigidbody's interpolation setting.


        public Quaternion rotation
        {
            get { return _rigidbody.rotation; }
            set { _rigidbody.MoveRotation(value); }
        }


        // Is this character standing on "ground".


        public bool isGrounded
        {
            get { return _groundDetection.isGrounded; }
        }


        // The impact point in world space where the cast hit the collider.
        // If the character is not on ground, it represent a point at character's base.


        public Vector3 groundPoint
        {
            get { return _groundDetection.groundPoint; }
        }


        // The normal of the surface the cast hit.
        // If the character is not grounded, it will point up (Vector3.up).


        public Vector3 groundNormal
        {
            get { return _groundNormal; }
            private set { _groundNormal = value; }
        }


        // The distance from the ray's origin to the impact point.


        public float groundDistance
        {
            get { return _groundDetection.groundDistance; }
        }


        // The Collider that was hit.
        // This property is null if the cast hit nothing (not grounded) and not-null if it hit a Collider.


        public Collider groundCollider
        {
            get { return _groundDetection.groundCollider; }
        }


        // Rotates the character to face the given direction.


        public void Rotate(Vector3 direction, float angularSpeed, bool onlyXZ = true)
        {
            if (onlyXZ)
                direction.y = 0.0f;

            if (direction.sqrMagnitude < 0.0001f)
                return;

            var targetRotation = Quaternion.LookRotation(direction);
            var newRotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                angularSpeed * Mathf.Deg2Rad * Time.deltaTime);

            _rigidbody.MoveRotation(newRotation);
        }


        // Apply a force to the character's rigidbody.


        public void ApplyForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
        {
            _rigidbody.AddForce(force, forceMode);
        }

        // Apply a vertical impulse (along world's up vector).
        // E.g. Use this to make character jump.


        public void ApplyVerticalImpulse(float impulse)
        {
            var verticalVelocityChange = impulse - velocity.y;
            _rigidbody.AddForce(0.0f, verticalVelocityChange, 0.0f, ForceMode.VelocityChange);
        }


        // Apply a drag to character, an opposing force that scales with current velocity.
        // Drag reduces the effective maximum speed of the character.


        public void ApplyDrag(float drag, bool onlyXZ = true)
        {
            var v = onlyXZ ? velocity.onlyXZ() : velocity;

            var d = -drag * v.sqrMagnitude * v.normalized;
            if (onlyXZ)
                d = Vector3.ProjectOnPlane(d, groundNormal);

            _rigidbody.AddForce(d, ForceMode.Acceleration);
        }


        /// Perform ground detection.


        private void GroundCheck()
        {
            var up = Vector3.up;

            if (_groundDetection.DetectGround())
            {
                // Cache ground normal

                groundNormal = _groundDetection.groundNormal;

                // Check if we are over other rigidbody...

                var otherRigidbody = _groundDetection.groundCollider.attachedRigidbody;

                // If other rigidbody is a dynamic platform (KINEMATIC rigidbody), get its velocity
                // GetPointVelocity will take the angularVelocity of the rigidbody into account when calculating the velocity,
                // allowing for rotating platforms

                platformVelocity = (otherRigidbody != null && otherRigidbody.isKinematic)
                    ? otherRigidbody.GetPointVelocity(transform.position).onlyXZ()
                    : Vector3.zero;

                // If other is a non-kinematic rigidbody, reset groundNormal (point up)
                
                if (otherRigidbody != null && !otherRigidbody.isKinematic)
                    groundNormal = up;

                // Apply slide if steep slope

                slopeAngle = Vector3.Angle(up, groundNormal);
                if (slopeAngle > slopeLimit)
                    _rigidbody.AddForce(up * -slideGravity, ForceMode.Acceleration);

            }
            else
            {
                // Reset info

                slopeAngle = 0.0f;
                groundNormal = up;
                
                platformVelocity = Vector3.zero;
            }
        }


        // Perform character's movement.


        private void ApplyMovement(Vector3 desiredVelocity, bool onlyXZ)
        {
            // Rigidbody's velocity

            var groundVelocity = _rigidbody.GetPointVelocity(transform.position);

            // Computes velocity change

            var velocityChange = desiredVelocity - groundVelocity;
            if (onlyXZ)
                velocityChange = Vector3.ProjectOnPlane(velocityChange.onlyXZ(), groundNormal);

            // Apply velocity change PLUS any platform velocity

            _rigidbody.AddForce(velocityChange + platformVelocity, ForceMode.VelocityChange);
        }


        // Perform character's movement.

        
        private void ApplyMovement(Vector3 desiredVelocity, float acceleration, float deceleration, bool onlyXZ)
        {
            // Computes desired velocity accelerating / decelerating character's velocity towards target velocity

            desiredVelocity = desiredVelocity.sqrMagnitude > 0.0001f
                ? Vector3.MoveTowards(velocity, desiredVelocity, acceleration * Time.deltaTime)
                : Vector3.MoveTowards(velocity, desiredVelocity, deceleration * Time.deltaTime);

            // Performs movement

            ApplyMovement(desiredVelocity, onlyXZ);
        }


        // Make sure we don't move any faster than our maxMoveSpeed.


        private void LimitMovementVelocity()
        {
            var hVelocity = velocity.onlyXZ();
            if (hVelocity.sqrMagnitude <= maxMoveSpeed * maxMoveSpeed)
                return;

            var velocityChange = hVelocity.normalized * maxMoveSpeed - hVelocity;
            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }


        // Apply custom gravity acceleration.


        private void ApplyGravity()
        {
            var gravityForce = groundNormal * -gravity;
            _rigidbody.AddForce(gravityForce, ForceMode.Acceleration);
        }


        // Make sure we don't fall any faster than maxFallSpeed.


        private void LimitFallSpeed()
        {
            var verticalVelocity = velocity.y;
            if (_groundDetection.isGrounded || verticalVelocity > -maxFallSpeed)
                return;

            var verticalVelocityChange = -maxFallSpeed - verticalVelocity;
            _rigidbody.AddForce(0.0f, verticalVelocityChange, 0.0f, ForceMode.VelocityChange);
        }


        // Performs character's movement, will apply our custom gravity if useGravity == true.
        // This function will apply the velocity change instantly (no acceleration / deceleration).
        // 
        // Must be called in FixedUpdate.


        public void Move(Vector3 desiredVelocity, bool onlyXZ = true)
        {
            GroundCheck();

            ApplyMovement(desiredVelocity, onlyXZ);
            LimitMovementVelocity();

            if (useGravity)
                ApplyGravity();

            LimitFallSpeed();
        }


        // Perform character's movement, will apply custom gravity if useGravity == true.
        //
        // Must be called in FixedUpdate.


        public void Move(Vector3 desiredVelocity, float acceleration, float deceleration, bool onlyXZ = true)
        {
            GroundCheck();

            ApplyMovement(desiredVelocity, acceleration, deceleration, onlyXZ);
            LimitMovementVelocity();

            if (useGravity)
                ApplyGravity();

            LimitFallSpeed();
        }


        public void OnValidate()
        {
            maxMoveSpeed = _maxMoveSpeed;
            maxFallSpeed = _maxFallSpeed;

            useGravity = _useGravity;
            gravity = _gravity;

            slopeLimit = _slopeLimit;
            slideGravityMultiplier = _slideGravityMultiplier;
        }

        public void Awake()
        {
            // Cache an initialize components

            _groundDetection = GetComponent<GroundDetection>();
            if (_groundDetection == null)
            {
                Debug.LogError(
                    string.Format(
                        "CharacterMovement: No 'GroundDetection' found for {0} game object.\nPlease add a 'GroundDetection' component to {0} game object",
                        name));

                return;
            }

            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null)
            {
                Debug.LogError(
                    string.Format(
                        "CharacterMovement: No 'Rigidbody' found for {0} game object.\nPlease add a 'Rigidbody' component to {0} game object",
                        name));

                return;
            }
            
            if (useGravity)
                _rigidbody.useGravity = false;

            _rigidbody.isKinematic = false;
            _rigidbody.freezeRotation = true;

            // Attempt to validate frictionless material (if collider found in this gameobject)

            var aCollider = GetComponent<CapsuleCollider>();
            if (aCollider == null)
                return;

            var physicMaterial = aCollider.sharedMaterial;
            if (physicMaterial != null)
                return;

            physicMaterial = new PhysicMaterial("Frictionless")
            {
                dynamicFriction = 0.0f,
                staticFriction = 0.0f,
                bounciness = 0.0f,
                frictionCombine = PhysicMaterialCombine.Multiply,
                bounceCombine = PhysicMaterialCombine.Multiply
            };

            aCollider.material = physicMaterial;

            Debug.LogWarning(
                string.Format(
                    "CharacterMovement: No 'PhysicMaterial' found for {0}'s Collider, a frictionless one has been created and assigned.\n You should add a Frictionless 'PhysicMaterial' to '{0}' game object.",
                    name));
        }
        

    }
}