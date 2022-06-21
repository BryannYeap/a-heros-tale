using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public int itemType;
    public int codeNumber;

    /*
    ITEM TYPES
    -1 => Empty Placeholder
    0 => Medallions
    1 => Potions
    2 => Charms
    
     CODE NUMBERS
    100 => Medallion of Balance
    101 => Medallion of Eternity
    102 => Medallion of Observation
    103 => Medallion of Protection

    10 => Small Potion
    11 => Large Potion

    20 => Useless Charm
    21 => Defence Charm
    22 => Health Charm
    23 => Heavy Attack Damage Charm
    24 => Light Attack Damage Charm
    25 => Money Charm
    26 => Heavy Attack Rate Charm
    27 => Light Attack Rate Charm
     */

    public string title;
    public int baseCost;
    public Sprite image;
    public string description;
}
    