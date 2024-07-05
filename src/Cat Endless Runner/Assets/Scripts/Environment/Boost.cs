using Menu;
using Unity.VisualScripting;
using UnityEngine;

namespace Environment
{
    public class Boost : MonoBehaviour
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

            _gameManager.DoBoost();
            Destroy(gameObject);
        }
    }
}