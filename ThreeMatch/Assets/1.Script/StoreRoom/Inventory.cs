using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public int SlotCnt;

    public List<AddItem> addItems;

    public Inventory(int _slotcnt)
    { 
        addItems = new List<AddItem>();
        SlotCnt = _slotcnt;
    }


    public async UniTask ItemAdd(AddItem getItem)
    {
        if (addItems.Count > SlotCnt)
        {
            Debug.Log($"저장고에 비어있는 공간이 없습니다.");
            return;
        }

        AddItem item = new AddItem() {itemSprite = getItem.itemSprite , kind = getItem.kind , itemCnt = 1 };

        //수집된 아이템이없으면 새로 생성
        if (addItems.Count < 1)
        {
            Debug.Log("없슴");
            addItems.Add(item);
        }
        //수집된 아이템이 있으면 
        else
        {
            //해당 아이템을 찾음
            var it = addItems.Find(x => x.kind == item.kind);

            //해당 아이템이 있으면 갯수증가
            if (it != null)
            {
                // 아이템이 있는 리스트의 인덱스 번호 찾기
                int idx = addItems.IndexOf(it);
                // 찾은 인덱스 번호로 갯수 증가 시킴
                Debug.Log($" 해당 idx = {idx}");
                addItems[idx].itemCnt++;
            }

            //해당 아이템이 없으면 새로 생성
            if (it == null)
            {
                addItems.Add(item);
            }

            await UniTask.Yield();
        }
    }


}

[System.Serializable]
public class AddItem
{
    public CookingKind kind;
    public Sprite itemSprite;
    public int itemCnt;
}