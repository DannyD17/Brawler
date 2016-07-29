using UnityEngine;
using System.Collections;
using Brawler;
using RootMotion.FinalIK;


namespace Brawler
{
    public class PuppetActions : MonoBehaviour
    {

      // Target we want to hit
        public Transform target;
  
        public Transform pin;
        public FullBodyBipedIK ik;
        public AimIK aim;

        public float weight;

        private FullBodyBipedEffector rightHandEffector;
        private FullBodyBipedEffector leftHandEffector;

        public AnimationCurve aimWeight;

        public KeyCode rightPunch; // JoystickButton5 = Right Bumper
        public KeyCode leftPunch;  // JoystickButton4  = Left Bumper
        public KeyCode jumpKick;       // JoystickButton2 = X Button



        private GameObject aimDirectionTarget;
        private Animator anim;


        // Input states
           public struct State
        {
            public int rightPunch;
            public int leftPunch;
            public int jumpKick;
            public float aimDirectionV;
            public float aimDirectionH;
            public int currentAction;

        }


        public State state = new State();

        public void Start()
        {
            //Setting our references

            anim = GetComponent<Animator>();
            aimDirectionTarget = GameObject.Find("Aim Direction IK Target");
            rightHandEffector = FullBodyBipedEffector.RightHand;
            leftHandEffector = FullBodyBipedEffector.LeftHand;
        }


        public void Update()
        {

            // Check if currently performing an action

            state.currentAction = anim.GetInteger("CurrentAction");



            // Apply Right Punch

            state.rightPunch = Input.GetKey(rightPunch) ? 1 : 0;
            if (state.rightPunch == 1)
            {
               anim.SetLayerWeight(1, 1f);
               anim.SetInteger("RightPunch", 3);
               anim.SetFloat("RightWeight", 1f, 0.4f, Time.deltaTime);


            }

            // (Input.GetKeyUp(rightPunch))
            if (state.rightPunch == 0)
            {
                anim.SetInteger("RightPunch", 0);
                anim.SetFloat("RightWeight", 0f);
            }


            // Apply Left Punch

            state.leftPunch = Input.GetKey(leftPunch) ? 1 : 0;
           // (Input.GetKeyDown(leftPunch))
            if (state.leftPunch == 1)
            {
                anim.SetLayerWeight(1, 1f);
                anim.SetInteger("LeftPunch", 3);
                anim.SetFloat("LeftWeight", 1f, 0.4f, Time.deltaTime);
            }

            // (Input.GetKeyUp(leftPunch))
            if (state.leftPunch == 0)
            {
                anim.SetInteger("LeftPunch", 0);
                 anim.SetFloat("LeftWeight", 0f);
            }


            // Apply Jump Kick

           state.jumpKick = Input.GetKey(jumpKick) ? 1 : 0;
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

            state.aimDirectionV = Input.GetAxis("RightStickV");


            if (state.aimDirectionV < 0)
            {
             //   Debug.Log("Looking Up");
                aimDirectionTarget.transform.localPosition = new Vector3(0, 2, 1);
            }

            if (state.aimDirectionV == 0)
            {
             //   Debug.Log("Looking Middle");
                aimDirectionTarget.transform.localPosition = new Vector3(0, 1, 1);
            }

            if (state.aimDirectionV > 0)
            {
             //   Debug.Log("Looking Down");
                aimDirectionTarget.transform.localPosition = new Vector3(0, 0, 1);
            }


        }

        void LateUpdate()
        {
            
            // Getting the weight of pinning the fist to the target
            float rightWeight = anim.GetFloat("RightWeight");
            float leftWeight = anim.GetFloat("LeftWeight");



            // Pinning the first with FBIK
            ik.solver.GetEffector(rightHandEffector).position = target.position;
            ik.solver.GetEffector(rightHandEffector).positionWeight = rightWeight * weight;
            ik.solver.GetEffector(rightHandEffector).rotationWeight = rightWeight * weight;

            ik.solver.GetEffector(leftHandEffector).position = target.position;
            ik.solver.GetEffector(leftHandEffector).positionWeight = leftWeight * weight;
            ik.solver.GetEffector(leftHandEffector).rotationWeight = leftWeight * weight;

            // Aiming the body with AimIK to follow the target
            if (aim != null)
            {
                // Make the aim transform always look at the pin. This will normalize the default aim diretion to the animated pose.
                aim.solver.transform.LookAt(pin.position);

                // Set aim target
                aim.solver.IKPosition = target.position;

                // Setting aim weight
               //        aim.solver.IKPositionWeight = aimWeight.Evaluate(hitWeight) * weight;
           //     aim.solver.IKPositionWeight = aimWeight.Evaluate(rightWeight) * weight;
            //    aim.solver.IKPositionWeight = aimWeight.Evaluate(leftWeight) * weight;

           //     aim.solver.IKPositionWeight = rightWeight;
           //     aim.solver.IKPositionWeight = leftWeight;


            }

            //if (rightWeight >= 0.38f)
            //{
            //    anim.SetFloat("RightWeight", 0f);
            //}

            //if (leftWeight >= 0.38f)
            //{
            //    anim.SetFloat("LeftWeight", 0f);
            }

        }

    }






