using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI healthStat;
    public TextMeshProUGUI lightAttackStat;
    public TextMeshProUGUI lightAttackRateStat;
    public TextMeshProUGUI heavyAttackStat;
    public TextMeshProUGUI heavyAttackRateStat;
    public TextMeshProUGUI defenceStat;

    // Start is called before the first frame update
    void Start() { 

    }

    // Update is called once per frame
    void Update()
    {
        healthStat.text = "Health Points:" + "\n" + Player.currentHealth.ToString() + " / " + Player.maxHealth.ToString();
        lightAttackStat.text = "Light Attack Damage:" + "\n" + Player.lightAttackDamage;
        lightAttackRateStat.text = "Light Attack Rate:" + "\n" + Player.lightAttackRate;
        heavyAttackStat.text = "Heavy Attack Damage:" + "\n" + Player.heavyAttackDamage;
        heavyAttackRateStat.text = "Heavy Attack Rate:" + "\n" + Player.heavyAttackRate;
        defenceStat.text = "Defence (% of dmg reduction):" + "\n" + Player.defence;
    }
}
