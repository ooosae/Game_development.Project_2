using Player;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _distanceText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private PlayerController _playerController;

        private const float IncrementInterval = 0.1f;

        private int _score = 0;
        private int _distance = 0;
        private float _timer = 0f;

        public void IncrementScore()
        {
            _score++;
            _scoreText.text = "SCORE: " + _score;
            _playerController.IncreaseSpeed();
        }

        public void IncrementScore(int value)
        {
            _score += value;
            _scoreText.text = "SCORE: " + _score;
            _playerController.IncreaseSpeed(value);
        }

        private void IncrementDistance()
        {
            _distance++;
            _distanceText.text = "DISTANCE: " + _distance;
        }

        public void DoBoost()
        {
            _playerController.Boost();
        }

        private void Update()
        {
            if (!_playerController.IsAlive)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (!(_timer >= IncrementInterval))
            {
                return;
            }

            IncrementDistance();
            _timer = 0f;
        }

        public int GetDistance()
        {
            return _distance;
        }
    }
}