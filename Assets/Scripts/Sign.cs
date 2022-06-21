using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    public DialogueTrigger dialogueTrigger;

    private bool interacting = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Interact()
    {
        dialogueTrigger.TriggerDialogue();

        interacting = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (FindObjectOfType<DialogueManager>().endOfDialogue == true && interacting)
        {
            dialogueTrigger.StopDialogue();
            interacting = false;
        }
    }
}
