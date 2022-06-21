using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_attack : MonoBehaviour
{
    public Transform ghostPrefab;
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
            byte red = (byte) Random.Range(0, 266);
            byte green = (byte)Random.Range(0, 266);
            byte blue = (byte)Random.Range(0, 266);
            ghostPrefab.GetComponent<SpriteRenderer>().color = new Color32(red, green, blue, 255);
            Transform projectile = Instantiate(ghostPrefab, transform.position, transform.rotation);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetDirection(EnemyProjectile.directions.LEFT, false);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetOriginalPosition(transform);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetRange(8);
            projectile.gameObject.GetComponent<EnemyProjectile>().SetDamage(0);
            timer = Time.time + 1f; 
        }
    }
}
