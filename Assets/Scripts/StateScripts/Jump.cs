using UnityEngine;

namespace the_dervish
{
    // Adds a Jump ability to be used in the animator's state machine
    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/Jump")]
    public class Jump : StateData
    {
        // Force applied upward when the jump starts
        [SerializeField]
        public float JumpForce;


        // Delay for jump 
        [Range(0f, 1f)]
        public float JumpTiming;

        bool isJumped;

        // Curve to control upward pull strength over time
        public AnimationCurve Pull;

        // Called once when the animator enters the jump state
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (JumpTiming == 0f)
            {
                // Apply upward jump force
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
                isJumped = true;
            }
            

            // Set grounded flag to false — prevents the player from jumping again mid-air
            animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        // Called once when the animator exits the jump state
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            // Reset pull multiplier to 0 (no upward force when jump ends)
            control.PullMultiplier = 0f;
            isJumped = false;
        }

        // Called every frame while in the jump state
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            // Adjust vertical pull force using the animation curve based on how far the animation has progressed
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);

            if (!isJumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
                isJumped = true;
            }
        }
    }
}
