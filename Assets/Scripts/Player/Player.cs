using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterMover), typeof(PlayerAnimation), typeof(PlayerInvincible))]
[RequireComponent(typeof(CoinStash))]
public class Player : MonoBehaviour
{
    [SerializeField] private bool _checkInputs;
    private CharacterMover _CharacterMover;
    private CoinStash _coinStash;
    private PlayerAnimation _playerAnimation;
    private PlayerInvincible _playerInvincible;
    private DeathParticle _deathParticle;
    private RunParticle _runParticle;
    private float _horizontalMove;
    private bool _isJump;
    private bool _isInvincible;
    private Timer _timer;
    private FinishFlag _finishFlag;
    private LevelLoader _levelLoader;

    public bool IsInvincible
    {
        get
        {
            return _isInvincible;
        }

        set
        {
            _isInvincible = value;
        }
    }
    public bool CheckInputs
    {
        get
        {
            return _checkInputs;
        }

        set
        {
            _checkInputs = value;
        }
    }

    public UnityEvent Hitted;

    private void Awake()
    {
        if (Hitted == null)
        {
            Hitted = new UnityEvent();
        }
        _CharacterMover = GetComponent<CharacterMover>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerInvincible = GetComponent<PlayerInvincible>();
        _coinStash = GetComponent<CoinStash>();
        _deathParticle = FindObjectOfType<DeathParticle>();
        _runParticle = FindObjectOfType<RunParticle>();
        _timer = FindObjectOfType<Timer>();
        _finishFlag = FindObjectOfType<FinishFlag>();
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Start()
    {
        _playerAnimation.SetGrounded(true);
    }

    public void OnEnable()
    {
        if (_timer != null)
            _timer.TimesOver += OnDied;
        if (_finishFlag != null)
            _finishFlag.LevelComplete += OnLevelComplete;
    }

    public void OnDisable()
    {
        if (_timer != null)
            _timer.TimesOver -= OnDied;
        if (_finishFlag != null)
            _finishFlag.LevelComplete += OnLevelComplete;
    }

    public void OnLand()
    {
        _playerAnimation.SetGrounded(true);
        if (_isInvincible)
        {
            _checkInputs = true;
        }
    }

    public void TakeDamage()
    {
        if (!_isInvincible)
        {
            if (_coinStash.Amount == 0)
            {
                OnDied();
            }
            _CharacterMover.RemoveVelocity();
            _CharacterMover.KnockBack(_horizontalMove);
            _coinStash.DropCoins(CoinStash.Count.Half);
            _playerInvincible.StartInvincible();
            Hitted.Invoke();
        }
    }

    public void GoingRight()
    {
        _horizontalMove = 1;
    }

    private void Update()
    {
        CheckPlayerInputs();
    }

    private void FixedUpdate()
    {
        MoveAction();
        SetAnimationMove();
        SetJumpAnimation();
    }

    private void SetAnimationMove()
    {
        if (_horizontalMove != 0)
        {
            _playerAnimation.SetRunning(true);
        }
        else
        {
            _playerAnimation.SetRunning(false);
        }
    }

    private void SetJumpAnimation()
    {
        if (!_CharacterMover.IsGrounded)
        {
            _playerAnimation.SetGrounded(false);
        }
    }

    private void CheckPlayerInputs()
    {
        if (_checkInputs)
        {
            _horizontalMove = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                _isJump = true;
            }
        }
    }

    private void MoveAction()
    {
        _CharacterMover.Move(_horizontalMove, _isJump);
        _isJump = false;
    }

    private void OnDied()
    {
        _deathParticle.PlayParticle();
        _runParticle.StopParticle();
        _levelLoader.RestartLevel();
    }

    private void OnLevelComplete()
    {
        enabled = false;
    }
}
