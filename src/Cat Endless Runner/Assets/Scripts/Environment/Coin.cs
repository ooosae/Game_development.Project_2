using Menu;
using UnityEngine;

namespace Environment
{
    public class Coin : MonoBehaviour
    {
        private GameManager _gameManager;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Flowers>() != null ||
                other.gameObject.GetComponent<Dogs>() != null ||
                other.gameObject.GetComponent<Birds>() != null)
            {
                Destroy(gameObject);
                return;
            }

            if (other.gameObject.name != "Cat")
            {
                return;
            }

            _gameManager.IncrementScore();
            Destroy(gameObject);
        }
    }
}