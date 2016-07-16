using UnityEngine;
using System.Collections;

namespace Brawler
{
    public class InputPlayer : MonoBehaviour
    {

        public PlayerInput current;



        void Start()
        {
            current = new PlayerInput();
        }

        // Update is called once per frame
        void Update()
        {

            // Retrieve our current WASD or Arrow Key input
            // Using GetAxisRaw removes any kind of gravity or filtering being applied to the input
            // Ensuring that we are getting either -1, 0 or 1
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            



            bool jumpInput = Input.GetButtonDown("Jump");

            current = new PlayerInput()
            {
                MoveInput = moveInput,
                JumpInput = jumpInput
            };
        }
    }

    public struct PlayerInput
    {
        public Vector3 MoveInput;
        public bool JumpInput;
    }
}