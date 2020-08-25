using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    [SerializeField] private float _startTime;
    private float _currentTime;
    private TextMeshProUGUI _timerText;
    private FinishFlag _finishFlag;
    private bool _isTenSeconds;

    public float CurrentTime => _currentTime;
    public event UnityAction TenSecondsLeft;
    public event UnityAction TimesOver;
    public event UnityAction ResetTimer;

    private void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _currentTime = _startTime;
        _finishFlag = FindObjectOfType<FinishFlag>();
    }

    private void Start()
    {
        _isTenSeconds = false;
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
        _isTenSeconds = false;
        ResetTimer?.Invoke();
    }

    public void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            _timerText.text = _currentTime.ToString("0.00");
            _timerText.color = Color.Lerp(Color.red, Color.green, _currentTime / _startTime);
            if (_currentTime <= 10)
            {
                if (!_isTenSeconds)
                {
                    TenSecondsLeft?.Invoke();
                    _isTenSeconds = true;
                }
            }
        }
        else
        {
            SetTimeOver();
        }
    }

    private void OnLevelComplete()
    {
        if (GameDataWriter.GameData.LevelHiscore[_finishFlag.NextLevel - 1] / 100 < _currentTime)
        {
            GameDataWriter.GameData.LevelHiscore[_finishFlag.NextLevel - 1] = _currentTime * 100;
        }
        enabled = false;
    }

    private void SetTimeOver()
    {
        _timerText.text = "0.00";
        TimesOver?.Invoke();
    }
}
