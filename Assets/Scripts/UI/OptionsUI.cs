using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private TextMeshProUGUI _musicText;
    [SerializeField] private TextMeshProUGUI _soundText;
    private CanvasGroup _optionsCanvas;
    private Toggle _postProcessTogle;
    

    private void Awake()
    {
        _optionsCanvas = GetComponent<CanvasGroup>();
        _postProcessTogle = FindObjectOfType<Toggle>();
    }

    private void Start()
    {
        _musicSlider.normalizedValue = GameDataWriter.GameData.MusicValue;
        _soundSlider.normalizedValue = GameDataWriter.GameData.SoundValue;
        _postProcessTogle.isOn = GameDataWriter.GameData.PostProcess;
    }

    public void Show()
    {
        _optionsCanvas.alpha = 1;
        _optionsCanvas.interactable = true;
        _optionsCanvas.blocksRaycasts = true;
    }

    public void Hide()
    {
        _optionsCanvas.alpha = 0;
        _optionsCanvas.interactable = false;
        _optionsCanvas.blocksRaycasts = false;
        GameDataWriter.SaveGameData();
    }

    public void OnQuitButtonClick()
    {
        GameDataWriter.SaveGameData();
        Application.Quit();
    }

    public void MusicValueChanged(float value)
    {
        _musicText.SetText(value.ToString());
        GameDataWriter.GameData.MusicValue = value / 100;
    }

    public void SoundValueChanged(float value)
    {
        _soundText.SetText(value.ToString());
        GameDataWriter.GameData.SoundValue = value / 100;
    }

    public void PostProcessChanged(bool value)
    {
        GameDataWriter.GameData.PostProcess = value;
    }
}
