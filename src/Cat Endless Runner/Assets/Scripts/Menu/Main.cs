using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Main : MonoBehaviour
    {
        public void LoadNextScene()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}