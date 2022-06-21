using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGeyser : MonoBehaviour
{
    public Transform lavaPrefab;

    public float timeBetweenEruptions;
    public float lengthOfEruptions;
    public int damage = 2;
    public string direction;
    public float delay;
    public float range;

    private float timeOfNextEruption;
    private float timeToEruptTill;
    // Start is called before the first frame update
    void Awake()
    {
        delay += Time.time;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Time.time >= timeOfNextEruption + delay)
        {
            timeOfNextEruption += timeBetweenEruptions;
            timeToEruptTill = Time.time + lengthOfEruptions;
        } else if (Time.time < timeToEruptTill)
        {
            ShootLavaPrefab();
        }
    }

    void ShootLavaPrefab()
    {
        EnemyProjectile.directions intendedDirection = EnemyProjectile.directions.UP;
        switch (direction)
        {
            case "down":
            case "Down":
            case "DOWN":
                intendedDirection = EnemyProjectile.directions.DOWN;
                break;
            case "left":
            case "Left":
            case "LEFT":
                intendedDirection = EnemyProjectile.directions.LEFT;
                break;
            case "right":
            case "Right":
            case "RIGHT":
                intendedDirection = EnemyProjectile.directions.RIGHT;
                break;
            default:
                intendedDirection = EnemyProjectile.directions.UP;
                break;

        }

        Transform projectile = Instantiate(lavaPrefab, transform.position, transform.rotation);
        projectile.gameObject.GetComponent<EnemyProjectile>().SetDirection(intendedDirection, true);
        projectile.gameObject.GetComponent<EnemyProjectile>().SetOriginalPosition(transform);
        projectile.gameObject.GetComponent<EnemyProjectile>().SetRange(range);
        projectile.gameObject.GetComponent<EnemyProjectile>().SetDamage(damage);
    }
}
