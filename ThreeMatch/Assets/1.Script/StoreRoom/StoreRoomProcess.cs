using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreRoomProcess : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI cur_Max_Cnt_Text;
    //슬롯 확장버튼
    public Button AddSlotBtn;
    //슬롯 생성 부모 트랜스폼
    public Transform slotParent;
    //요리 데이터
    public CookingData cookingData;

    public List<Item> items;

    public List<Slot> slots;


    private int slotCnt;

    public int SlotCnt
    { 
        get => slotCnt;
        set
        { 
            slotCnt = value;
            cur_Max_Cnt_Text.text = $"{GameManager.Instance.gameData.storeRoom.addItems.Count} / {slotCnt}";
        }
    }

    public Button PrevBtn;



    private void Awake()
    {
        items = new List<Item>();
        slots = new List<Slot>();

        StoreRoomSetting();
        AddSlotBtn.onClick.AddListener(AddSlot);

        PrevBtn.onClick.AddListener(() => { GameManager.Instance.LevelChange("MainScene"); });

        SoundManager.Instance.Sound_Play("StoreRoomBGM" , true , Property.BGM);
    }

    private void StoreRoomSetting()
    {
        #region 슬롯 생성

        SlotCnt = GameManager.Instance.gameData.storeRoom.SlotCnt;

        int cnt = GameManager.Instance.gameData.storeRoom.SlotCnt;
        for (int i = 0 ; i < cnt ; i++) 
        {
            var slot = Instantiate(cookingData.slotPrefabs, slotParent);
            slots.Add(slot);
        }
        #endregion

        #region 아이템슬롯 생성
        var additems = GameManager.Instance.gameData.storeRoom.addItems;
        for(int i=0;i < additems.Count; i++) 
        {
            var item = Instantiate(cookingData.itemPrefabs, slots[i].transform);
            items.Add(item);
        }
        #endregion

        #region 아이템 속성및 데이터 세팅
        for (int i = 0; i < additems.Count; i++)
        {
            items[i].Set(additems[i].kind, additems[i].itemSprite, additems[i].itemCnt);
        }
        #endregion
    }



    public void AddSlot()
    {
        SoundManager.Instance.Sound_Play("Btn" ,false , Property.SFX);

        GameManager.Instance.gameData.storeRoom.SlotCnt += 1;
        SlotCnt = GameManager.Instance.gameData.storeRoom.SlotCnt;

        var slot = Instantiate(cookingData.slotPrefabs, slotParent);
        slots.Add(slot);
    }


    private void OnDisable()
    {
        slots.Clear();
        items.Clear();
    }


}
