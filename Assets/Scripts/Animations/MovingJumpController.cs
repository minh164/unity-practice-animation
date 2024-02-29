using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class MovingJumpController : AnimationManager
    {
        public float jumpForce = 40f;

        private Rigidbody rigid;
        private bool isJumping;
        private int isJumpingHash;
        private bool jumpPressed;

        // Start is called before the first frame update
        void Start()
        {
            rigid = gameObject.GetComponent<Rigidbody>();
            isJumpingHash = Animator.StringToHash("isJumping");
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Update is called once per fixed delta time (0.02).
        void FixedUpdate()
        {
            isJumping = gameObject.GetComponent<Animator>().GetBool(isJumpingHash);
            jumpPressed = Input.GetButton("Jump");

            if (jumpPressed && ! isJumping) {
                OnJumping();
            }

            
            if (! jumpPressed && isJumping) {
                OutJumping();
            }
        }

        /// <summary>
        /// If state is grounded, switch state is jumping.
        /// </summary>
        private void OnJumping()
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            gameObject.GetComponent<Animator>().SetBool(isJumpingHash, true);
        }

        /// <summary>
        /// If state is jumping, switch state is grounded.
        /// NOTICE: Transition in here need to be set HasExitTime, because Jump animation is long,
        /// it need a time to be finished, after that Grounded transition is triggered.
        /// </summary>
        private void OutJumping()
        {
            gameObject.GetComponent<Animator>().SetBool(isJumpingHash, false);
        }
    }
}