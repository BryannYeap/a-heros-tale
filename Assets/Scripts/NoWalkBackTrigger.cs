using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWalkBackTrigger : MonoBehaviour
{

    public GameObject invisibleWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enabled)
        {
            invisibleWall.SetActive(true);
        }
    }
}
