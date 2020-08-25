using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteBlink), typeof(Player))]
public class PlayerInvincible : MonoBehaviour
{
    [SerializeField] private float _invincibleTime = 3f;
    private SpriteBlink _spriteBlink;
    private Player _player;

    private void Awake()
    {
        _spriteBlink = GetComponent<SpriteBlink>();
        _player = GetComponent<Player>();
    }

    public void StartInvincible()
    {
        _player.IsInvincible = true;
        StartCoroutine(Invincible());
        _player.CheckInputs = false;
    }

    private IEnumerator Invincible()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_invincibleTime);
        _spriteBlink.enabled = true;
        yield return waitTime;
        _player.IsInvincible = false;
        _spriteBlink.enabled = false;
        _player.CheckInputs = true;
    }


}
