using Menu;
using Player;
using UnityEngine;

namespace Environment
{
    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GroundTile _groundTile;

        private Vector3 _nextSpawnPoint;

        private void Start()
        {
            for (var i = 0; i < 30; ++i)
            {
                SpawnTile(i < 3 ? 0 : i);
            }
        }

        public void SpawnTile(int spawnItems)
        {
            var groundTile = Instantiate(_groundTile, _nextSpawnPoint, Quaternion.identity);
            groundTile.Init(groundSpawner: this, _playerController, _gameManager);
            _nextSpawnPoint = groundTile.transform.GetChild(1).transform.position;
            
            if (spawnItems > 0)
            {
                switch (spawnItems % 4)
                {
                    case 0:
                    {
                        groundTile.SpawnFlowers();
                        break;
                    }
                    case 1:
                    {
                        groundTile.SpawnDogs();
                        break;
                    }
                    case 2:
                    {
                        groundTile.SpawnBirds();
                        break;
                    }
                    case 3:
                    {
                        groundTile.SpawnEnemies(_gameManager);
                        break;
                    }
                }
            }

            groundTile.SpawnCoins();
            groundTile.SpawnBoost();
        }
    }
}