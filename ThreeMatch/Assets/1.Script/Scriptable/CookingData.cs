using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookingData", menuName = "Scriptable/CookingData", order = 1)]
public class CookingData : SerializedScriptableObject 
{
    [Header("요리 데이터 Sprite , Enum")]
    // 완성요리
    public List <Cooking> finishCooking = new List <Cooking>();



    [Header("프리팹")]
    //인벤토리 슬롯프리팹
    public Slot slotPrefabs;

    //인벤토리 아이템프리팹
    public Item itemPrefabs;

    //Farm Slot 프리팹
    public FarmCookingSlot FarmCookingSlotPrefabs;

    //요리합성 시 요리 재료 칸 프리팹
    public RsIngredients ingredientsPrefabs;
    //ex
    public SlotEx slotExPrefabs;

}


[System.Serializable]
public class Cooking
{
    public string name;
    public Sprite img;

    public List<ingredient> ingredients = new List<ingredient>();
}
[System.Serializable]
public struct ingredient
{
    public CookingKind kind;
    public Sprite img;
    public int cnt;
}