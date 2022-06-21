using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameObject player;
    public GameObject dialogueBox;

    public static int currentScore = 0;
    public int currentCoins = 0;

    public static int coinsAtStart = 0;
    public static int healthAtStart = -1;

    public static Transform checkPoint;

    public static int costOfLoading = 5;

    private static bool gameHasEnded = false;
    private static bool canLoad = false;

    public static ShopItemSO[] staticInventoryItems;

    private GameObject cinemachine;

    private void Awake()
    {
        Cursor.visible = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        switch (PlayerPrefs.GetInt("selectedCharacterClass"))
        {
            case 0:
                foreach (GameObject character in players)
                {
                    if (character.name != "Warrior")
                    {
                        character.tag = "Untagged";
                        character.SetActive(false);
                    }
                }
                break;
            case 1:
                foreach (GameObject character in players)
                {
                    if (character.name != "Archer")
                    {
                        character.tag = "Untagged";
                        character.SetActive(false);
                    }
                }
                break;
            case 2:
                foreach (GameObject character in players)
                {
                    if (character.name != "Mage")
                    {
                        character.tag = "Untagged";
                        character.SetActive(false);
                    }
                }
                break;
        }

        cinemachine = GameObject.FindGameObjectWithTag("CM");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        cinemachine.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
        dialogueBox.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        dialogueBox.transform.Find("EToContinue").GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        player.transform.Find("InteractableIcon").GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

        Inventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<Inventory>();
        if (staticInventoryItems != null) // If last scene had inventory 
        {
            inventory.inventoryItems = staticInventoryItems;
            inventory.LoadSlots();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentCoins = currentScore;

        foreach (CoinManager coinManager in FindObjectsOfType<CoinManager>())
        {
            coinManager.setCoins(currentScore);
        }

        if (gameHasEnded)
        {
            if (Input.GetKeyDown(KeyCode.R)) {
                Restart();
                gameHasEnded = false;
            } else if (Input.GetKeyDown(KeyCode.L) && canLoad)
            {
                LoadGame();
                gameHasEnded = false;
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                QuitGame();
                gameHasEnded = false;
            }
        }

    }

    public static void SetCoins(int coins)
    {
        currentScore = coins;
    }

    public static void SetCoinsAtStart(int coins)
    {
        coinsAtStart = coins;
    }

    public static void SetHealthAtStart(int health)
    {
        healthAtStart = health;
    }

    public static void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
    }

    public static void ResumeGame()
    {
        Debug.Log("Game Resumed");
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    public static void FreezeControls()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    public static void UnfreezeControls()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public static void SetCheckpoint(Transform newCheckPoint)
    {
        checkPoint = newCheckPoint;
    }

    public static void SetCanLoad(bool canILoad)
    {
        canLoad = canILoad;
    }

    public static void SetGameEnded()
    {
        gameHasEnded = true;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        currentScore = coinsAtStart;
        Player.currentHealth = healthAtStart == -1 ? Player.maxHealth : healthAtStart;

        GameObject.Find("Canvas").transform.Find("Game Over Box").gameObject.SetActive(false);
        checkPoint = null;
        ResumeGame();
    }

    public static void LoadGame()
    {
        currentScore -= costOfLoading;

        GameObject.Find("Canvas").transform.Find("Game Over Box").gameObject.SetActive(false);

        player.transform.position = checkPoint.position;
        player.GetComponent<Player>().GainHealth(Player.maxHealth * 2);

        // Lock cursor
        Cursor.visible = false;

        ResumeGame();
    }

    public static void QuitGame()
    {
        // RESET PORTION
        checkPoint = null;

        Player.maxHealth = 0;
        Player.currentHealth = 0;

        Player.heavyAttackRate = 0;
        Player.heavyAttackDamage = 0;

        Player.lightAttackRate = 0;
        Player.lightAttackDamage = 0;

        Player.defence = 0f;

        currentScore = 0;

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Inventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").gameObject.GetComponent<Inventory>();
            for (int i = 0; i < inventory.inventoryItems.Length; i++)
            {
                inventory.RemoveItem(i);
            }
        }

        SceneManager.LoadScene(0);
        ResumeGame();
        GameObject.Find("Canvas").transform.Find("Game Over Box").gameObject.SetActive(false);

    }
}

