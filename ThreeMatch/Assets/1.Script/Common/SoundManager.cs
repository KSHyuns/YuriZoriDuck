using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioData audioData;

    // BGM 관리 
    public AudioSource BGM_source;
    // SFX 관리
    public AudioSource SFX_source;

    public override void Awake() => base.Awake();

    public void Sound_Play(string key , bool loop   ,Property audioProperty = Property.SFX, float volume = 0.6f)
    {
        var data = audioData.data[key];
        if (audioProperty == Property.BGM)
        {
            if (data.AudioClip == BGM_source.clip) return;


            if(BGM_source.isPlaying) { BGM_source.Stop(); }
            BGM_source.loop = loop;
            BGM_source.volume = volume - 0.2f;
            BGM_source.clip = data.AudioClip;
            BGM_source.PlayOneShot(data.AudioClip);
        }
        else if(audioProperty == Property.SFX) 
        {
            SFX_source.loop = loop;
            SFX_source.volume = volume;
            SFX_source.PlayOneShot(data.AudioClip);
        }
    }

    public void Sound_SFX_Stop()
    {
        if(SFX_source.isPlaying) { SFX_source.Stop(); }
    }
}
