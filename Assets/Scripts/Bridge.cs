using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bridge : Interactable
{
    public YesNoBox yesOrNoBox;
    public string leavingMessage;

    // Start is called before the first frame update
    void Awake()
    {

    }

    public override void Interact()
    {
        yesOrNoBox.SetYesNoButton(leavingMessage, LoadNextWorld, UndoButton);
    }

    private void LoadNextWorld() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Close Popup box
        yesOrNoBox.ClosePopUpBox();

        GameMaster.SetCoinsAtStart(GameMaster.currentScore);
        GameMaster.SetHealthAtStart(Player.currentHealth);
    }

    void UndoButton()
    {
        // Close Popup box
        yesOrNoBox.ClosePopUpBox();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
