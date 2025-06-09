using TMPro;
using UnityEngine;



namespace the_dervish
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.MoveRight = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveRight = false;
            }


            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.MoveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveLeft = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Jump = true;
            }
            else
            {
                VirtualInputManager.Instance.Jump = false;
            }

            if (Input.GetKey(KeyCode.R))
            {
                VirtualInputManager.Instance.Attack = true;
            }
            else
            {
                VirtualInputManager.Instance.Attack = false;
            }
            if (Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.Instance.MoveUp = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveUp = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveDown = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveDown = false;
            }
            
        }
    }

}

