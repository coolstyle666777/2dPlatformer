using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LevelMusic : MonoBehaviour
{
    [SerializeField] private float _startPositionOfTimeOverSound;
    private AudioSource _mainMusic;
    private AudioSource _timeOverMusic;
    private Timer _timer;

    private void Awake()
    {
        _mainMusic = GetComponent<AudioSource>();
        _timeOverMusic = GetComponentsInChildren<AudioSource>()[1];
        _timer = FindObjectOfType<Timer>();
    }

    private void Start()
    {
        SetVolume(GameDataWriter.GameData.MusicValue);
    }

    public void OnEnable()
    {
        if (_timer != null)
        {
            _timer.TenSecondsLeft += StartTimeOverMusic;
            _timer.ResetTimer += ResetTimeOver;
        }
    }

    public void OnDisable()
    {
        if (_timer != null)
        {
            _timer.TenSecondsLeft -= StartTimeOverMusic;
            _timer.ResetTimer += ResetTimeOver;
        }
    }
    private void StartTimeOverMusic()
    {
        _mainMusic.Stop();
        _timeOverMusic.time = _startPositionOfTimeOverSound;
        _timeOverMusic.Play();
    }

    private void ResetTimeOver()
    {
        _timeOverMusic.Stop();
        if (!_mainMusic.isPlaying)
        {
            _mainMusic.Play();
        }
    }

    private void SetVolume(float volume)
    {
        _mainMusic.volume = volume;
        _timeOverMusic.volume = volume;
    }

    public void VolumeChanged()
    {
        _mainMusic.volume = GameDataWriter.GameData.MusicValue;
        _timeOverMusic.volume = GameDataWriter.GameData.MusicValue;
    }
}
