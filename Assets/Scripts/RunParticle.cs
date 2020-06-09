using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour
{
    [SerializeField] private float _enableSpeed;
    private ParticleSystem _particleSystem;
    private CharacterMover _characterMover;

    public void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _characterMover = GetComponentInParent<CharacterMover>();
    }

    public void Update()
    {
        FlipSide();
        SpeedControl();
    }

    private void FlipSide()
    {
        if (_characterMover.FacingRight)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void SpeedControl()
    {
        if(Mathf.Abs(_characterMover.Rigidbody2D.velocity.x) + Mathf.Abs(_characterMover.Rigidbody2D.velocity.y) > _enableSpeed)
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }       
        } else
        {
            _particleSystem.Stop(); 
        }           
    }
}
