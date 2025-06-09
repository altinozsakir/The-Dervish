using System.Collections.Generic;
using UnityEngine;


namespace the_dervish{



    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/ToggleGravity")]
    public class ToggleGravity : StateData
    {

        public bool On;
        public bool OnStart;

        public bool OnEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

            if (OnStart)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleGrav(control);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (OnEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleGrav(control);
            }

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {


        }

        private void ToggleGrav(CharacterControl control)
        {
            control.RIGID_BODY.useGravity = On;
            control.RIGID_BODY.linearVelocity = Vector3.zero;
        }

            
    }
}




