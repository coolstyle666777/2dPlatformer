using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _damageForce = 400f;
    [SerializeField] private float _invincibleTime = 3f;   
    private CharacterMover _CharacterMover;
    private Animator _playerAnimator;
    private Rigidbody2D _rigidbody2D;
    private CoinStash _coinStash;
    private SpriteBlink _spriteBlink;
    private float _horizontalMove;
    private bool _isJump;
    private bool _isInvincible;
    private bool _canMove;

    public bool IsInvincible => _isInvincible;
    public UnityEvent Hitted;
    public UnityEvent CoinPicked;

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
                SceneManager.LoadScene(1);
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

    private void Awake()
    {
        if (Hitted == null)
        {
            Hitted = new UnityEvent();
        }
        _CharacterMover = GetComponent<CharacterMover>();
        _playerAnimator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteBlink = GetComponent<SpriteBlink>();
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
            _playerAnimator.SetBool("Running", true);
        } else
        {
            _playerAnimator.SetBool("Running", false);
        }
    }

    private void CheckInputs()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _isJump = true;
            _playerAnimator.SetBool("Grounded", false);
        }
    }

    private void MoveAction()
    {
        if (_canMove)
        {
            _CharacterMover.Move(_horizontalMove, _isJump);
            _isJump = false;
            SetAnimationMove();
        }
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
