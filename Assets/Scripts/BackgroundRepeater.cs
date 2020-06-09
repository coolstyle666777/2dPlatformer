using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private SpriteRenderer _sprite;
    private float _lenght;
    private float _startPosition;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _startPosition = transform.position.x;
        _lenght = _sprite.bounds.size.x;
    }

    private void FixedUpdate()
    {
        float cameraPositionX = _camera.transform.position.x;
        transform.position = new Vector3(_startPosition, transform.position.y);
        if (cameraPositionX > _startPosition + _lenght)
        {
            _startPosition += _lenght;
        }
        else if (cameraPositionX < _startPosition - _lenght)
        {
            _startPosition -= _lenght;
        }
    }
}
