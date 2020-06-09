using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private int _level;
    private SpriteRenderer _closeDoorSprite;
    private LevelLoader _levelLoader;
    private bool _unlock;


    public void Start()
    {
        _unlock = GameData.LevelUnlockList[_level - 1];
        if (_unlock)
        {
            _closeDoorSprite = GetComponentsInChildren<SpriteRenderer>()[1];
            _closeDoorSprite.enabled = false;
        }
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.Return) && _unlock)
            {
                _levelLoader.LoadLevel(++_level);
            }
        }
    }
}
