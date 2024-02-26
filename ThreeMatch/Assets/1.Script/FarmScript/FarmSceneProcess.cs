using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.FlowStateWidget;
using Sequence = DG.Tweening.Sequence;

public class FarmSceneProcess : MonoBehaviour
{
    
    public CookingData cookingData;

    public Transform parent;

    //저장용 
    [System.NonSerialized] public FarmCookingSlot farmFoodData;

    public  List<curAction> eventList = new List<curAction>(); 
    //합성 패널
    public RsWindow rs_Window;

    public RsFood rs_food;

    public void Awake()
    {
        rs_Window.transform.localScale = Vector3.zero;
        rs_Window.close.onClick.AddListener(() => 
        { 
            rs_Window.transform.DOScale(0, 0.2f); 
            eventList.Clear(); 
        });

        for (int i = 0; i < cookingData.finishCooking.Count; i++)
        {
            var ob = Instantiate(cookingData.FarmCookingSlotPrefabs , parent);

            ob.getimg("cooking").sprite = cookingData.finishCooking[i].img;
            ob.getText().gameObject.SetActive(false);
            ob.Getbtn().onClick.AddListener(openWindow);
        }
    }




    public void openWindow()
    {
        //합성 가능 불가능 
        bool b_goCooking = true;
        //만들 음식의 idx 번호 
        int idx = farmFoodData.idx;


        //만들 음식의 이미지 데이터 
        Cooking cooking = cookingData.finishCooking[idx];

        //저장소 데이터 
        var storeRoom = GameManager.Instance.gameData.storeRoom;

        //window의 메인이미지와 음식이름Text 
        rs_Window.foodImg.sprite = cooking.img;
        rs_Window.foodname.text = cooking.name;

        #region 갯수가 0보다 낫은 요리 재료를 찾아서 딜리트

        while (true)
        {
            //갯수가 0보다 작은 아이템을 찾는다.
            var item = storeRoom.addItems.Find(x => x.itemCnt <= 0);
            //못찾았다면 0이된 아이템이 없는것으로 판단 (탈출)
            if (item == null) break;
            //찾은 아이템의 idx 번호 검출
            int id = storeRoom.addItems.IndexOf(item);
            //못찾았다면 탈출
            if (id == -1)
                break;
            else
            {
                //찾았다면 찾아서 지움 
                storeRoom.addItems.RemoveAt(id);
            }
        }


        #endregion

        //음식의 재료 갯수만큼 반복
        for (int i = 0; i < cooking.ingredients.Count; i++)
        {
            //해당 음식 검출
            var ingredient = cooking.ingredients[i];
            //이미지 -> sprite 데이터 넘겨줌
            rs_Window.idts[i].img.sprite = ingredient.img;

            //재료(열거)에 맞는 아이템 검출 
            var data = storeRoom.addItems.Find(x => x.kind == ingredient.kind);

            //index 번호 검출 
            int data_idx = storeRoom.addItems.IndexOf(data);

            // -1 이 아니면 검출 성공
            bool searchIdx = data_idx != -1 ? true : false;


            if (searchIdx)
            {
                rs_Window.idts[i].text.color = Color.white;
                rs_Window.idts[i].text.text = $"{ingredient.cnt} / {storeRoom.addItems[data_idx].itemCnt}";

                eventList.Add(new curAction() { item = storeRoom.addItems[data_idx], curCnt = ingredient.cnt });
            }
            else //검출 실패 
            {
                rs_Window.idts[i].text.color = Color.red;
                rs_Window.idts[i].text.text = $"X";
                b_goCooking = false;
            }
        }

        #region 요리하기 버튼 활성 ( 1개 이상 재료가 충족할때만) 
        rs_Window.goCooking.interactable = b_goCooking;
        #endregion


        //팝업창 띄우기
        rs_Window.transform.DOScale(0.8f, 0.2f);

        //연결된 리스너를 지움 
        rs_Window.goCooking.onClick.RemoveAllListeners();
        //팝업창의 버튼 클릭이벤트 
        rs_Window.goCooking.onClick.AddListener( () =>
        {
            rs_food.goCookingClickEvent(cooking.img, cooking.name);
            rs_Window.popupWindowScale();
            rs_food.popupWindowScale(eventList);
        });

       
    }
}
