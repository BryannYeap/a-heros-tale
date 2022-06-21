using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFirstTime : Interactable
{
    public DialogueTrigger dialogueTrigger;

    private bool interacting = false;

    private bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Interact()
    {
        if (!interacted)
        {
            dialogueTrigger.TriggerDialogue();

            interacting = true;

            interacted = true;
        }
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
