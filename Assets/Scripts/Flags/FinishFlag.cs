using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishFlag : MonoBehaviour
{
    [SerializeField] private int _nextLevel;
    public int NextLevel => _nextLevel;
    public event UnityAction LevelComplete;
    private AudioSource _finishSound;
    private Timer _timer;

    private void Awake()
    {
        _finishSound = GetComponent<AudioSource>();
        _timer = FindObjectOfType<Timer>();
    }

    private void Start()
    {
        _finishSound.volume = GameDataWriter.GameData.SoundValue;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _timer.enabled = true;
            LevelComplete?.Invoke();
            _finishSound.volume = GameDataWriter.GameData.SoundValue;
            _finishSound.Play();
            ApplyFinishGameData();
        }
    }

    private void ApplyFinishGameData()
    {
        GameDataWriter.GameData.LevelUnlock[_nextLevel] = true;
        GameDataWriter.GameData.LastLevelComplete = _nextLevel - 1;
        GameDataWriter.SaveGameData();
    }
}
