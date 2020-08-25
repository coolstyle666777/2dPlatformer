using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float _transitionTime;
    private Animator _animator;
    private Timer _timer;

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _timer = FindObjectOfType<Timer>();
    }

    public void OnEnable()
    {
        if (_timer != null)
            _timer.TimesOver += RestartLevel;
    }

    public void OnDisable()
    {
        if (_timer != null)
            _timer.TimesOver -= RestartLevel;
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
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
