using System;
using System.Collections.Generic;
using FSM;

using Enemy.States;

namespace Enemy
{
    public class EnemyStateMachine : BaseStateMachine
    {
        public EnemyStateMachine(EnemyDirectionController enemyDirectionController)
        {
            var moveForwardState = new MoveForwardState(enemyDirectionController);
            var avoidObstacleState = new AvoidObstacleState(enemyDirectionController);

            SetInitialState(moveForwardState);

            AddState(moveForwardState, new List<Transition>
            {
                new Transition(
                    avoidObstacleState,
                    enemyDirectionController.IsObstacleAhead)
            });

            AddState(avoidObstacleState, new List<Transition>
            {
                new Transition(
                    moveForwardState,
                    () => !enemyDirectionController.IsObstacleAhead())
            });
        }
    }
}