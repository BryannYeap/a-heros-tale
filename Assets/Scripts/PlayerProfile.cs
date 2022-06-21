using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetInteger("Character Class", PlayerPrefs.GetInt("selectedCharacterClass"));
    }
}
