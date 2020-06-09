using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteBlink : MonoBehaviour //Как работать с двумя классами с одной логикой в скрипте(Дженерик,интерфейс?)
{
    [SerializeField] private float _fadeRate;
    private SpriteRenderer _sprite;
    private Tilemap _tileMap;
    private float alpha = 1.0f;
    private Color color;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite == null)
        {
            _tileMap = GetComponent<Tilemap>();
            color = _tileMap.color;
        }
        else
        {
            color = _sprite.color;
        }
    }

    private void FixedUpdate()
    {
        color.a = Mathf.MoveTowards(color.a, alpha, Time.fixedDeltaTime + _fadeRate);
        if (_sprite == null)
        {
            _tileMap.color = color;
        }
        else
        {
            _sprite.color = color;
        }

        if (color.a == alpha)
        {
            if (alpha == 1.0f)
            {
                alpha = 0.0f;
            }
            else
            {
                alpha = 1.0f;
            }
        }
    }

    private void OnDisable()
    {
        if (_sprite == null)
        {
            _tileMap.color = new Color(_tileMap.color.r, _tileMap.color.g, _tileMap.color.b, 1);
        }
        else
        {
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1);
        }
    }
}