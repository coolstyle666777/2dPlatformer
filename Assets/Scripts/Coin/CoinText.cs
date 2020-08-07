using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    private TextMeshProUGUI _coinText;
    private Player _player;

    public void Awake()
    {
        _player = FindObjectOfType<Player>();
        _coinText = GetComponent<TextMeshProUGUI>();
    }

    public void SetCoinText()
    {
        _coinText.text = _player.GetComponent<CoinStash>().Amount.ToString();
    }
}
