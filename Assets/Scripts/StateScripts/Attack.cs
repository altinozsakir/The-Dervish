using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace the_dervish{
    // Allows creation of this ScriptableObject from Unity's asset menu.
    [CreateAssetMenu(fileName = "New State", menuName = "TheDervish/AbilityData/Attack")]
    public class Attack : StateData
    {
        // Time range (0-1 normalized) in the animation when the attack is active
        public float StartAttackTime;
        public float EndAttackTime;


        public bool debug;
        // Names of colliders (e.g. fists, weapons) used in the attack
        public List<string> ColliderNames = new List<string>();

        public bool MustCollide;

        public bool LaunchIntoAir;

        public bool MustFaceAttacker;

        public float LethalRange;

        public int MaxHits;




        private List<AttackInfo> FinishedAttacks = new List<AttackInfo>();




        // Called when entering the attack state
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            // Reset attack transition parameter
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            // Get an AttackInfo object from the object pool
            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            // Reactivate and initialize the object
            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator));
            // Register the attack in the global AttackManager if not already present
            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            ClearAttack(); // Cleanup finished attacks
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);   // Activate attack
            DeregisterAttack(characterState, animator, stateInfo); // Deactivate when time's up
            CheckCombo(characterState, animator, stateInfo);       // See if combo input was triggered

        }

        // Registers the attack during its active time window
        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null) continue;

                    // Register only unregistered attacks tied to this ability
                    if (!info.isRegistered && info.AttackAbility == this)
                    {
                        if (debug)
                        {
                            Debug.Log(this.name + " registired: " + stateInfo.normalizedTime);
                        }
                        info.Register(this);
                    }
                }
            }
        }

       // Deregisters (finishes) the attack after the active time
        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (EndAttackTime <= stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null) continue;

                    // Mark as finished and return to object pool
                    if (!info.isFinished && info.AttackAbility == this)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();

                        if (debug)
                        {
                            Debug.Log(this.name + " registired: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

     // Clears finished attacks from the global AttackManager
        public void ClearAttack()
        {
            FinishedAttacks.Clear(); // Clear local list first

            // Add finished attacks to the list
            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null || info.AttackAbility == this)
                {
                    FinishedAttacks.Add(info);
                }
            }

            // Remove them from the global list
            foreach (AttackInfo info in FinishedAttacks)
            {
                if (AttackManager.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }
        }

        // Checks for combo input during a specific phase of the attack
        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            // Check if the animation is in the middle phase of the attack
            if (stateInfo.normalizedTime >= StartAttackTime + ((EndAttackTime - StartAttackTime) / 3f))
            {
                if (stateInfo.normalizedTime < EndAttackTime)
                {
                    // Get character control script
                    CharacterControl characterControl = characterState.GetCharacterControl(animator);

                    // If attack input is given again, trigger combo
                    if (characterControl.Attack)
                    {
                        Debug.Log("combo attack");
                        animator.SetBool(TransitionParameter.Attack.ToString(), true);
                    }
                }
            }
        }
        
    }


}