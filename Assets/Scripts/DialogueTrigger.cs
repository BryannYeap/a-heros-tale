using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private GameObject player;

    public Dialogue dialogue;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TriggerDialogue()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().SetFloat("Speed", 0);

        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX |
                    RigidbodyConstraints2D.FreezeRotation;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void StopDialogue()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerMovement>().SetJump(true);
        player.GetComponent<PlayerMovement>().SetCrouch(true);
        player.GetComponent<PlayerMovement>().SetCombat(true);
    }
}
