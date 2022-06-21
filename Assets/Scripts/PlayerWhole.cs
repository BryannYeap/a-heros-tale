using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitBox"  && gameObject.CompareTag("Whole") && gameObject.activeInHierarchy)
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                GetComponentInParent<CharacterController2D>().knockFromRight = false;
            }
            else
            {
                GetComponentInParent<CharacterController2D>().knockFromRight = true;
            }

            GetComponentInParent<Player>().TakeDamage(collision.gameObject.GetComponentInParent<EnemyAI>().damage);
        }
    }
}
