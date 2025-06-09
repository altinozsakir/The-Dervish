using UnityEngine;



namespace the_dervish
{

    public interface IInteractable
    {

        float HoldDuration { get; }
        bool HoldInteract { get; }

        bool MultipleUse { get; }

        bool IsInteractable { get;  }
        void OnInteract();
    }



}

