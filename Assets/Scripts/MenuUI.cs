using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _authorGroup;

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnAuthorButtonClick()
    {
        _authorGroup.alpha = 1;
        _authorGroup.blocksRaycasts = true;
    }

    public void OnAuthorCloseButtonClick()
    {
        _authorGroup.alpha = 0;
        _authorGroup.blocksRaycasts = false;
    }
}
