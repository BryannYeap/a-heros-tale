using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Scriptable Objects/New Character", order = 2)]
public class Characters : ScriptableObject
{
    public int characterClass;

    [TextArea(15,20)]
    public string stats;

    [TextArea(15, 20)]
    public string description;
}
