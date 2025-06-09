using UnityEngine;



// Declare a namespace to organize the code under "the_dervish"
namespace the_dervish
{
    // This attribute allows you to create a new instance of this scriptable object from Unity's "Create" menu.
    // It will appear under: Create > TheDervish > AbilityData > ForceTransition
    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/ForceTransition")]
    
    // 'ForceTransition' is a ScriptableObject derived class that likely defines behavior for a state transition.
    public class ForceTransition : StateData
    {
        // A public float variable that can be edited in the Unity Inspector, limited between 0.0 and 1.0.
        // It determines at what point in the animation (as a percentage) to force a transition.
        [Range(0.0f,1f)]
        public float TransitionTiming;

        // Called when the state this script is attached to is entered.
        // Currently empty – you could initialize or trigger something here.
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        // Called when the state is exited.
        // This resets the "ForceTransition" parameter in the Animator to false.
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameter.ForceTransition.ToString(), false);
        }

        // Called every frame while in this state.
        // If the current animation time (normalized between 0 and 1) has reached the transition point,
        // it sets the "ForceTransition" parameter to true, triggering a transition.
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.normalizedTime >= TransitionTiming)
            {
                animator.SetBool(TransitionParameter.ForceTransition.ToString(), true);
            }
        }
    }
}


