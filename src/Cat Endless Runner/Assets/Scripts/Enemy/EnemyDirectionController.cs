using UnityEngine;

namespace Enemy
{
    public class EnemyDirectionController : MonoBehaviour
    {
        private int _currentLaneIndex;
        private readonly float[] _lanes = { -3.3f, 0f, 3.3f };
        private Collider _collider;
        
        private const float MinRunSpeed = 2f;
        private const float MaxRunSpeed = 7f;

        public void InitializePosition(int laneIndex)
        {
            _currentLaneIndex = laneIndex;
            var newPosition = new Vector3(_lanes[_currentLaneIndex], transform.position.y, transform.position.z);
            transform.position = newPosition;
            _collider = GetComponent<Collider>();
        }

        public void MoveForward()
        {
            var speed = Random.Range(MinRunSpeed, MaxRunSpeed);
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        public void ChangeLane()
        {
            _currentLaneIndex = _currentLaneIndex switch
            {
                0 or 2 => 1,
                1 => Random.value > 0.5f ? 0 : 2,
                _ => _currentLaneIndex
            };

            var newPosition = new Vector3(_lanes[_currentLaneIndex], transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        public bool IsObstacleAhead()
        {
            var rayOrigin = new Vector3(transform.position.x, _collider.bounds.center.y, transform.position.z);
            return Physics.Raycast(rayOrigin, Vector3.forward, out var hit, 5.0f) &&
                   hit.collider.CompareTag("Obstacle");
        }
    }
}
