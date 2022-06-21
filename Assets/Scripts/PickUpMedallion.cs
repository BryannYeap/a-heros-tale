using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMedallion : Interactable
{
    public ShopItemSO medallion;

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
        FindObjectOfType<AudioManager>().Play("Medallion");
        Inventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").gameObject.GetComponent<Inventory>();
        inventory.AddItem(medallion);
        Destroy(gameObject);
    }
}
