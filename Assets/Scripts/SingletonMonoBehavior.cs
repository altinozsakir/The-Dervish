using UnityEngine;


namespace the_dervish
{

    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // _instance = (T)FindAnyObjectByType(typeof(T));
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = typeof(T).ToString();

                }

                return _instance;
            }
        }
    }


}

