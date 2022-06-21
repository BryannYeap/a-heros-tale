using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flora_attack : MonoBehaviour
{
    public Transform spikePrefab;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer)
        {
            Transform projectile = Instantiate(spikePrefab, transform.position, transform.rotation);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetDirection(EnemyProjectile.directions.LEFT, false);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetOriginalPosition(transform);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetRange(8);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetDamage(0);
            timer = Time.time + 1f;
        }
    }
}
