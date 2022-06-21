using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatDialogueTrigger : MonoBehaviour
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
            collided = true;
            dialogueTrigger.TriggerDialogue();
        }
    }

    private void Update()
    {
        if (dialogueManager.endOfDialogue == true && collided)
        {
            dialogueTrigger.StopDialogue();
            collided = false;

            Destroy(gameObject);
        }
    }

}
