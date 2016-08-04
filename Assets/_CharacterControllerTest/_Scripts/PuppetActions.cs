using UnityEngine;
using System.Collections;
using Brawler;
using RootMotion.FinalIK;
using InControl;


namespace Brawler
{
    public class PuppetActions : MonoBehaviour
    {

        // Target we want to hit




        public float weight;


        public Rigidbody[] rb;
        public float startMass = .5f;
        public float swingMass = 20f;


        public KeyCode rightPunch; // JoystickButton5 = Right Bumper
        public KeyCode leftPunch;  // JoystickButton4  = Left Bumper
        public KeyCode jumpKick;       // JoystickButton2 = X Button




        private Animator anim;
        private int playerNum;


        // Input states
        public struct State
        {
            public int rightPunch;
            public int leftPunch;
            public int jumpKick;
            public int currentAction;

        }


        public State state = new State();

        public void Start()
        {
            //Setting our references

            anim = GetComponent<Animator>();

            SoftHandWeight();

            playerNum = GetComponentInParent<BaseCharacterController>().playerNum;
        }


        public void Update()
        {

            // Check if currently performing an action

            state.currentAction = anim.GetInteger("CurrentAction");

            var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

            // Apply Right Punch

            //state.rightPunch = Input.GetKey(rightPunch) ? 1 : 0;
            state.rightPunch = inputDevice.RightTrigger ? 1 : 0;
            if (state.rightPunch == 1)
            {
                anim.SetLayerWeight(1, 1f);
                anim.SetInteger("RightPunch", 3);
                anim.SetFloat("RightWeight", 1f, 0.4f, Time.deltaTime);


            }

            // (Input.GetKeyUp(rightPunch))
            if (state.rightPunch == 0  && state.leftPunch != 1)
            {
                anim.SetInteger("RightPunch", 0);
                anim.SetFloat("RightWeight", 0f);
                SoftHandWeight();
            }


            // Apply Left Punch

            //state.leftPunch = Input.GetKey(leftPunch) ? 1 : 0;
            state.leftPunch = inputDevice.LeftTrigger ? 1 : 0;
            // (Input.GetKeyDown(leftPunch))
            if (state.leftPunch == 1)
            {
                anim.SetLayerWeight(1, 1f);
                anim.SetInteger("LeftPunch", 3);
                anim.SetFloat("LeftWeight", 1f, 0.4f, Time.deltaTime);
            }

            // (Input.GetKeyUp(leftPunch))
            if (state.leftPunch == 0 && state.rightPunch != 1)
            {
                anim.SetInteger("LeftPunch", 0);
                anim.SetFloat("LeftWeight", 0f);
                SoftHandWeight();
            }


            // Apply Jump Kick

            //state.jumpKick = Input.GetKey(jumpKick) ? 1 : 0;
            state.jumpKick = inputDevice.RightBumper.WasPressed ? 1 : 0;
            //(Input.GetKeyDown(jumpKick))
            if (state.jumpKick == 1)
            {
                anim.SetLayerWeight(2, 1f);
                anim.SetInteger("Kick", 3);
            }

            // (Input.GetKeyUp(jumpKick))
            if (state.jumpKick == 0)
            {
                anim.SetInteger("Kick", 0);
            }


            // Apply Aim Direction with Right Stick Vertical. Goes back to Middle when in DeadZone. Middle(0) - Up(1) - Down(-1)

            //state.aimDirectionV = Input.GetAxis("RightStickV");




        }

        void LateUpdate()
        {

            // Getting the weight of pinning the fist to the target
            float rightWeight = anim.GetFloat("RightWeight");
            float leftWeight = anim.GetFloat("LeftWeight");



        }

        void HeavyHandWeight()
        {
            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].mass = swingMass;
            }
        }

        void SoftHandWeight()
        {
            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].mass = startMass;
            }
        }
    }
}





