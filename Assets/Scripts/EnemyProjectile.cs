using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float range;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public bool collidesWithGround;
    public bool collidesWithPlayer;
    public bool collidesWithEnemy;
    public bool damagesEnemy;
    public enum directions {UP, DOWN, LEFT, RIGHT};

    private Transform originalposition;
    private int damage;
    private bool isHorizontal;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidesWithGround = collidesWithGround ? collision.CompareTag("Ground") : collidesWithGround;
        collidesWithPlayer = collidesWithPlayer ? collision.CompareTag("Whole") : collidesWithPlayer;
        collidesWithEnemy = collidesWithEnemy ? collision.CompareTag("Enemy") : collidesWithEnemy;

        if (collidesWithPlayer|| collidesWithGround || collidesWithEnemy)
        {
            FindObjectOfType<AudioManager>().Play("Lava");
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Whole"))
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponentInParent<CharacterController2D>().knockFromRight = true;
            }
            else
            {
                collision.gameObject.GetComponentInParent<CharacterController2D>().knockFromRight = false;
            }
            collision.gameObject.GetComponentInParent<Player>().TakeDamage(damage);
        }
        else if (collision.CompareTag("Enemy") && damagesEnemy)
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
        if (isHorizontal && Mathf.Abs(transform.position.x - originalposition.position.x) > range)
        {
            Destroy(gameObject);
        } else if (!isHorizontal && Mathf.Abs(transform.position.y - originalposition.position.y) > range)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void SetRange(float rge)
    {
        range = rge;
    }

    public void SetOriginalPosition(Transform position)
    {
        originalposition = position;
    }

    public void SetDirection(directions direction, bool rotate)
    {
        switch (direction)
        {
            case directions.UP:
                rb.velocity = transform.up * speed;
                if (rotate) GetComponent<Rigidbody2D>().rotation = 0;
                isHorizontal = false;
                break;
            case directions.DOWN:
                rb.velocity = -transform.up * speed;
                if (rotate) GetComponent<Rigidbody2D>().rotation = 180;
                isHorizontal = false;
                break;
            case directions.LEFT:
                rb.velocity = -transform.right * speed;
                if (rotate) GetComponent<Rigidbody2D>().rotation = 90;
                isHorizontal = true;
                break;
            case directions.RIGHT:
                if (rotate) GetComponent<Rigidbody2D>().rotation = 270;
                rb.velocity = transform.right * speed;
                isHorizontal = true;
                break;
        }
    }
}
