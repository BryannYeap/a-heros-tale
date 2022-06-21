using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetYesNoButton(string message, UnityEngine.Events.UnityAction yesCall, UnityEngine.Events.UnityAction noCall)
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

        transform.Find("Yes").transform.Find("Yes Button").GetComponent<Button>().onClick.AddListener(yesCall);
        transform.Find("No").transform.Find("No Button").GetComponent<Button>().onClick.AddListener(noCall);
    }

    public void ClosePopUpBox()
    {
        FindObjectOfType<AudioManager>().Play("ClickingButton");

        transform.gameObject.SetActive(false);

        transform.Find("Yes").transform.Find("Yes Button").GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("No").transform.Find("No Button").GetComponent<Button>().onClick.RemoveAllListeners();

        // Unfreezing controls
        GameMaster.UnfreezeControls();

        // Lock cursor
        Cursor.visible = false;

        // Resuming game
        GameMaster.ResumeGame();
    }
}
