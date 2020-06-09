using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float _transitionTime;
    private Animator _animator;

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(Load(index));
    }

    public IEnumerator Load(int index)
    {
        _animator.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(index);
    }
}
