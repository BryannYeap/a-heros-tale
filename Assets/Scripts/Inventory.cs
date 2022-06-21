using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Player player;
    public ShopItemSO emptyPlaceholder;

    public ShopItemSO[] inventoryItems;
    public InventorySlot[] inventorySlots;

    private float smallPotionHealRate = 0.25f;
    private float largePotionHealRate = 0.5f;

    // CHARM BUFFS
    #region CHARM BUFFS
    private const float defenceCharmBuff = 0.2f;
    private const float healthCharmBuff = 0.33f;
    private const float lightAttackCharmBuff = 0.4f;
    private const float heavyAttackCharmBuff = 0.4f;
    private const float moneyCharmBuff = 2f;
    private const float lightAttackRateCharmBuff = 0.4f;
    private const float heavyAttackRateCharmBuff = 0.4f;
    #endregion

    private static int numberOfMedallions = 0;
    private static int numberOfPotions = 0;
    private static int numberOfCharms = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadSlots();
    }

    // Update is called once per frame
    void Update()
    {
        GameMaster.staticInventoryItems = inventoryItems;
    }

    public void LoadSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventoryItems[i].itemType != -1)
            {
                inventorySlots[i].GetComponent<Button>().interactable = true;
            }
            inventorySlots[i].slotImage.GetComponent<Image>().sprite = inventoryItems[i].image;
            inventorySlots[i].slotNumber = i;
        }
    }

    public void AddItem(ShopItemSO shopItem)
    {
        switch (shopItem.itemType)
        {
            case 0: // MEDALLIONS
                AddToInventorySlots(0, 3, shopItem);
                numberOfMedallions += 1;
                break;
            case 1: // POTIONS
                AddToInventorySlots(4, 7, shopItem);
                numberOfPotions += 1;
                break;
            case 2: // CHARMS
                AddToInventorySlots(8, 13, shopItem);
                numberOfCharms += 1;
                switch(shopItem.codeNumber % 20)
                {
                    case 0: // USELESS CHARM
                        // do nothing
                        break;
                    case 1: // DEFENCE CHARM
                        Player.defence += (1 - Player.defence) * defenceCharmBuff;
                        break;
                    case 2: // HEALTH CHARM
                        Player.currentHealth += Mathf.CeilToInt(Player.currentHealth * healthCharmBuff);
                        Player.maxHealth += Mathf.CeilToInt(Player.maxHealth * healthCharmBuff);
                        break;
                    case 3: // HEAVY ATTACK DAMAGE CHARM
                        Player.heavyAttackDamage += Mathf.CeilToInt(Player.heavyAttackDamage * heavyAttackCharmBuff);
                        break;
                    case 4: // LIGHT ATTACK DAMAGE CHARM
                        Player.lightAttackDamage += Mathf.CeilToInt(Player.lightAttackDamage * lightAttackCharmBuff); 
                        break;
                    case 5: // MONEY CHARM
                        CoinPickUp.coinValue = (int) moneyCharmBuff;
                        break;
                    case 6: // HEAVY ATTACK RATE CHARM
                        Player.heavyAttackRate += Player.heavyAttackRate * heavyAttackRateCharmBuff;
                        break;
                    case 7: // LIGHT ATTACK RATE CHARM
                        Player.lightAttackRate += Player.lightAttackRate * lightAttackRateCharmBuff;
                        break;
                }
                break;
        }
    }

    public int GetNumOfMedallions()
    {
        return numberOfMedallions;
    }

    public int GetNumOfPotions()
    {
        return numberOfPotions;
    }

    public int GetNumOfCharms()
    {
        return numberOfCharms;
    }

    void AddToInventorySlots(int firstSlot, int lastSlot, ShopItemSO shopItem)
    {
         for (int i = firstSlot; i <= lastSlot; i++)
         {
            if (inventoryItems[i].itemType == -1)
            {
                inventoryItems[i] = shopItem;
                LoadSlots();
                break;
            }
         }
    }
    
    public void RemoveItem(int slotNumber)
    {
        ShopItemSO shopItem = inventoryItems[slotNumber];

        switch (shopItem.itemType)
        {
            case 0: // MEDALLIONS
                numberOfMedallions -= 1;
                break;
            case 1: // POTIONS
                numberOfPotions -= 1;
                break;
            case 2: // CHARMS
                numberOfCharms -= 1;
                switch (shopItem.codeNumber % 20)
                {
                    case 0: // USELESS CHARM
                        // do nothing
                        break;
                    case 1: // DEFENCE CHARM
                        float def = (Player.defence - defenceCharmBuff) * (1 / (1 - defenceCharmBuff));
                        Player.defence = def <= 0.0001 ? 0 : def;
                        break;
                    case 2: // HEALTH CHARM
                        Player.currentHealth = Mathf.FloorToInt(Player.currentHealth * (1 / (1 + healthCharmBuff)));
                        Player.maxHealth = Mathf.FloorToInt(Player.maxHealth * (1 / (1 + healthCharmBuff)));
                        break;
                    case 3: // HEAVY ATTACK DAMAGE CHARM
                        Player.heavyAttackDamage = Mathf.FloorToInt(Player.heavyAttackDamage * (1 / (1 + heavyAttackCharmBuff)));
                        break;
                    case 4: // LIGHT ATTACK DAMAGE CHARM
                        Player.lightAttackDamage = Mathf.FloorToInt(Player.lightAttackDamage * (1 / (1 + lightAttackCharmBuff)));
                        break;
                    case 5: // MONEY CHARM
                        CoinPickUp.coinValue = 1;
                        break;
                    case 6: // HEAVY ATTACK RATE CHARM
                        Player.heavyAttackRate = Player.heavyAttackRate * (1 / (1 + heavyAttackRateCharmBuff));
                        break;
                    case 7: // LIGHT ATTACK RATE CHARM
                        Player.lightAttackRate = Player.lightAttackRate * (1 / (1 + lightAttackRateCharmBuff));
                        break;
                }
                break;
        }

        inventoryItems[slotNumber] = emptyPlaceholder;
        inventorySlots[slotNumber].GetComponent<Button>().interactable = false;
        LoadSlots();
    }

    public void UsePotion(int slotNumber)
    {
        ShopItemSO item = inventoryItems[slotNumber];

        if (item.codeNumber == 10)
        {
            FindObjectOfType<AudioManager>().Play("Potion_Small");
            player.GainHealth((int) (smallPotionHealRate * Player.maxHealth));
            RemoveItem(slotNumber);
        } else if (item.codeNumber == 11)
        {
            FindObjectOfType<AudioManager>().Play("Potion_Large");
            player.GainHealth((int) (largePotionHealRate * Player.maxHealth));
            RemoveItem(slotNumber);
        }
    }
}