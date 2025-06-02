using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


namespace the_dervish
{
    public class PoolObject : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public PoolObjectType poolObjectType;
        public float ScheduledOffTime;
        private Coroutine OffRoutine;
        public void TurnOff()
        {
            PoolManager.Instance.AddObject(this);
        }

        private void OnEnable()
        {
            if (OffRoutine != null)
            {
                StopCoroutine(OffRoutine);
            }
            if (ScheduledOffTime >= 0f)
            {
                OffRoutine = StartCoroutine(_ScheduledOff());
            }
            
        }
        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(ScheduledOffTime);

            if (!PoolManager.Instance.PoolDictionary[poolObjectType].Contains(this.gameObject)){
                TurnOff();
                
            }

        }
    }
}

