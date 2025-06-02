using UnityEngine;
using UnityEngine.Video;


namespace the_dervish{

    [CreateAssetMenu(fileName ="New State", menuName = "TheDervish/AbilityData/Jump")]
    public class Jump : StateData
    {
        [SerializeField]
        public float JumpForce;
        public AnimationCurve Gravity;
        public AnimationCurve Pull;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce );
            animator.SetBool(TransitionParameter.Grounded.ToString(),false);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
        }
    }


}

