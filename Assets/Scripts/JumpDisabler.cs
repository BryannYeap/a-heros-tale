using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDisabler : MonoBehaviour
{
    public PlayerMovement player;
    public bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            player.SetJump(false);

            player.SetCrouch(false);
            player.SetCombat(false);
        }
    }
}
