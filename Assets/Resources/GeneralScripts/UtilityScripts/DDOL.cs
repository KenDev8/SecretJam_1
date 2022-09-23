using UnityEngine;

namespace KenDev
{
    public class DDOL : MonoBehaviour
    {
        private static GameObject _instance;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance == null)
                _instance = gameObject;
            else
                Destroy(_instance);
        }
    }
}
