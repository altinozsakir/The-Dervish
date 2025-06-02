using Unity.VisualScripting;
using UnityEngine;




namespace the_dervish
{

    public class VirtualInputManager : SingletonMonoBehaviour<VirtualInputManager>
    {
        public bool MoveRight;
        public bool MoveLeft;

        public bool Jump;

        public bool Attack;
    }
}
