using Constants;
using UnityEngine;

namespace Animations
{
    public abstract class AnimationManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Get is grounded state.
        /// </summary>
        /// <returns></returns>
        protected bool GetGroundedState()
        {
            return gameObject.GetComponent<Animator>().GetBool(AnimationParams.IS_GROUNDED);
        }

        /// <summary>
        /// Set is grounded state.
        /// </summary>
        /// <param name="value"></param>
        protected void SetGroundedState(bool value)
        {
            gameObject.GetComponent<Animator>().SetBool(AnimationParams.IS_GROUNDED, value);            
        }

        /// <summary>
        /// Get is walking param.
        /// </summary>
        /// <returns></returns>
        protected bool GetWalkingState()
        {
            return gameObject.GetComponent<Animator>().GetBool(AnimationParams.IS_WALKING);
        }

        /// <summary>
        /// Set walking param.
        /// </summary>
        /// <param name="value"></param>
        protected void SetWalkingState(bool value)
        {
            gameObject.GetComponent<Animator>().SetBool(AnimationParams.IS_WALKING, value);
        }

        /// <summary>
        /// Get is running param.
        /// </summary>
        /// <returns></returns>
        protected bool GetRunningState()
        {
            return gameObject.GetComponent<Animator>().GetBool(AnimationParams.IS_RUNNING);
        }

        /// <summary>
        /// Set running param.
        /// </summary>
        /// <param name="value"></param>
        protected void SetRunningState(bool value)
        {
            gameObject.GetComponent<Animator>().SetBool(AnimationParams.IS_RUNNING, value);
        }

        /// <summary>
        /// Get is jumping up param.
        /// </summary>
        /// <returns></returns>
        protected bool GetJumpingUpState()
        {
            return gameObject.GetComponent<Animator>().GetBool(AnimationParams.IS_JUMPING_UP);
        }

        /// <summary>
        /// Set jumping up param.
        /// </summary>
        /// <param name="value"></param>
        protected void SetJumpingUpState(bool value)
        {
            gameObject.GetComponent<Animator>().SetBool(AnimationParams.IS_JUMPING_UP, value);
        }


        /// <summary>
        /// Get is jumping down param.
        /// </summary>
        /// <returns></returns>
        protected bool GetJumpingDownState()
        {
            return gameObject.GetComponent<Animator>().GetBool(AnimationParams.IS_JUMPING_DOWN);
        }

        /// <summary>
        /// Set jumping down param.
        /// </summary>
        /// <param name="value"></param>
        protected void SetJumpingDownState(bool value)
        {
            gameObject.GetComponent<Animator>().SetBool(AnimationParams.IS_JUMPING_DOWN, value);
        }
    }
}