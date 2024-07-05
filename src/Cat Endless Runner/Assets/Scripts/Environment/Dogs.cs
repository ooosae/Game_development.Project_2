using Player;
using UnityEngine;

namespace Environment
{
    public class Dogs : MonoBehaviour
    {
        private PlayerController _playerController;
        
        public void Init(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != "Cat")
            {
                return;
            }

            if (_playerController == null)
            {
                return;
            }

            if (_playerController.IsBoosted())
            {
                _playerController.Deboost();
                Destroy(gameObject);
            }
            else
            {
                _playerController.Die();
            }
        }
    }
}