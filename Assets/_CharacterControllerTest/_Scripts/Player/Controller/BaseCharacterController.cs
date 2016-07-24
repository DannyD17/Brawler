using UnityEngine;
using InControl;
using System;

namespace Brawler
{


    public class BaseCharacterController : MonoBehaviour
    {

        public int playerNum;

        [Header("Steering")]
        [SerializeField]
        private float _speed = 5.0f;

        [SerializeField]
        private float _angularSpeed = 240.0f;

        [SerializeField]
        private float _acceleration = 20.0f;

        [SerializeField]
        private float _deceleration = 20.0f;

        [Range(0.0f, 2.0f)]
        [SerializeField]
        private float _brakingFriction = 0.6f;

        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float _airControl = 0.2f;

        [Header("Jump")]
        [SerializeField]
        private float _baseJumpHeight = 2.0f;

        [SerializeField]
        private float _extraJumpTime = 0.5f;

        [SerializeField]
        private float _extraJumpPower = 10.0f;


        private bool _jump;
        protected bool _canJump;
        private bool _isJumping;

        protected bool _updateJumpTimer;
        private float _jumpTimer;


        // Cached CharacterMovement component.


        protected CharacterMovement movement { get; private set; }


        // Maximum movement speed (in m/s).


        public float speed
        {
            get { return _speed; }
            set { _speed = Mathf.Max(0.0f, value); }
        }


        // Maximum turning speed (in deg/s).


        public float angularSpeed
        {
            get { return _angularSpeed; }
            set { _angularSpeed = Mathf.Max(0.0f, value); }
        }


        // The rate of change of velocity.


        public float acceleration
        {
            get { return movement.isGrounded ? _acceleration : _acceleration * airControl; }
            set { _acceleration = Mathf.Max(0.0f, value); }
        }


        // The rate at which the character's slows down.


        public float deceleration
        {
            get { return movement.isGrounded ? _deceleration : _deceleration * airControl; }
            set { _deceleration = Mathf.Max(0.0f, value); }
        }


        // Friction (drag) coefficient applied when braking (whenever desiredVelocity ~ 0).
        // When braking, this property allows you to control how much friction is applied when moving across the ground,
        // applying an opposing force that scales with current velocity.
        // 
        // Braking is composed of friction (velocity-dependent drag) and constant deceleration.


        public float brakingFriction
        {
            get { return movement.isGrounded ? _brakingFriction : 0.0f; }
            set { _brakingFriction = Mathf.Clamp(value, 0.0f, 2.0f); }
        }


        // When not grounded, the amount of lateral movement control available to the character.
        // 0 == no control, 1 == full control. Defaults to 0.2.


        public float airControl
        {
            get { return _airControl; }
            set { _airControl = Mathf.Clamp01(value); }
        }


        // The initial jump height (in meters).


        public float baseJumpHeight
        {
            get { return _baseJumpHeight; }
            set { _baseJumpHeight = Mathf.Max(0.0f, value); }
        }


        // Computed jump impulse.
 

        public float jumpImpulse
        {
            get { return Mathf.Sqrt(2.0f * baseJumpHeight * movement.gravity); }
        }


        // The extra jump time (e.g. holding jump button) in seconds.


        public float extraJumpTime
        {
            get { return _extraJumpTime; }
            set { _extraJumpTime = Mathf.Max(0.0f, value); }
        }


        // Acceleration while jump button is held down, given in meters / sec^2.


        public float extraJumpPower
        {
            get { return _extraJumpPower; }
            set { _extraJumpPower = Mathf.Max(0.0f, value); }
        }


        // Jump input command.

        
        public bool jump
        {
            get { return _jump; }
            protected set
            {
                _jump = value;

                // If jump button is released, allow to jump again

                if (!_jump)
                    _canJump = true;
            }
        }


        // True if character is jumping, false if not.


        public bool isJumping
        {
            get
            {
                // We are in jump mode but just became grounded

                if (_isJumping)
                    return !movement.isGrounded;

                return _isJumping;
            }
        }


        // Movement input command.


        public Vector3 moveDirection { get; set; }


        protected void Jump()
        {
            // Is jump button released?

            if (!_canJump)
                return;

            // Jump button not pressed or not in ground, return

            if (!jump || !movement.isGrounded)
                return;

            // Apply jump impulse

            _canJump = false;
            _isJumping = true;
            _updateJumpTimer = true;

            movement.ApplyVerticalImpulse(jumpImpulse);
        }

        protected void UpdateJumpTimer()
        {
            if (!_updateJumpTimer)
                return;

            // If jump button is held down and extra jump time is not exceeded...

            if (jump && _jumpTimer < extraJumpTime)
            {
                // Update jump timer

                _jumpTimer = Mathf.Min(_jumpTimer + Time.deltaTime, extraJumpTime);

                // Apply vertical acceleration to simulate variable height jump

                movement.ApplyForce(Vector3.up * extraJumpPower, ForceMode.Acceleration);
            }
            else
            {
                // Button released or extra jump time ends, reset info

                _jumpTimer = 0.0f;
                _updateJumpTimer = false;
            }
        }



        public virtual void OnValidate()
        {
            speed = _speed;
            angularSpeed = _angularSpeed;

            acceleration = _acceleration;
            deceleration = _deceleration;
            brakingFriction = _brakingFriction;

            airControl = _airControl;

            baseJumpHeight = _baseJumpHeight;
            extraJumpTime = _extraJumpTime;
            extraJumpPower = _extraJumpPower;
        }

        public virtual void Awake()
        {
            // Cache components

            movement = GetComponent<CharacterMovement>();
        }

        public virtual void FixedUpdate()
        {
            // Apply movement

            var desiredVelocity = Vector3.ClampMagnitude(moveDirection * speed, speed);
            movement.Move(desiredVelocity, acceleration, deceleration);

            // Apply braking drag

            if (Mathf.Approximately(moveDirection.sqrMagnitude, 0.0f))
                movement.ApplyDrag(brakingFriction);

            // Apply Jump

            Jump();
            UpdateJumpTimer();
        }

        public virtual void Update()
        {
            // Handle input

            //moveDirection = new Vector3 //OLD
            //{//OLD
            //    x = Input.GetAxisRaw("Horizontal"),//OLD
            //    y = 0.0f,//OLD
            //    z = Input.GetAxisRaw("Vertical")//OLD
            //};//OLD

            //jump = Input.GetButton("Jump");//OLD

            // Rotate towards input movement direction
			try{
			var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
            if (inputDevice == null)
            {
                // If no controller exists for this cube, just make it translucent.
           //     cubeRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            }
            else
            {
              //  UpdateCubeWithInputDevice(inputDevice);
            }
            moveDirection = new Vector3
            {
                x = inputDevice.Direction.X,
                y = 0.0f,
                z = inputDevice.Direction.Y
            };
				jump = inputDevice.Action1;
			} catch (Exception e){
				moveDirection = Vector3.zero;
				jump = false;
			}
            

            movement.Rotate(moveDirection, angularSpeed);
        }


    }
}