using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MenuPanel : MonoBehaviour
{
    private int _nextLevel;
    private CanvasGroup _canvasGroup;
    private LevelLoader _levelLoader;

    public bool BlockPanel = false;

    public void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void Update()
    {
        if (!BlockPanel)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_canvasGroup.alpha == 1)
                {
                    ClosePanel();
                }
                else
                {
                    OpenPanel();
                }
            }
        }
    }

    public void OnResumeButtonClick()
    {
        ClosePanel();
    }

    public void OnLevelSelectButtonClick()
    {
        Time.timeScale = 1;
        _levelLoader.LoadLevel(1);
    }

    public void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        _levelLoader.LoadLevel(0);
    }

    public void OpenPanel()
    {
        Time.timeScale = 0;
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public void ClosePanel()
    {
        Time.timeScale = 1;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
}
