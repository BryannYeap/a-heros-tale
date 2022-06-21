using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text currentCoins;

    public void setCoins(int coins)
    {
        currentCoins.text = coins.ToString();
    }
}
