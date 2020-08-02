using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    [SerializeField] private float _saturationTime;
    private Timer _timer;
    private PostProcessVolume _postProcessVolume;
    private ColorGrading _colorGrading;
    private float _normalSaturationValue;
    private void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings<ColorGrading>(out _colorGrading);
    }

    private void Start()
    {
        _normalSaturationValue = _colorGrading.saturation.value;
    }

    public void OnEnable()
    {
        if (_timer != null)
        {
            _timer.ResetTimer += ResetTimeOver;
        }
    }

    public void OnDisable()
    {
        if (_timer != null)
        {
            _timer.ResetTimer += ResetTimeOver;
        }
    }

    private void Update()
    {
        EnableCheck();
        if (_timer != null)
        {
            if (_timer.CurrentTime < _saturationTime)
            {
                _colorGrading.saturation.value = Mathf.Lerp(-100, _normalSaturationValue, _timer.CurrentTime / _saturationTime);
            }
        }
    }

    private void EnableCheck()
    {
        _postProcessVolume.enabled = GameDataWriter.GameData.PostProcess;
    }

    private void ResetTimeOver()
    {
        _colorGrading.saturation.value = _normalSaturationValue;
    }
}
