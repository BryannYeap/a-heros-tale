using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExechDialogueI : Interactable
{
    public DialogueTrigger dialogueTrigger;
    public GameObject invisibleWall;

    private bool interacting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<DialogueManager>().endOfDialogue == true && interacting)
        {
            dialogueTrigger.StopDialogue();
            interacting = false;
            GetComponent<BoxCollider2D>().enabled = false;
            invisibleWall.SetActive(false);
        }
    }

    public override void Interact()
    {
        dialogueTrigger.TriggerDialogue();

        interacting = true;
    }
}
