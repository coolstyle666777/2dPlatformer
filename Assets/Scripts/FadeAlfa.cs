using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAlfa : MonoBehaviour {

    [SerializeField] private float _fadeRate;
    private SpriteRenderer _sprite;
    private float alpha = 1.0f;
    private Color color;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
         color = _sprite.color;
        enabled = false;
    }



    private void FixedUpdate()
    {       
        color.a = Mathf.MoveTowards(color.a, alpha, Time.fixedDeltaTime + _fadeRate);
        _sprite.color = color;

        if (color.a == alpha)
        {
            if (alpha == 1.0f)
            {
                alpha = 0.0f;
            }
            else
                alpha = 1.0f;
        }
    }

    private void OnDisable()
    {
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1);
    }
}

