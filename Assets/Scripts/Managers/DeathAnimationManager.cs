using System.Collections.Generic;
using System.Collections;
using UnityEngine;


namespace the_dervish
{

    public class DeathAnimationManager : SingletonMonoBehaviour<DeathAnimationManager>
    {
        DeathAnimationLoader deathAnimationLoader;
        List<RuntimeAnimatorController> Candidates = new List<RuntimeAnimatorController>();

        void SetupDeathAnimationLoader()
        {
            if (deathAnimationLoader == null)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                deathAnimationLoader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GeneralBodyPart generalBodyPart)
        {
            SetupDeathAnimationLoader();

            Candidates.Clear();

            foreach (DeathAnimationData data in deathAnimationLoader.DeathAnimationDataList)
            {
                foreach (GeneralBodyPart p in data.GeneralBodyParts)
                {
                    if (p == generalBodyPart)
                    {
                        Candidates.Add(data.Animator);
                        break;
                    }
                }
            }

            return Candidates[Random.Range(0, Candidates.Count)];
        }

    }




}


