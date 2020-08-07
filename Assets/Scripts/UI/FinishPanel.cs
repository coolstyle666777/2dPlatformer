using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hiscoreText;
    private int _nextLevel;
    private CanvasGroup _canvasGroup;
    private FinishFlag _finishFlag;
    private MenuPanel _menuPanel;
    private LevelLoader _levelLoader;
    private Timer _timer;
    
    public void Awake()
    {
        _nextLevel = FindObjectOfType<FinishFlag>().NextLevel;
        _canvasGroup = GetComponent<CanvasGroup>();
        _finishFlag = FindObjectOfType<FinishFlag>();
        _menuPanel = FindObjectOfType<MenuPanel>();
        _levelLoader = FindObjectOfType<LevelLoader>();
        _timer = FindObjectOfType<Timer>();
    }

    public void OnEnable()
    {
        _finishFlag.LevelComplete += OpenPanel;
    }

    public void OnDisable()
    {
        _finishFlag.LevelComplete -= OpenPanel;
    }

    public void OnLevelSelectButtonClick()
    {
        _levelLoader.LoadLevel(1);
    }

    public void OnNextLevelButtonClick()
    {
        _levelLoader.LoadLevel(_nextLevel+2);
    }

    public void OnLevelRestartButtonClick()
    {
        _levelLoader.LoadLevel(++_nextLevel);
    }

    public void OpenPanel()
    {
        _hiscoreText.text = _timer.CurrentTime.ToString("0.00");
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _menuPanel.BlockPanel = true;
    }
}
