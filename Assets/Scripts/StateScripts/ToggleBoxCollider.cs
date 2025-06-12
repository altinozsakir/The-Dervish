using System.Collections.Generic;
using UnityEngine;


namespace the_dervish{



    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData
    {

        public bool On;
        public bool OnStart;

        public bool OnEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

            if (OnStart)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleBoxCol(control);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (OnEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleBoxCol(control);
            }

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {


        }

        private void ToggleBoxCol(CharacterControl control)
        {

            control.GetComponent<BoxCollider>().enabled = On;
            control.RIGID_BODY.linearVelocity = Vector3.zero;
        }

            
    }
}




