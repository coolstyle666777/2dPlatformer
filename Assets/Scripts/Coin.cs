using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteBlink _spriteBlink;

    private void Awake()
    {
        _spriteBlink = GetComponent<SpriteBlink>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        var coinStash = collision.GetComponent<CoinStash>();
        var player = collision.GetComponent<Player>();
        if (player != null && !player.IsInvincible)
        {
            coinStash.AddCoin();
            player.OnCoinPick();
            Destroy(gameObject);
        }       
    }

    public void FadeDestroy(float timeToDestroy)
    {
        StartCoroutine(Destroy(timeToDestroy));
    }

    private IEnumerator Destroy(float timeToDestroy)
    {
        WaitForSeconds waitTime = new WaitForSeconds(timeToDestroy);
        _spriteBlink.enabled = true;
        yield return waitTime;
        Destroy(gameObject);
    }
}
