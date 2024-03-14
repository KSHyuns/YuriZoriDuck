using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager :MonoBehaviour
{
    public CookingData cookingData;
    public static ObjectPool<SlotEx> ExPool;


    private void Awake()
    {
        SlotExPool();
    }


    private void SlotExPool( )
    {
        ExPool = new ObjectPool<SlotEx>
           (
               () =>
               {
                   var ex = Instantiate(cookingData.slotExPrefabs);
                   ex.ParticleSystem = ex.GetComponent<ParticleSystem>();
                   ex.Ipool = ExPool;
                   return ex;
               },
               (ex) => { ex.gameObject.SetActive(true); },
               (ex) => { ex.gameObject.SetActive(false); },
               (ex) => { Destroy(ex.gameObject); }, maxSize: 10
           );
    }
}
