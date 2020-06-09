using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour
{
    private Timer _timer;

    public void Awake()
    {
        _timer = FindObjectOfType<Timer>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (transform.position.x > player.transform.position.x)
            {
                _timer.enabled = false;
            }
            else
            {
                _timer.enabled = true;
            }
        }
    }
}
