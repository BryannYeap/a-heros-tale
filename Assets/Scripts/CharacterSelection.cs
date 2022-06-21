using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Characters[] characters;
    public int selectedCharacterClass = 0;

    public Text characterStats;
    public Text characterDescription;

    private void Start()
    {
        SetProfile(characters[selectedCharacterClass]);    
    }

    public void NextCharacter()
    {
        selectedCharacterClass = (selectedCharacterClass + 1) % characters.Length;
        SetProfile(characters[selectedCharacterClass]);
        FindObjectOfType<AudioManager>().Play("Next Prev Button");
    }

    public void PreviousCharacter()
    {
        selectedCharacterClass--;
        if (selectedCharacterClass < 0)
        {
            selectedCharacterClass += characters.Length;
        }
        SetProfile(characters[selectedCharacterClass]);
        FindObjectOfType<AudioManager>().Play("Next Prev Button");
    }

    public void SetProfile(Characters character)
    {
        characterStats.text = character.stats;
        characterDescription.text = character.description;
        gameObject.transform.Find("Player Image").transform.Find("Animation").GetComponent<Animator>().SetInteger("Character Class", selectedCharacterClass);
    }

    public void ChooseHero()
    {
        FindObjectOfType<AudioManager>().Play("Start");
        PlayerPrefs.SetInt("selectedCharacterClass", selectedCharacterClass);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
