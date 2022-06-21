using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject heroPickingScreen;

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        heroPickingScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("We quit the game");
        Application.Quit();
    }

    public void Cancel()
    {
        heroPickingScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
