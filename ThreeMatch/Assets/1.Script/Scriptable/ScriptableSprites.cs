using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableSprites" , menuName = "Scriptable/Sprites" , order = 0)]
public class ScriptableSprites : ScriptableObject
{
    public Sprite[] tileSprites;

    public Block block;

    public Cell cell;
}
