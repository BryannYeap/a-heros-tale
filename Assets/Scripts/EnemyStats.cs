using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject invisibleWall;

    // KnockBack
    public float knockBack;
    public float knockBackLength;
    public float knockBackCount;
    public bool knockFromRight;
        
    private Rigidbody2D m_Rigidbody2D;
    private float fallSpeed = 2f;
    private float m_MovementSmoothing = 1f;
    private Vector3 m_Velocity = Vector3.zero;

    private void Start()
    {
        currentHealth = maxHealth;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (knockBackCount > 0)
        {
            GetComponent<EnemyAI>().SetMoveActive(false);

            if (knockFromRight)
            {
                m_Rigidbody2D.velocity = new Vector2(-knockBack, knockBack);
            }
            else if (!knockFromRight)
            {
                m_Rigidbody2D.velocity = new Vector2(knockBack, knockBack);
            }
            knockBackCount -= Time.deltaTime;
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, -m_Rigidbody2D.velocity.y * fallSpeed);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            GetComponent<EnemyAI>().SetMoveActive(true);
        }
    }

    public void ShowHealth()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        healthBar.gameObject.SetActive(true);
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDie");
        healthBar.gameObject.SetActive(false);
        invisibleWall.SetActive(false);
        GetComponent<EnemyAI>().enabled = false;
        transform.Find("EnemyCollider").gameObject.layer = 12; // DeadEnemy
        transform.Find("EnemyCollider").gameObject.tag = "Untagged";

        GetComponent<Animator>().SetBool("canWalk", false);
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("IsDead");
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Hit");
        currentHealth -= damage;
        ShowHealth();
        if (PlayerPrefs.GetInt("selectedCharacterClass") == 0) // Knockback For Melee
        {
            knockBackCount = knockBackLength;
        } else                                                 // Knockback For Range
        {
            knockBackCount = knockBackLength * 0.15f;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
