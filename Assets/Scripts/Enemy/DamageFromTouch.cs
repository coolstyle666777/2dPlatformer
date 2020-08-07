using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromTouch : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        CheckPlayer(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        CheckPlayer(collision);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayer(collision);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        CheckPlayer(collision);
    }

    private void CheckPlayer(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage();
        }
    }

    private void CheckPlayer(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
