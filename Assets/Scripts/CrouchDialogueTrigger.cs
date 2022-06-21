using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrouchDialogueTrigger : MonoBehaviour
{
    private GameObject player;
    public DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;

    private bool collided = false;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Whole")
        {
            dialogueTrigger.TriggerDialogue();

            collided = true;
        }
    }

    private void Update()
    {
        if (dialogueManager.endOfDialogue == true && collided)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerMovement>().SetJump(true);
            player.GetComponent<PlayerMovement>().SetCrouch(true);
            player.GetComponent<PlayerMovement>().SetCombat(false);

            collided = false;

            Destroy(gameObject);
        }
    }

}
