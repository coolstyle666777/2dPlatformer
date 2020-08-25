using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinText : MonoBehaviour
{
    private TextMeshProUGUI _coinText;
    private Player _player;

    public void Awake()
    {
        _player = FindObjectOfType<Player>();
        _coinText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText()
    {
        _coinText.text = _player.GetComponent<CoinStash>().Amount.ToString();
    }
}
