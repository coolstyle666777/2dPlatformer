using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour {

    [SerializeField] private GameObject _camera;
    private float _lenght;
    private float _startPosition;
    
	private void Start () {
        _startPosition = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
	}

    private void FixedUpdate()
    {       
        float distance = 0;
        float temp = _camera.transform.position.x;
        transform.position = new Vector3(_startPosition + distance, transform.position.y);

        if (temp > _startPosition + _lenght) _startPosition += _lenght;
        else if (temp < _startPosition - _lenght) _startPosition -= _lenght;
    }
}
