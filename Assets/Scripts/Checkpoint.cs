using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : Interactable
{
    public MessageBox messageBox;

    private bool interacting = false;

    public void Start()
    {   
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && interacting)
        {
            messageBox.ClosePopUpBox();
            interacting = false;
            messageBox.transform.Find("Button").Find("Accept Text").gameObject.GetComponent<Text>().text = "OK";
        }
    }
    public override void Interact()
    {
        FindObjectOfType<AudioManager>().Play("Checkpoint");
        interacting = true;
        GameMaster.SetCheckpoint(transform);
        messageBox.SetMessageBox("Bioinformation successfully replicated! You will return to this location upon death!", messageBox.ClosePopUpBox);
        messageBox.transform.Find("Button").Find("Accept Text").gameObject.GetComponent<Text>().text = "[ O ]K";
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
