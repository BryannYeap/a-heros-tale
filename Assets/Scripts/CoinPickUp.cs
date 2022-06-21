using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public CoinManager coinManager;

    public static int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whole"))
        {
            FindObjectOfType<AudioManager>().Play("CoinPickUp");
            Destroy(gameObject);
            GameMaster.currentScore += coinValue;
        }       
    }
}
