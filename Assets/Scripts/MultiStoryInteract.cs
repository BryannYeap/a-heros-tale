using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStoryInteract : Interactable
{
    public override void Interact()
    {
        GetComponent<Door>().enabled = true;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y == GetComponent<Door>().spawnPoint.position.y)
        {
            GetComponent<Door>().enabled = false;
        }
    }
}
