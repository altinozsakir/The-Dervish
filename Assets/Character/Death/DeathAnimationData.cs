using System.Collections.Generic;
using UnityEngine;


namespace the_dervish
{

    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "TheDervish/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {

        public List<GeneralBodyPart> GeneralBodyParts = new List<GeneralBodyPart>();
        public RuntimeAnimatorController Animator;
        public bool IsFacingAttacker;
    }



}

