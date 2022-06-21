using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float range;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    private Vector3 direction;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {





            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponentInParent<Animator>().SetTrigger("OnHit");
            collision.transform.parent.GetComponent<EnemyStats>().TakeDamage(damage);
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponentInParent<EnemyStats>().knockFromRight = true;
            }
            else
            {
                collision.gameObject.GetComponentInParent<EnemyStats>().knockFromRight = false;
            }
        }
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x) > range) {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void SetDirection(Vector3 vectorDirection)
    {
        direction = vectorDirection;
    }
}
