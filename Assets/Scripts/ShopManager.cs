using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Inventory inventory;
    public ShopItemSO[] shopItemsSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtn;

    private bool shopOpen;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++) shopPanelsGO[i].SetActive(true);
        LoadPanels();
        CheckPurchaseable();
    }

    // Update is called once per frame
    void Update()
    {
        //Unlock cursor
        Cursor.visible = true;

        if (shopOpen)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().FreezeMovement();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().UnfreezeMovement();
        }
    }

    public void SetShopOpen(bool isOpen)
    {
        shopOpen = isOpen;
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].itemImage.GetComponent<Image>().sprite = shopItemsSO[i].image;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = "Coins: " + shopItemsSO[i].baseCost.ToString();
        }
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (GameMaster.currentScore >= shopItemsSO[i].baseCost) // If I have enough coins
            {
                myPurchaseBtn[i].interactable = true;
            }
            else
            {
                myPurchaseBtn[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        ShopItemSO shopItem = shopItemsSO[btnNo];

        if ((shopItem.itemType == 1 && inventory.GetNumOfPotions() < 4) || (shopItem.itemType == 2 && inventory.GetNumOfCharms() < 6)) {
            if (GameMaster.currentScore >= shopItemsSO[btnNo].baseCost)
            {
                FindObjectOfType<AudioManager>().Play("Purchase");

                GameMaster.currentScore -= shopItemsSO[btnNo].baseCost;

                inventory.AddItem(shopItemsSO[btnNo]);

                CheckPurchaseable();
            }
        } else
        {
            MessageBox messageBox = GameObject.Find("Canvas").transform.Find("Message Box").gameObject.GetComponent<MessageBox>();
            messageBox.SetMessageBox("No more inventory slots for this item! Clear your inventory!", messageBox.ClosePopUpBox);
        }
    }
}
