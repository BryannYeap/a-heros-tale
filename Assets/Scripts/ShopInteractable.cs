using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopInteractable : Interactable
{
    public GameObject shop;

    private void Start()
    {
    }
    public override void Interact()
    {
        shop.SetActive(true);
        shop.GetComponent<ShopManager>().CheckPurchaseable();
        shop.GetComponent<ShopManager>().SetShopOpen(true);
        if (SceneManager.GetActiveScene().buildIndex == 2) GetComponent<ShopFirstTime>().Interact();
    }
    private void Update()
    {

    }
    public void CloseShop()
    { 
        shop.SetActive(false);

        shop.GetComponent<ShopManager>().SetShopOpen(false);

        // Lock cursor
        Cursor.visible = false;
    }
}
