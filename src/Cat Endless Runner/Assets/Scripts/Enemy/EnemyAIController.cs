using UnityEngine;

namespace Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;

        protected void Awake()
        {
            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            _stateMachine = new EnemyStateMachine(enemyDirectionController);
        }

        protected void Update()
        {
            _stateMachine.Update();
        }
    }
}
