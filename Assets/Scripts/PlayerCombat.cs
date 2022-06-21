using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public Transform bulletPrefab;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
    float nextAttackTime;
    private int damage;
    private float attackRate;
    private float timeOfAttack;
    private float tempGravityScale;

    private void Awake()
    {
        tempGravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nextAttackTime);
        if (animator.GetBool("AnimationFinished") == true)
        {
            UnfreezeMovement();
        }

        transform.Find("Player Canvas").transform.Find("Attack Cooldown Image").GetComponent<Image>().fillAmount = (Time.time - timeOfAttack) / (1f / attackRate);
        if (transform.Find("Player Canvas").transform.Find("Attack Cooldown Image").GetComponent<Image>().fillAmount == 1) {
            transform.Find("Player Canvas").transform.Find("Attack Cooldown Image").GetComponent<Image>().enabled = false;
        } else
        {
            transform.Find("Player Canvas").transform.Find("Attack Cooldown Image").GetComponent<Image>().enabled = true;
        }
    }

    public bool EnoughTimePassed()
    {
        return Time.time >= nextAttackTime;
    }

    public void LightAttack()
    {
        // Play attack animation
        animator.SetTrigger("LightAttack");

        // Freeze in place
        FreezeMovement();

        damage = Player.lightAttackDamage;
        attackRate = Player.lightAttackRate;

        nextAttackTime = Time.time + (1f / attackRate);
        timeOfAttack = Time.time;
    }

    public void HeavyAttack()
    {
        // Play attack animation
        animator.SetTrigger("HeavyAttack");

        // Freeze in place
        FreezeMovement();

        damage = Player.heavyAttackDamage;
        attackRate = Player.heavyAttackRate;

        nextAttackTime = Time.time + (1f / attackRate);
        timeOfAttack = Time.time;
    }

    void Attack()
    {
        FindObjectOfType<AudioManager>().Play("WarriorAttack");

        // Detect enemies in ranges
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);

            if (enemy.name != "Training Dummy SpriteSheet_0")
            {
                if (enemy.gameObject.transform.position.x < transform.position.x)
                {
                    enemy.gameObject.GetComponentInParent<EnemyStats>().knockFromRight = true;
                }
                else
                {
                    enemy.gameObject.GetComponentInParent<EnemyStats>().knockFromRight = false;
                }

                enemy.transform.parent.GetComponent<EnemyStats>().TakeDamage(damage);
            }

            enemy.GetComponentInParent<Animator>().SetTrigger("OnHit");
        }
        
    }

    public void RangeAttack() // call in animator
    {
        Transform projectile = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        projectile.gameObject.GetComponent<Projectile>().SetDirection(projectile.right);
        projectile.gameObject.GetComponent<Projectile>().SetDamage(damage);

        switch (PlayerPrefs.GetInt("selectedCharacterClass"))
        {
            case 1: // Archer
                FindObjectOfType<AudioManager>().Play("ArcherAttack");
                break;
            case 2: // Mage
                FindObjectOfType<AudioManager>().Play("MageAttack");
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void FreezeMovement()
    {
        GetComponent<PlayerMovement>().SetMove(false);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | 
            RigidbodyConstraints2D.FreezePositionY |
            RigidbodyConstraints2D.FreezeRotation;
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void UnfreezeMovement()
    {
        GetComponent<PlayerMovement>().SetMove(true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<CharacterController2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = tempGravityScale;
        animator.SetBool("AnimationFinished", false);
    }
}
