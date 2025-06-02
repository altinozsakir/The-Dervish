using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;


namespace the_dervish
{

    public enum GeneralBodyPart
    {
        Upper,
        Lower,
        Arm,
        Leg,
    }
    public class TriggerDetector : MonoBehaviour
    {
        public GeneralBodyPart generalBodyPart;

        private CharacterControl owner;
        public List<Collider> CollidingParts = new List<Collider>();

        private void Awake()
        {
            owner = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (owner.RagdollParts.Contains(col))
            {
                return;
            }

            CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

            if (attacker == null)
            {
                return;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            if (!CollidingParts.Contains(col))
            {
                CollidingParts.Add(col);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (CollidingParts.Contains(col))
            {
                CollidingParts.Remove(col);
            }
        }

    }



}
