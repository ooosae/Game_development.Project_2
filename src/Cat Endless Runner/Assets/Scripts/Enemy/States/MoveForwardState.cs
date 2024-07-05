using FSM;

namespace Enemy.States
{
    public class MoveForwardState : BaseState
    {
        private readonly EnemyDirectionController _enemyDirectionController;

        public MoveForwardState(EnemyDirectionController enemyDirectionController)
        {
            _enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            if (!_enemyDirectionController.IsObstacleAhead())
            {
                _enemyDirectionController.MoveForward();
            }
        }
    }
}