using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _damageForce = 400f;
    [SerializeField] private float _invincibleTime = 3f;
    [Header("Events")]
    [Space]
    [SerializeField] private UnityEvent _onHitEvent;
    [SerializeField] private UnityEvent _onCoinPickEvent;
    private CharacterMover _moveController;
    private Animator _animationController;
    private Rigidbody2D _rigidbody2D;
    private CoinStash _coinStash;
    private SpriteBlink _fadeAlfa;
    private float _horizontalMove;
    private bool _isJump;
    private bool _isInvincible;
    private bool _canMove;

    public bool IsInvincible => _isInvincible;
   
    public void OnLand()
    {
        _animationController.SetBool("Grounded", true);
        _canMove = true;
    }

    public void OnCoinHit()
    {
        _onCoinPickEvent.Invoke();
    }

    public void TakeDamage()
    {        
        if (!_isInvincible)
        {
            if (_coinStash.Amount == 0)
            {
                SceneManager.LoadScene(0);
            }
            _moveController.RemoveVelocity();
            _rigidbody2D.AddForce(new Vector2(-_horizontalMove * _damageForce, _damageForce));
            _coinStash.DropCoins(CoinStash.Count.Half);           
            _isInvincible = true;
            StartCoroutine(Invincible());
            _canMove = false;
            _onHitEvent.Invoke();
        }
    }

    private void Awake()
    {
        if (_onHitEvent == null)
            _onHitEvent = new UnityEvent();
        _moveController = GetComponent<CharacterMover>();
        _animationController = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _fadeAlfa = GetComponent<SpriteBlink>();
        _coinStash = GetComponent<CoinStash>(); 
    }

    private void Update()
    {
        CheckInputs();
    }

    private void FixedUpdate()
    {
        MoveAction();       
    }

    private void SetAnimationMove()
    {
        if (_horizontalMove != 0)
        {
            _animationController.SetBool("Running", true);
        } else
        {
            _animationController.SetBool("Running", false);
        }
    }

    private void CheckInputs()
    {
        _horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _isJump = true;
            _animationController.SetBool("Grounded", false);
        }
    }

    private void MoveAction()
    {
        if (_canMove)
        {
            _moveController.Move(_horizontalMove, _isJump);
            _isJump = false;
            SetAnimationMove();
        }
    }

    private IEnumerator Invincible()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_invincibleTime);
        _fadeAlfa.enabled = true;
        yield return waitTime;
        _isInvincible = false;
        _fadeAlfa.enabled = false;       
    }   
}
