using UnityEngine;


namespace the_dervish{

    [CreateAssetMenu(fileName ="New State", menuName = "TheDervish/AbilityData/Idle")]
    public class Idle : StateData
    {


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl characterControl = characterState.GetCharacterControl(animator);
            if (characterControl.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            if (characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            if (characterControl.Attack)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }
        }
    }


}

