using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericTrigger : MonoBehaviour
{
    private GameObject player;

    public DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private bool collided = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
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
            dialogueTrigger.StopDialogue();
            collided = false;
        }
    }

}
