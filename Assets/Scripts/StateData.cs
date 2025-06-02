using UnityEngine;




namespace the_dervish{
    public abstract class StateData : ScriptableObject
    {

        public abstract void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo);

        public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo);

        public abstract void OnExit(CharacterState characterState, Animator animator,  AnimatorStateInfo animatorStateInfo);
    }
}


