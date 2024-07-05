using FSM;

namespace Enemy.States
{
    public class AvoidObstacleState : BaseState
    {
        private readonly EnemyDirectionController _enemyDirectionController;

        public AvoidObstacleState(EnemyDirectionController enemyDirectionController)
        {
            _enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            if (_enemyDirectionController.IsObstacleAhead())
            {
                _enemyDirectionController.ChangeLane();
            }
            _enemyDirectionController.MoveForward();
        }
    }
}