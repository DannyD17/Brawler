using UnityEngine;
using System.Collections;

namespace Brawler
{

    [RequireComponent(typeof(InputPlayer))] // requires InputPlayer script


    public class PlayerMovement : StateMachine
    {

        public Transform character;

        public float moveSpeed = 4.0f;
        public float moveAcceleration = 30.0f;
        public float jumpAcceleration = 5.0f;
        public float jumpHeight = 3.0f;
        public float initialRotation = 0.0f;

        public CapsuleCollider capsuleCollider;
        public LayerMask groundLayer;

        private Rigidbody rb;


        private bool grounded = true;

        private InputPlayer input;

        // Player states. Add more states by comma separating them
        enum PlayerStates { Idle, Walk, Jump, Fall }

        // current velocity
        private Vector3 moveDirection;

        // current direction our character's art is facing
        public Vector3 lookDirection { get; private set; }


        // Use this for initialization
        void Start()
        {
            // Our character's current facing direction, planar to the ground
            lookDirection = transform.forward;
            // Grab the input object from our object
            input = gameObject.GetComponent<InputPlayer>();
            // Set our currentState to idle on startup
            currentState = PlayerStates.Idle;

            rb = gameObject.GetComponent<Rigidbody>();




        }

        protected override void EarlyGlobalSuperUpdate()
        {
            moveDirection = Vector3.MoveTowards(moveDirection, LocalMovement() * moveSpeed, moveAcceleration * Time.deltaTime);
            lookDirection = Quaternion.AngleAxis(initialRotation, transform.up) * Vector3.forward;

            CheckIfPlayerGrounded();

                // Put any code in here you want to run BEFORE the state's update function.
                // This is run regardless of what state you're in
            
        }

        protected override void LateGlobalSuperUpdate()
        {
            // Put any code in here you want to run AFTER the state's update function.
            // This is run regardless of what state you're in

            // Move the player by our velocity every frame
            transform.position += moveDirection * Time.deltaTime;

            // Rotate our mesh to face where we are "looking"
           if (input.current.MoveInput.x != 0 || input.current.MoveInput.z != 0)
                character.rotation = Quaternion.LookRotation(moveDirection, transform.up);

            

        }

    


        private Vector3 LocalMovement()
        {
            Vector3 right = Vector3.Cross(transform.up, lookDirection);

            Vector3 local = Vector3.zero;

            if (input.current.MoveInput.x != 0)
            {
                local += right * input.current.MoveInput.x;
            }

            if (input.current.MoveInput.z != 0)
            {
                local += lookDirection * input.current.MoveInput.z;
            }

            return local.normalized;
        }

              void Jump()
            {

                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);


             }

        // Calculate the initial velocity of a jump based off gravity and desired maximum height attained
        private float CalculateJumpSpeed(float jumpHeight, float gravity)
        {
            return Mathf.Sqrt(2 * jumpHeight * gravity);
        }


        // Below are state functions. Each one is called based on the name of the state,
        // so when currentState = Idle, we call Idle_EnterState. If currentState = Jump, we call
        // Jump_Update()


        void Idle_EnterState()
        {

        }

        void Idle_Update()
        {
            // Run every frame we are in the idle state

            if (input.current.JumpInput)
            {
                Debug.Log("Jump Pressed");
                currentState = PlayerStates.Jump;

                return;
            }

            if (input.current.MoveInput != Vector3.zero)
            {
                currentState = PlayerStates.Walk;
                return;
            }

            // Apply friction to slow us to a halt
            moveDirection = Vector3.MoveTowards(moveDirection, Vector3.zero, 10.0f * Time.deltaTime);
        }

        void Idle_ExitState()
        {
            // Run once when we exit the idle state
        }

        void Walk_Update()
        {
            if (input.current.JumpInput)
            {
                Debug.Log("Jump Pressed");
                currentState = PlayerStates.Jump;
                return;
            }

            if (input.current.MoveInput != Vector3.zero)
            {
                moveDirection = Vector3.MoveTowards(moveDirection, LocalMovement() * moveSpeed, moveAcceleration * Time.deltaTime);
            }
            else
            {
                currentState = PlayerStates.Idle;
                return;
            }
        }

        void Jump_EnterState()
        {
            Jump();
        }

        void Jump_Update()
        {
            if (grounded)
            {
                currentState = PlayerStates.Idle;
                return;
            }
        }

        void Fall_EnterState()
        {

        }

        void Fall_Update()
        {

        }

        void CheckIfPlayerGrounded() {

            //get the radius of the players capsule collider, and make it a tiny bit smaller than that
            float radius = capsuleCollider.radius * 0.9f;
            //get the position (assuming its right at the bottom) and move it up by almost the whole radius
            Vector3 pos = transform.position + Vector3.up * (radius * 0.9f);
            //returns true if the sphere touches something on that layer
            if (Physics.CheckSphere(pos, radius, groundLayer))
            {
                grounded = true;
            }
            else
            {

                grounded = false;
            }




        }

    }
        // Do not put code below this line. It wont work.
    }


