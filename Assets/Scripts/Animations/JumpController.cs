using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class JumpController : MonoBehaviour
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

            // If state is grounded, switch state is jumping.
            if (jumpPressed && ! isJumping) {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                gameObject.GetComponent<Animator>().SetBool(isJumpingHash, true);
            }

            // If state is jumping, switch state is grounded.
            // NOTICE: Transition in here need to be set HasExitTime, because Jump animation is long,
            // it need a time to be finished, after that Grounded transition is triggered.
            if (! jumpPressed && isJumping) {
                gameObject.GetComponent<Animator>().SetBool(isJumpingHash, false);
            }
        }
    }
}