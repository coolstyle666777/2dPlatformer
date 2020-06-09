using UnityEngine;

public class CoinStash : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _coinDropHeight;
    [SerializeField] private float _coinDropForce;
    [SerializeField] private float _coinDropAngleRange;
    [SerializeField] private float _coinDropRange;
    [SerializeField] private float _blinkAliveTime;
    [SerializeField] private int _amount;
    private Vector3 _dropPosition;

    public int Amount => _amount;

    public enum Count
    {
        Half,
        All
    }

    public void AddCoin()
    {
        _amount++;
    }

    public void DropCoins(Count count)
    {
        if (count == Count.Half)
        {
            if (_amount != 0)
            {
                _amount /= 2;
            }
        }
        for (int i = 0; i < _amount; i++)
        {
            float angleStep = _coinDropAngleRange * Mathf.PI * i / _amount;
            _dropPosition = new Vector3(Mathf.Cos(angleStep), Mathf.Sin(angleStep) + _coinDropHeight, 0) * _coinDropRange;
            var tempCoin = Instantiate(_coin, transform.position + _dropPosition, Quaternion.identity);
            tempCoin.FadeDestroy(_blinkAliveTime);
            var rigidCoin = tempCoin.GetComponent<Rigidbody2D>();
            rigidCoin.AddForce(_dropPosition * _coinDropForce);
        }
        _amount = 0;
    }
}
