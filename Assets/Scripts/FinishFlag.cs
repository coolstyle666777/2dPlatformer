using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishFlag : MonoBehaviour
{
    [SerializeField] private int _nextLevel;
    public int NextLevel => _nextLevel;
    public event UnityAction LevelComplete;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            LevelComplete?.Invoke();
            GameData.LevelUnlockList[_nextLevel - 1] = true;
        }
    }
}
