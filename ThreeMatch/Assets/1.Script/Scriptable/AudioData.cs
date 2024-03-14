using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable/AudioData", order = 2)]
public class AudioData : SerializedScriptableObject
{
    //clip
    //1. BGM
    //2. SFX

    public Dictionary<string, SOUND> data = new Dictionary<string, SOUND>();

}

[System.Serializable]
public struct SOUND
{ 
    public Property AudioProperty;
    public AudioClip AudioClip;
}


public enum Property
{ 
    BGM , SFX , MAX
}

