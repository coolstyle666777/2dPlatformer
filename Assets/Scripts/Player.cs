using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _damageForce = 400f;
    [SerializeField] private float _invincibleTime = 3f;
    [SerializeField] private ParticleSystem _deathParticle;
    [SerializeField] private ParticleSystem _runParticle;
    private CharacterMover _CharacterMover;
    private Animator _playerAnimator;
    private Rigidbody2D _rigidbody2D;
    private CoinStash _coinStash;
    private SpriteBlink _spriteBlink;
    private float _horizontalMove;
    private bool _isJump;
    private bool _isInvincible;
    private bool _canMove;
    private Timer _timer;
    private FinishFlag _finishFlag;
    private LevelLoader _levelLoader;
    private SpriteRenderer _spriteRenderer;

    public bool IsInvincible => _isInvincible;

    public UnityEvent Hitted;
    public UnityEvent CoinPicked;

    private void Awake()
    {
        if (Hitted == null)
        {
            Hitted = new UnityEvent();
        }
        if (CoinPicked == null)
        {
            CoinPicked = new UnityEvent();
        }
        _CharacterMover = GetComponent<CharacterMover>();
        _playerAnimator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteBlink = GetComponent<SpriteBlink>();
        _coinStash = GetComponent<CoinStash>();
        _timer = FindObjectOfType<Timer>();
        _finishFlag = FindObjectOfType<FinishFlag>();
        _levelLoader = FindObjectOfType<LevelLoader>();
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
        _playerAnimator.SetBool("Grounded", true);
        _canMove = true;
    }



    public void OnCoinPick()
    {
        CoinPicked.Invoke();
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
            _rigidbody2D.AddForce(new Vector2(-_horizontalMove * _damageForce, _damageForce));
            _coinStash.DropCoins(CoinStash.Count.Half);
            _isInvincible = true;
            StartCoroutine(Invincible());
            _canMove = false;
            Hitted.Invoke();
        }
    }

    private void Update()
    {
        CheckInputs();
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
            _playerAnimator.SetBool("Running", true);
        }
        else
        {
            _playerAnimator.SetBool("Running", false);
        }
    }

    private void SetJumpAnimation()
    {
        if (!_CharacterMover.IsGrounded)
        {
            _playerAnimator.SetBool("Grounded", false);
        }
    }

    private void CheckInputs()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _isJump = true;
        }
    }

    private void MoveAction()
    {
        if (_canMove)
        {
            _CharacterMover.Move(_horizontalMove, _isJump);
            _isJump = false;          
        }
    }

    private void OnDied()
    {
        _deathParticle.Play();
        _runParticle.Stop();
        _spriteRenderer.enabled = false;
        _levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelComplete()
    {
        enabled = false;
    }

    private IEnumerator Invincible()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_invincibleTime);
        _spriteBlink.enabled = true;
        yield return waitTime;
        _isInvincible = false;
        _spriteBlink.enabled = false;
    }
}
