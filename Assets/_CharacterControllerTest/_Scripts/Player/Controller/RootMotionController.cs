using UnityEngine;

namespace Brawler
{


    // Helper component to get 'Animator' root-motion velocity vector (animVelocity).
    // This must be attached to the game object with the 'Animator' component.

    public sealed class RootMotionController : MonoBehaviour
    {


        private Animator _animator;



        // The animation velocity vector.


        public Vector3 animVelocity { get; private set; }


        public void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
            {
                Debug.LogError(
                    string.Format(
                        "RootMotionController: There is no 'Animator' attached to the {0} game object.\nYou need to attach a 'Animator' to the game object {0}",
                        name));
            }
        }

        public void OnAnimatorMove()
        {
            // Compute movement velocity from animation

            var deltaTime = Time.deltaTime;
            if (deltaTime <= 0.0f)
                return;

            animVelocity = _animator.deltaPosition / deltaTime;
        }

    }
}