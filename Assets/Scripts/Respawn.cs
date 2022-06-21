using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.gameObject.tag == "Player" && enabled)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Die();
            Debug.Log("fell off the map");
        } else if (collision.gameObject.tag == "Enemy" && enabled)
        {
            Debug.Log("death zone hit enemy");
            EnemyStats enemy = collision.GetComponentInParent<EnemyStats>(); 
            enemy.Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
