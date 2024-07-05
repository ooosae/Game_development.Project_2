using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Space] 
        [Header("Player")] 
        [SerializeField] private float _playerSpeedIncreasePerPoint;
        [SerializeField] private float _playerMaxSpeed;
        [Header("Defeat")] 
        [SerializeField] private float _defeatPanelDuration;

        // Player
        public float PlayerSpeedIncreasePerPoint => _playerSpeedIncreasePerPoint;
        public float PlayerMaxSpeed => _playerMaxSpeed;
        
        // Defeat
        public float DefeatPanelDuration => _defeatPanelDuration;
    }
}
