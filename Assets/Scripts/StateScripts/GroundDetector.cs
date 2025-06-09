using UnityEngine;


namespace the_dervish{

    [CreateAssetMenu(fileName ="New State", menuName = "TheDervish/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        public float Distance;
        [Range(0.01f,1.0f)]
        public float CheckTime;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {


        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

                if(stateInfo.normalizedTime >= CheckTime){
                    if(IsGorunded(control)){
                        animator.SetBool(TransitionParameter.Grounded.ToString(),true);
                    }else{
                        animator.SetBool(TransitionParameter.Grounded.ToString(),false);
                    }

                }
        }


        bool IsGorunded(CharacterControl control){
            if(control.RIGID_BODY.linearVelocity.y > -0.01f && control.RIGID_BODY.linearVelocity.y <= 0f){
                return true;
            }

            foreach(GameObject o in control.BottomSpheres){
                RaycastHit hit;
                Debug.DrawRay(o.transform.position, - Vector3.up * 0.3f, Color.yellow);

                if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, Distance))
                {
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        return true;
                    }
                    
                    
                }


            }

            return false;
        }
    }


}

