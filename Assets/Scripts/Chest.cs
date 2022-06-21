using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public MessageBox messageBox;
    public int coinsGiven;

    public override void Interact()
    {
        FindObjectOfType<AudioManager>().Play("Chest");
        messageBox.SetMessageBox("You found " + coinsGiven + " coins!", closeMessageBox);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void closeMessageBox()
    {
        GameMaster.currentScore += coinsGiven;
        messageBox.ClosePopUpBox();
    }
}
