using UnityEngine;


namespace the_dervish{

    [CreateAssetMenu(fileName ="New State", menuName = "TheDervish/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StateData
    {


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.SkinnedMeshAnimator.gameObject;
            anim.transform.parent = control.ledgeChecker.GrabbedLedge.transform;
            anim.transform.localPosition = control.ledgeChecker.GrabbedLedge.Offset;

            control.RIGID_BODY.linearVelocity = Vector3.zero;

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {


        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }


}

