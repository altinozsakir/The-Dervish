using System.Collections.Generic;
using UnityEngine;


namespace the_dervish{


    public enum TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        JUMP,
        ATTACK,
        GRABBING_LEDGE

    }

    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/TransitionIdexer")]
    public class TransitionIdexer : StateData
    {

        public int Index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (MakeTransition(control))
            {
                animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), Index);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {


        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (MakeTransition(control))
            {
                animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), Index);
            }

        }

        private bool MakeTransition(CharacterControl control)
        {
            foreach (TransitionConditionType c in transitionConditions)
            {
                switch (c)
                {
                    case TransitionConditionType.UP:
                        {
                            if (!control.MoveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOWN:
                        {
                            if (!control.MoveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT:
                        {
                            if (!control.MoveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.RIGHT:
                        {
                            if (!control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK:
                        {
                            if (!control.Attack)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.JUMP:
                        {
                            if (!control.Jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GRABBING_LEDGE:
                        {
                            if (!control.ledgeChecker.IsGrabbingLedge)
                            {
                                return false;
                            }

                        }
                        break;


                }
            }

            return true;
        }
    }
}




