using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private LevelLoader _levelLoader;
    private Player _player;

    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();
        _player = FindObjectOfType<Player>();
        GameDataWriter.LoadGameData();
    }

    public void OnStartButtonClick()
    {
        if (GameDataWriter.GameData.LevelUnlock[1])
        {
            _levelLoader.LoadLevel(1);
        }
        else
        {
            _levelLoader.LoadLevel(2);
        }
        _levelLoader.GetComponentInChildren<Animator>().SetFloat("speed", 0.2f);
        _player.GoingRight();
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
