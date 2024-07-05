using System.Collections.Generic;
using Menu;
using Player;
using UnityEngine;
using Enemy;

namespace Environment
{
    public class GroundTile : MonoBehaviour
    {
        [SerializeField] private GameObject _birdPrefab;
        [SerializeField] private GameObject _dogPrefab;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Boost _boostPrefab;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private List<GameObject> _flowerPrefabs;

        private GroundSpawner _groundSpawner;
        private PlayerController _playerController;
        private GameManager _gameManager;

        public void Init(
            GroundSpawner groundSpawner, 
            PlayerController playerController,
            GameManager gameManager)
        {
            _groundSpawner = groundSpawner;
            _playerController = playerController;
            _gameManager = gameManager;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _groundSpawner.SpawnTile(Random.Range(0, 5));
                Destroy(gameObject, 2);
            }
        }

        public void SpawnBirds()
        {
            var obstacleSpawnIndex = Random.Range(2, 5);
            var spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
            var spawnPosition = spawnPoint.position - new Vector3(0f, -1.3f, 0f);
            var spawnRotation = Quaternion.Euler(0f, 180f, 0f);

            var go = Instantiate(_birdPrefab, spawnPosition, spawnRotation, transform);
            go.AddComponent<Birds>().Init(_playerController);
        }

        public void SpawnDogs()
        {
            var obstacleSpawnIndex = Random.Range(2, 5);
            var spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
            var spawnPosition = spawnPoint.position - new Vector3(0f, 0.5f, 0f);
            var spawnRotation = Quaternion.Euler(0f, 180f, 0f);

            var go = Instantiate(_dogPrefab, spawnPosition, spawnRotation, transform);
            go.AddComponent<Dogs>().Init(_playerController);
        }

        public void SpawnEnemies(GameManager gameManager)
        {
            var enemySpawnIndex = Random.Range(2, 5);
            var spawnPoint = transform.GetChild(enemySpawnIndex).transform;
            var spawnPosition = spawnPoint.position;
            var spawnRotation = Quaternion.identity;

            var go = Instantiate(_enemyPrefab, spawnPosition, spawnRotation, transform);
            var enemyDirectionController = go.AddComponent<EnemyDirectionController>();
            var randomLaneIndex = enemySpawnIndex - 2;
            enemyDirectionController.InitializePosition(randomLaneIndex);
            go.AddComponent<EnemyCharacter>();
            var enemyCharacter = go.GetComponent<EnemyCharacter>();
            enemyCharacter.Initialize(gameManager);
        }

        public void SpawnFlowers()
        {
            if (_flowerPrefabs == null || _flowerPrefabs.Count <= 0)
            {
                Debug.LogError("No Flowers prefab!");
                return;
            }
            
            var obstacleIndex = Random.Range(0, _flowerPrefabs.Count);
            var obstaclePrefab = _flowerPrefabs[obstacleIndex];

            if (obstaclePrefab == null)
            {
                Debug.LogError("No Flower prefab!");
                return;
            }

            var obstacleSpawnIndex = Random.Range(2, 5);
            var spawnPoint = transform.GetChild(obstacleSpawnIndex);
            var go = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
            go.AddComponent<Flowers>().Init(_playerController);
            var boxCollider = go.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(1, 1, 1);
            go.tag = "Obstacle";
            boxCollider.tag = "Obstacle";
        }

        public void SpawnCoins()
        {
            const int CoinsToSpawn = 2;
            for (var i = 0; i < CoinsToSpawn; ++i)
            {
                var coin = Instantiate(_coinPrefab, transform);
                coin.Init(_gameManager);
                var coinCollider = coin.gameObject.AddComponent<BoxCollider>();
                coinCollider.isTrigger = true;
                coin.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
            }
        }

        public void SpawnBoost()
        {
            var toSpawn = Random.Range(0, 10);
            if (toSpawn != 4)
            {
                return;
            }

            var boost = Instantiate(_boostPrefab, transform);
            boost.Init(_gameManager);
            var boostCollider = boost.gameObject.AddComponent<BoxCollider>();
            boostCollider.isTrigger = true;
            boost.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }

        private static Vector3 GetRandomPointInCollider(Collider collider)
        {
            var bounds = collider.bounds;
            var point = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
            
            if (point != collider.ClosestPoint(point))
            {
                point = GetRandomPointInCollider(collider);
            }

            point.y = 1;
            
            return point;
        }
    }
}
