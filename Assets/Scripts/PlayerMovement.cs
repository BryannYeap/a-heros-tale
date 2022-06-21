using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public PlayerCombat playerCombat;

    public float runSpeed = 40f;

    [SerializeField]
    private bool crouchActive = true;
    [SerializeField]
    private bool jumpActive = true;
    [SerializeField]
    private bool combatActive = true;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool inventoryOpen = false;
    bool settingsOpen = false;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {

    } 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsOpen)
        {
            SetSettingsOpen(true);
            GameObject.Find("Canvas").transform.Find("PAUSED").transform.Find("PauseMenu").gameObject.SetActive(true);
            GameMaster.PauseGame();
            Cursor.visible = true;
        }

        if (canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && jumpActive)
            {
                jump = true;
            }

            if (Input.GetButtonDown("Crouch") && crouchActive)
            {
                crouch = true;
                GetComponent<Animator>().SetBool("Crouching", true);
            }
            else if (Input.GetButtonUp("Crouch") && crouchActive)
            {
                crouch = false;
                GetComponent<Animator>().SetBool("Crouching", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Player>().CheckInteraction();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryOpen)
            {
                // Lock cursor
                Cursor.visible = false;

                foreach (InventorySlot inventorySlot in GameObject.Find("Inventory").GetComponent<Inventory>().inventorySlots)
                {
                    inventorySlot.HideDeleteButton();
                    inventorySlot.useButton.SetActive(false);
                }
                GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(false);
                inventoryOpen = false;

            } else
            {
                GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(true);
                inventoryOpen = true;
            }
        }

        if (playerCombat.EnoughTimePassed() && combatActive)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                playerCombat.LightAttack();
            }
            if (Input.GetMouseButtonDown(1))
            {
                playerCombat.HeavyAttack();
            }
        }

        if (inventoryOpen)
        {
            //Unlock cursor
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void SetJump(bool isActive)
    {
        jumpActive = isActive;
    }

    public void SetCrouch(bool isActive)
    {
        crouchActive = isActive;
    }

    public void SetCombat(bool isActive)
    {
        combatActive = isActive;
    }

    public void SetMove(bool isActive)
    {
        canMove = isActive;
    }

    public void SetSettingsOpen(bool isOpen)
    {
        settingsOpen = isOpen;
    }
}
