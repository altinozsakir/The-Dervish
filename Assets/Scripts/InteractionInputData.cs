using UnityEngine;



namespace the_dervish
{

    [CreateAssetMenu(fileName = "InteractionInputData", menuName = "TheDervish/InteractionSystem/InputData")]
    public class InteractionInputData : ScriptableObject
    {

        private bool m_interactedTriggered;

        private bool m_interactedRelease;

        public bool InteractedTriggered
        {
            get => m_interactedTriggered;
            set => m_interactedTriggered = value;
        }

        public bool InteractedRelease
        {
            get => m_interactedRelease;
            set => m_interactedRelease = value;
        }

        public void Reset()
        {
            m_interactedRelease = false;
            m_interactedTriggered = false;
        }

    }





}