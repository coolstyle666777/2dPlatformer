using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour {

    private SpriteBlink _fadeAlfa;

    private void Awake()
    {
        _fadeAlfa = GetComponent<SpriteBlink>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        var coinStash = collision.GetComponent<CoinStash>();
        var player = collision.GetComponent<Player>();
        if (player != null && !player.IsInvincible)
        {
            coinStash.AddCoin();
            player.OnCoinHit();
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
        _fadeAlfa.enabled = true;
        yield return waitTime;
        Destroy(gameObject);
    }
}
