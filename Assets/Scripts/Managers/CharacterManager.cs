using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace the_dervish
{
    public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
    {


        public List<CharacterControl> Characters = new List<CharacterControl>();



        public CharacterControl GetCharacter(Animator animator)
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.SkinnedMeshAnimator == animator)
                {
                    return control;
                }
            }

            return null;
        }

        public CharacterControl GetCharacter(GameObject obj)
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.gameObject == obj)
                {
                    return control;
                }
            }

            return null;
        }

      
    }

}

