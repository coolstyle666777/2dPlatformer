using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private TextMeshProUGUI _hiScoreText;
    private Player _player;
    private SpriteRenderer _closeDoorSprite;
    private SpriteRenderer _star;
    private LevelLoader _levelLoader;
    private CanvasGroup _LevelInfoGroup;
    private bool _unlock;

    public void Awake()
    {
        _player = FindObjectOfType<Player>();
        _unlock = GameDataWriter.GameData.LevelUnlock[_level];
        _closeDoorSprite = GetComponentsInChildren<SpriteRenderer>()[1];
        _star = GetComponentsInChildren<SpriteRenderer>()[2];
        _LevelInfoGroup = GetComponentInChildren<CanvasGroup>();
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Start()
    {
        _hiScoreText.SetText(GameDataWriter.GameData.LevelHiscore[_level].ToString("00-00"));
        if (_unlock)
        {
            _closeDoorSprite.enabled = false;
        }
        if (GameDataWriter.GameData.LevelHiscore[_level] != 0)
        {
            _star.enabled = true;
        }
        if (GameDataWriter.GameData.LastLevelComplete == _level)
        {
            _player.transform.position = transform.position;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (_unlock)
            {
                _LevelInfoGroup.alpha = 1;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _levelLoader.LoadLevel(_level + 2);
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _LevelInfoGroup.alpha = 0;
        }
    }
}
