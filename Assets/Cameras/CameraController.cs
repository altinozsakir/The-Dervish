using UnityEngine;

namespace the_dervish
{

    public enum CameraTrigger
    {
        Default,
        Shake
    }


    public class CameraController : MonoBehaviour
    {
        private Animator animator;
        public Animator ANIMATOR
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }


                return animator;
            }
        }

        
        public void TriggerCamera(CameraTrigger trigger) {
            ANIMATOR.SetTrigger(trigger.ToString());
        }

    }



}

