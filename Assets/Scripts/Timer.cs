using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _startTime;
    private float _currentTime;
    private TextMeshProUGUI _timerText;
    private FinishFlag _finishFlag;

    public float CurrentTime => _currentTime;
    public event UnityAction TimesOver;

    public void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _currentTime = _startTime;
        _finishFlag = FindObjectOfType<FinishFlag>();
    }

    public void OnEnable()
    {
        _finishFlag.LevelComplete += OnLevelComplete;
    }

    public void OnDisable()
    {
        _currentTime = _startTime;
        _timerText.text = "";
        _finishFlag.LevelComplete -= OnLevelComplete;
    }

    public void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            _timerText.text = _currentTime.ToString("0.00");
            _timerText.color = Color.Lerp(Color.red, Color.green, _currentTime / _startTime);
        }
        else
        {
            _timerText.text = "0.00";
            TimesOver?.Invoke();
        }
    }

    private void OnLevelComplete()
    {
        if (GameData.LevelHiscore[_finishFlag.NextLevel - 2] < _currentTime)
        {
            GameData.LevelHiscore[_finishFlag.NextLevel - 2] = _currentTime;
        }       
        enabled = false;
    }
}
