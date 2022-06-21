using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject InteractableIcon;
    public GameObject gameOver;

    // Health Fields
    public static int maxHealth;
    public static int currentHealth;
    public HealthBar healthBar;

    // Heavy Attack Fields
    public static float heavyAttackRate;
    public static int heavyAttackDamage;

    // Light Attack Fields
    public static float lightAttackRate;
    public static int lightAttackDamage;

    // Defence Fields
    public static float defence = 0f;

    public Vector2 boxSize = new Vector2(0.1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        if (maxHealth == 0)
        {
            switch (PlayerPrefs.GetInt("selectedCharacterClass"))
            {
                case 0: // Warrior
                    maxHealth = 100;
                    currentHealth = 100;

                    lightAttackDamage = 7;
                    lightAttackRate = 2f;

                    heavyAttackDamage = 12;
                    heavyAttackRate = 1.2f;

                    defence = 0.2f;
                    break;
                case 1: // Archer
                    maxHealth = 70;
                    currentHealth = 70;

                    lightAttackDamage = 5;
                    lightAttackRate = 1.15f;

                    heavyAttackDamage = 10;
                    heavyAttackRate = .9f;

                    defence = 0.15f;
                    break;
                case 2: // Mage
                    maxHealth = 85;
                    currentHealth = 85;

                    lightAttackDamage = 8;
                    lightAttackRate = 1.05f;

                    heavyAttackDamage = 15;
                    heavyAttackRate = .8f;

                    defence = 0.1f;
                    break;
            }
        }
 
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        healthBar.maxHealth.text = maxHealth.ToString();
        InteractableIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        healthBar.maxHealth.text = maxHealth.ToString();
        healthBar.currentHealth.text = currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= (int)(damage - (defence * damage));
        GetComponent<Animator>().SetTrigger("Hurt");
        GetComponent<CharacterController2D>().knockBackCount = GetComponent<CharacterController2D>().knockBackLength;

        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("0 hp");
        } else
        {
            FindObjectOfType<AudioManager>().Play("TakeDamage");
        }
    }

    public void GainHealth(int health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void OpenInteractableIcon()
    {
        InteractableIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        InteractableIcon.SetActive(false);
    }

    public void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");

        GameMaster.PauseGame();

        gameOver.SetActive(true); 
        GameMaster.SetGameEnded();

        //Unlock cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Transform loadCheckpointButton = gameOver.transform.Find("Load Checkpoint Button");

        if (GameMaster.currentScore < GameMaster.costOfLoading || GameMaster.checkPoint == null)
        {
            loadCheckpointButton.transform.Find("Load Button").GetComponent<Button>().interactable = false;
            loadCheckpointButton.transform.Find("CoinSprite").SetAsFirstSibling();
            loadCheckpointButton.transform.Find("CurrentCoins").SetAsFirstSibling();
            GameMaster.SetCanLoad(false);
        } else
        {

            loadCheckpointButton.transform.Find("Load Button").GetComponent<Button>().interactable = true;
            loadCheckpointButton.transform.Find("CoinSprite").SetAsLastSibling();
            loadCheckpointButton.transform.Find("CurrentCoins").SetAsLastSibling();
            GameMaster.SetCanLoad(true);
        }
    }
}
