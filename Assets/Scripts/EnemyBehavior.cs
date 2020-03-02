using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint;
    [SerializeField] private float _pathTime;   
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _runningTime;

    private void Start()
    {
        _startPosition = transform.position;
        _endPosition = _wayPoint.position;       
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        _runningTime += Time.deltaTime;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _runningTime / _pathTime);
        if (_runningTime > _pathTime)
        {
            ChangeDirection();
            _runningTime = 0;
        }
    }

    private void ChangeDirection()
    {
        Vector3 temp = _startPosition;
        _startPosition = _endPosition;
        _endPosition = temp;
    }   

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayer(collision);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        CheckPlayer(collision);
    }

    private void CheckPlayer(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
