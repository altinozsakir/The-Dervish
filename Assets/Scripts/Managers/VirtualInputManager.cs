using Unity.VisualScripting;
using UnityEngine;




namespace the_dervish
{

    public class VirtualInputManager : SingletonMonoBehaviour<VirtualInputManager>
    {
        public bool MoveRight;
        public bool MoveLeft;
        
        public bool MoveUp;
        public bool MoveDown;

        public bool Jump;

        public bool Attack;
    }
}
