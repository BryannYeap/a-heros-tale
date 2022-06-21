using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessageBox(string message, UnityEngine.Events.UnityAction okayCall)
    {
        // Freezing controls
        GameMaster.FreezeControls();

        // Pausing game
        GameMaster.PauseGame();

        //Finding and activating the popup box
        transform.Find("Message").GetComponent<Text>().text = message;
        transform.gameObject.SetActive(true);

        //Unlock cursor
        Cursor.visible = true;

        // Adding function to the OK button
        transform.Find("Button").transform.Find("Accept Button").GetComponent<Button>().onClick.AddListener(okayCall);
    }

    public void ClosePopUpBox()
    {
        FindObjectOfType<AudioManager>().Play("ClickingButton");

        transform.gameObject.SetActive(false);

        transform.Find("Button").transform.Find("Accept Button").GetComponent<Button>().onClick.RemoveAllListeners();

        // Unfreezing controls
        GameMaster.UnfreezeControls();

        // Lock cursor
        Cursor.visible = false;

        // Resuming game
        GameMaster.ResumeGame();
    }
}
