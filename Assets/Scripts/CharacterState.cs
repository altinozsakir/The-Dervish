using System.Collections.Generic;
using UnityEngine;
namespace the_dervish
{
    // This class extends Unity's built-in StateMachineBehaviour.
    // It allows you to run custom logic when entering, updating, or exiting an animation state.
    public class CharacterState : StateMachineBehaviour
    {
        // A list of abilities or logic blocks (like Attack, ForceTransition) to run during this state
        public List<StateData> ListAbilityData = new List<StateData>();

        // Calls UpdateAbility on all StateData (abilities) attached to this state
        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        // Unity calls this every frame while the animator is in this state
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        // Unity calls this once when the animator enters this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);
            }
        }

        // Unity calls this once when the animator exits this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);
            }
        }

        // Caches and returns the CharacterControl component associated with the character
        private CharacterControl characterControl;

        public CharacterControl GetCharacterControl(Animator animator)
        {
            // Only fetch once for performance
            if (characterControl == null)
            {
                // Assumes CharacterControl is attached to the same GameObject or a parent of the Animator
                characterControl = animator.GetComponentInParent<CharacterControl>();
            }

            return characterControl;
        }
    }
}
