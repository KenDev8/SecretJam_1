using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KenDev
{
    public class MenuManager : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.PlayGraveYardAmbient();
        }

        public void StartPress()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitPress()
        {
            Application.Quit();
        }
    }
}
