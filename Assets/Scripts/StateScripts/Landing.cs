using UnityEngine;


namespace the_dervish{

    [CreateAssetMenu(fileName ="New State", menuName = "TheDervish/AbilityData/Landing")]
    public class Landing : StateData
    {


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(),false);
            animator.SetBool(TransitionParameter.Move.ToString(), false);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }


}

