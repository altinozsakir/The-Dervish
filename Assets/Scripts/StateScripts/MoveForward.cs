using Unity.VisualScripting;
using UnityEngine;



namespace the_dervish{


    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {

        public bool Constant;
        public float Speed = 2f;
        public float blockDistance;
        public AnimationCurve SpeedGraph;



        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl characterControl = characterState.GetCharacterControl(animator);

            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
                return;
            }

            if (Constant)
            {
                ConstantMove(characterControl, animator, animatorStateInfo);
            }
            else
            {
                ControlledMove(characterControl, animator, animatorStateInfo);
            }


        }


        private void ControlledMove(CharacterControl characterControl, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (characterControl.MoveLeft && characterControl.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!characterControl.MoveLeft && !characterControl.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }


            if (characterControl.MoveRight)
            {
                characterControl.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!checkFront(characterControl))
                {
                    characterControl.MoveForward(Speed, SpeedGraph.Evaluate(animatorStateInfo.normalizedTime));
                }


            }

            if (characterControl.MoveLeft)
            {
                characterControl.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!checkFront(characterControl))
                {
                    characterControl.MoveForward(Speed, SpeedGraph.Evaluate(animatorStateInfo.normalizedTime));
                }

            }
        }

        private void ConstantMove(CharacterControl characterControl, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (!checkFront(characterControl))
            {
                characterControl.MoveForward(Speed, SpeedGraph.Evaluate(animatorStateInfo.normalizedTime));
            }
        }

        bool checkFront(CharacterControl control)
        {

            foreach (GameObject o in control.FrontSpheres)
            {
                RaycastHit hit;
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.2f, Color.yellow);

                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, blockDistance))
                {

                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider))
                        {
                            return true;
                        }
                        
                    }
                }


            }

            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (control == null)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }
            if (control.RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }

}

