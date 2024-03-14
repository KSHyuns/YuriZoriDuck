using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SlotEx : MonoBehaviour
{
    public IObjectPool<SlotEx> Ipool;
    public ParticleSystem ParticleSystem;
    
    public async UniTask Release()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(ParticleSystem.main.duration) , DelayType.UnscaledDeltaTime);
        Ipool.Release(this);
    }

    public async UniTask Release(SlotEx ex)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(ParticleSystem.main.duration), DelayType.UnscaledDeltaTime);
        Ipool.Release(ex);
    }

    

}
