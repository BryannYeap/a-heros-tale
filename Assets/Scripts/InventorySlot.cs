using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image slotImage;
    public GameObject deleteButton;
    public GameObject useButton;
    public int slotNumber;

    private bool buttonsShown = false;

    private YesNoBox yesOrNoBox;
    private MessageBox popupBox;

    private void Awake()
    {
        yesOrNoBox = GameObject.Find("Canvas").transform.Find("Yes No box").gameObject.GetComponent<YesNoBox>();
        popupBox = GameObject.Find("Canvas").transform.Find("Message Box").gameObject.GetComponent<MessageBox>();
    }

    void Update()
    {

    }

    public void Clicked()
    {
        if (!buttonsShown)
        {
            ShowDeleteButton();

            if (slotNumber >= 4 && slotNumber <= 7)
            {
                useButton.SetActive(true);
            }

        } else
        {
            HideDeleteButton();

            if (slotNumber >= 4 && slotNumber <= 7)
            {
                useButton.SetActive(false);
            }
        }
    }

    public void DeleteButtonClicked()
    {
        if (slotNumber < 4)
        {
            popupBox.SetMessageBox("We might need those for later...", ClosePopUpBox);
        }
        else
        {
            yesOrNoBox.SetYesNoButton("Are you sure you want to remove this item permanantly?", RemoveThisItem, UndoButton);
        }
    }

    public void UseButtonClicked()
    {
        yesOrNoBox.SetYesNoButton("Do you want to use this potion?", UsePotion, UndoButton);
    }

    void RemoveThisItem()
    {
        // Remove from inventory
        GetComponentInParent<Inventory>().RemoveItem(slotNumber);

        // Close Popup box
        ClosePopUpBox();

        // Close delete button
        HideDeleteButton();

        // Close use button
        useButton.SetActive(false);
    }

    void UndoButton()
    {
        // Close Popup box
        ClosePopUpBox();

        // Close delete button
        HideDeleteButton();

        // Close use button
        useButton.SetActive(false);
    }

    void UsePotion()
    {
        GetComponentInParent<Inventory>().UsePotion(slotNumber);
        UndoButton();
    }

    void ClosePopUpBox()
    {
        yesOrNoBox.ClosePopUpBox();
        popupBox.ClosePopUpBox();
    }

    public void ShowDeleteButton()
    {
        deleteButton.SetActive(true);
        buttonsShown = true;
        deleteButton.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void HideDeleteButton()
    {
        deleteButton.SetActive(false);
        buttonsShown = false;
        deleteButton.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
    }
}
