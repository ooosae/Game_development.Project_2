using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Defeat : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        
        private void Start()
        {
            Invoke(nameof(MainScene), _gameSettings.DefeatPanelDuration);
        }

        private void MainScene()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
