using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        public void OnPlay()
        {
            SceneManager.LoadScene("level1");
        }

        public void OnQuit()
        {
            Application.Quit();
        }
    }
}
