using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coinsAmount = 0;

    public void AddCoin()
    {
        _coinsAmount++;
    }
}
