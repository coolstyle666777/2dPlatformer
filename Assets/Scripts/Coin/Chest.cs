using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private float _animationSpeed;
    private Animator _chestAnimator;

    private void Awake()
    {
        _chestAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _chestAnimator.SetFloat("Speed", 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            _chestAnimator.SetFloat("Speed", _animationSpeed);
        }
    }
}
