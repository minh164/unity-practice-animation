using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Animations
{
    public class JumpController : AnimationManager
    {
        public float jumpForce = 40f;

        private Rigidbody rigid;
        private bool isJumpingUp;
        private bool isJumpingDown;
        private bool jumpPressed;

        // Start is called before the first frame update
        void Start()
        {
            rigid = gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Update is called once per fixed delta time (0.02).
        void FixedUpdate()
        {
            isJumpingUp = GetJumpingUpState();
            isJumpingDown = GetJumpingDownState();
            jumpPressed = Input.GetButton("Jump");

            if (jumpPressed && ! isJumpingUp && ! isJumpingDown && IsGrounded()) {
                OnJumpingUp();
            }

            
            if (isJumpingUp) {
                OnJumpingDown();
            }

            if (IsGrounded() && isJumpingDown) {
                SetJumpingDownState(false);
            }
        }

        /// <summary>
        /// If state is grounded, switch state is jumping.
        /// </summary>
        private void OnJumpingUp()
        {
            SetJumpingUpState(true);
            if (GetJumpingUpState()) {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        /// <summary>
        /// If state is jumping, switch state is grounded.
        /// NOTICE: Transition in here need to be set HasExitTime, because Jump animation is long,
        /// it need a time to be finished, after that Grounded transition is triggered.
        /// </summary>
        private void OnJumpingDown()
        {
            SetJumpingDownState(true);
            SetJumpingUpState(false);
        }
    }
}