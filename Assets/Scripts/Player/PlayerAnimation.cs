using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void SetGrounded(bool value)
    {
        _playerAnimator.SetBool("Grounded", value);
    }

    public void SetRunning(bool value)
    {
        _playerAnimator.SetBool("Running", value);
    }
}
