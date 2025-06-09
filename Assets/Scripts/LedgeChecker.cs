using UnityEngine;


namespace the_dervish
{

    public class LedgeChecker : MonoBehaviour
    {

        Ledge ledge = null;
        [SerializeField] public bool IsGrabbingLedge;

        private void OnTriggerEnter(Collider other)
        {
            ledge = other.gameObject.GetComponent<Ledge>();
            if (ledge != null)
            {
                IsGrabbingLedge = true;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            ledge = other.gameObject.GetComponent<Ledge>();
            if (ledge != null)
            {
                IsGrabbingLedge = false;
            }
            
        }

    }

}

