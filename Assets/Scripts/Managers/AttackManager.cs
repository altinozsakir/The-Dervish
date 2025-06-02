using System.Collections.Generic;
using UnityEngine;



namespace the_dervish
{
    public class AttackManager : SingletonMonoBehaviour<AttackManager>
    {

        public List<AttackInfo> CurrentAttacks = new List<AttackInfo>();
    }
}


