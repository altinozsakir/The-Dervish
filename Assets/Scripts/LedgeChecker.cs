using UnityEngine;


namespace the_dervish
{

    public class LedgeChecker : MonoBehaviour
    {

        Ledge checkedledge = null;
        [SerializeField] public bool IsGrabbingLedge;
        [SerializeField] public Ledge GrabbedLedge;

        private void OnTriggerEnter(Collider other)
        {
            checkedledge = other.gameObject.GetComponent<Ledge>();
            if (checkedledge != null)
            {
                IsGrabbingLedge = true;
                GrabbedLedge = checkedledge;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            checkedledge = other.gameObject.GetComponent<Ledge>();
            if (checkedledge != null)
            {
                IsGrabbingLedge = false;
                // GrabbedLedge = null;
            }
            
        }

    }

}

