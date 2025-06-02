using UnityEngine;


namespace the_dervish
{
    public class CameraState : StateMachineBehaviour
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CameraTrigger[] arr = System.Enum.GetValues(typeof(CameraTrigger)) as CameraTrigger[];


            foreach (CameraTrigger t in arr)
            {
                CameraManager.Instance.CAM_CONTROLLER.ANIMATOR.ResetTrigger(t.ToString());
            }
        }
    }


}

