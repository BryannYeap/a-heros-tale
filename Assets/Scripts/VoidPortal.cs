using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPortal : Interactable
{
    public MessageBox messageBox;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (!opened)
        {
            Inventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").gameObject.GetComponent<Inventory>();
            if (inventory.GetNumOfMedallions() < 4)
            {
                messageBox.SetMessageBox("All 4 Medallions are required for this action!", messageBox.ClosePopUpBox);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Portal1");
                FindObjectOfType<AudioManager>().Play("Portal2");
                FindObjectOfType<AudioManager>().Play("Portal3");
                GetComponent<Animator>().SetBool("opened", true);
                GetComponent<MultiStoryInteract>().enabled = true;
                opened = true;
            }
        } else
        {
            GetComponent<Door>().enabled = true;
        }
    }
}
