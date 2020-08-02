using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour
{
    [SerializeField] private float _timePositionOfStartSound;
    private Timer _timer;
    private AudioSource _startSound;

    public void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _startSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _startSound.volume = GameDataWriter.GameData.SoundValue;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (transform.position.x > player.transform.position.x)
            {
                _timer.enabled = false;
            }
            else
            {
                _timer.enabled = true;
                _startSound.time = _timePositionOfStartSound;
                _startSound.volume = GameDataWriter.GameData.SoundValue;
                _startSound.Play();
            }
        }
    }
}
