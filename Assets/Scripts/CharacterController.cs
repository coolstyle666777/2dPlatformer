using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))] 
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _moveSpeed = 10f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;
    [Header("Events")]
    [Space]
    [SerializeField] private UnityEvent OnLandEvent;

    private const float _groundedRadius = .2f;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private Vector3 _velocity = Vector3.zero;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * _moveSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        if ((move > 0 && !_facingRight) || (move < 0 && _facingRight))
        {
            Flip();
        }        

        if (_isGrounded && jump)
        {
            Jump();
        }
    }

    public void RemoveVelocity()
    {
        _rigidbody2D.velocity = Vector3.zero;
    }

    private void Jump()
    {
        _isGrounded = false;
        transform.Translate(Vector2.up * 0.1f); //магическое число чтобы не срабатывал OnLandEvent на старте прыжка
        _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
   
}
