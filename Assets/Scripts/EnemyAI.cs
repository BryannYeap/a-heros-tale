using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public int damage;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; // Check if player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; // Dist btw enemy and player
    private bool attackMode;
    private bool cooling; // Check if enemy is cooling after attack
    private float intTimer;

    private float offsetDisplacement = 0f;
    private bool moveActive = true;
    #endregion
    private void Awake()
    {
        SelectTarget();
        intTimer = timer; // Store the initial value of timer
        anim = GetComponent<Animator>();
        transform.Find("EnemyHitBox").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }

        if (moveActive)
        {
            if (!attackMode)
            {
                Move();
            }

            if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("enemy_attack")) // CHANGE HERE
            {
                SelectTarget();
            }

            if (inRange)
            { 
                EnemyLogic();
            }
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
    }

    void Move()
    {
        Transform targetTransform = CompareTag("Flying") ? target : transform;

        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("enemy_attack")) // CHANGE HERE
        {
            Vector2 targetPosition;

            if (transform.position.x > target.position.x)
            {
                targetPosition = new Vector2(target.position.x + offsetDisplacement, targetTransform.position.y);
            } else
            {
                targetPosition = new Vector2(target.position.x - offsetDisplacement, targetTransform.position.y);
            }


            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; // Reset timer when player enter attack range
        attackMode = true; // To check if enemy can still attack

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);

        transform.Find("EnemyHitBox").gameObject.SetActive(true);
    }

    void StopAttack()
    {
        attackMode = false;
        anim.SetBool("Attack", false);
        transform.Find("EnemyHitBox").gameObject.SetActive(false);
    }

    public void TriggerCooling() // call in animation
    {
        cooling = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        StopAttack();

        if (timer <= 0 && cooling && !attackMode)
        {
            timer = intTimer;
            cooling = false;
        }
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x + offsetDisplacement + 0.01 &&
            transform.position.x < rightLimit.position.x - offsetDisplacement - 0.01 ;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
    
        if (distanceToLeft  > distanceToRight)
        {
            target = leftLimit;
        } else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        } else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public void SetMoveActive(bool active)
    {
        moveActive = active;
    }
}
