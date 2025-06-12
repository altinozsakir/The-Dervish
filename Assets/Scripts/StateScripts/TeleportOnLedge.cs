using UnityEngine;


namespace the_dervish{

    [CreateAssetMenu(fileName ="New State", menuName = "TheDervish/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);
            Vector3 endPosition = control.ledgeChecker.GrabbedLedge.transform.position + control.ledgeChecker.GrabbedLedge.EndPosition;

            control.transform.position = endPosition;
            control.SkinnedMeshAnimator.transform.position = endPosition;
            control.SkinnedMeshAnimator.transform.parent = control.transform;

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }


}

