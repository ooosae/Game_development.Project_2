using Menu;
using Menu.LeaderBord;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LeaderBordDataController _leaderBordDataController;
        [SerializeField] private float _movementSpeed = 9;
        [SerializeField] private float _jumpForce = 300;
        [SerializeField] private float _timeBeforeNextJump = 1.2f;
        [SerializeField] private float _timeBeforeNextSlide = 1.2f;

        private float _speedIncreasePerPoint = 0.1f;
        private float _maxSpeed = 20f;
        private const float HorizontalSpeedMultiplier = 1.5f;
        
        public bool IsAlive { get; private set; } = true;

        private bool _isBoosted = false;
        private float _canJumpOrSlide = 0f;
        
        private Animator _animator;
        private Rigidbody _rigidBody;

        private static readonly int Boost1 = Animator.StringToHash("Boost");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Slide = Animator.StringToHash("Slide");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Awake()
        {
            _speedIncreasePerPoint = _gameSettings.PlayerSpeedIncreasePerPoint;
            _maxSpeed = _gameSettings.PlayerMaxSpeed;
            
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            ControllPlayer();
            if (transform.position.y < -2)
            {
                Die();
            }
        }

        public void IncreaseSpeed()
        {
            if (_movementSpeed < _maxSpeed)
            {
                _movementSpeed += _speedIncreasePerPoint;
            }
        }
        
        public void IncreaseSpeed(int value)
        {
            if (_movementSpeed < _maxSpeed)
            {
                _movementSpeed += _speedIncreasePerPoint * value;
            }
        }
        
        public bool IsBoosted()
        {
            return _isBoosted;
        }

        public void Boost()
        {
            _animator.SetInteger(Boost1, 1);
            _isBoosted = true;
        }

        public void Deboost()
        {
            _animator.SetInteger(Boost1, 0);
            _isBoosted = false;
        }

        private void ControllPlayer()
        {
            if (!IsAlive)
                return;

            var moveHorizontal = Input.GetAxisRaw("Horizontal");

            var movement = new Vector3(moveHorizontal * HorizontalSpeedMultiplier / _movementSpeed * 9, 0.0f, 1.0f);
            _animator.SetInteger(Walk, 1);
        
            transform.Translate(movement * (_movementSpeed * Time.deltaTime), Space.World);

            if (Input.GetButtonDown("Slide") && Time.time > _canJumpOrSlide)
            {
                _canJumpOrSlide = Time.time + _timeBeforeNextSlide;
                _animator.SetTrigger(Slide);
            }

            if (!Input.GetButtonDown("Jump") || !(Time.time > _canJumpOrSlide))
            {
                return;
            }
        
            _rigidBody.AddForce(0, _jumpForce, 0);
            _canJumpOrSlide = Time.time + _timeBeforeNextJump;
            _animator.SetTrigger(Jump);
        }

        public void Die()
        {
            if (!IsAlive)
            {
                return;
            }
            
            IsAlive = false;
            Invoke(nameof(Restart), 1);
        }

        private static void DefeatScene()
        {
            SceneManager.LoadScene("Defeat");
        }

        private void Restart()
        {
            var distance = _gameManager.GetDistance();
            _leaderBordDataController.AddLeaderBordResult(distance);
            DefeatScene();
        }
    }
}
