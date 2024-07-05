using Menu;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyCharacter : MonoBehaviour
    {
        private float _currentSpeed;
        private EnemyDirectionController _directionController;
        private EnemyStateMachine _stateMachine;
        private GameManager _gameManager;

        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _directionController = GetComponent<EnemyDirectionController>();

            _stateMachine = new EnemyStateMachine(_directionController);
        }

        private void Update()
        {
            _stateMachine.Update();
            if (!(transform.position.y < -2)) return;
            
            _gameManager.IncrementScore(5);
            Destroy(gameObject);
        }
    }
}