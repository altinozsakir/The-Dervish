using UnityEngine;



namespace the_dervish{


    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/ForceTransition")]
    public class ForceTransition : StateData
    {
        
        [Range(0.0f,1f)]
        public float TransitionTiming;



        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameter.ForceTransition.ToString(),false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl characterControl = characterState.GetCharacterControl(animator);

            if(animatorStateInfo.normalizedTime <= TransitionTiming){
                animator.SetBool(TransitionParameter.ForceTransition.ToString(),true);
            }
        }
    }

}

