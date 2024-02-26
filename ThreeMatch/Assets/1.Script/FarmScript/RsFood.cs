using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RsFood : MonoBehaviour
{

    [SerializeField] private Image foodImg;

    [SerializeField] private TextMeshProUGUI foodNameText;

    [SerializeField] private Slider resultSlider;

    [SerializeField] private Button closeBtn;




    public Image mainImg() => foodImg;

    public TextMeshProUGUI foodName() => foodNameText;

    public Slider slider () => resultSlider;

    public Button close() => closeBtn;

    /// <summary>
    /// 슬라이더 진행
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public async UniTask sliderProcess(List<curAction> list)
    {
        Debug.Log("횟수?");

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.2f));
        DOTween.To(() => resultSlider.value, x => resultSlider.value = x, 1, 2).OnComplete(async () =>
        {
            Debug.Log("완");
            foreach (var item in list)
            {
                item.item.itemCnt -= item.curCnt;
            }
            Debug.Log("완 후");

        });
        await UniTask.Delay(System.TimeSpan.FromSeconds(3f));

        list.ForEach(x => x.item = null);
        list.Clear();
        closeBtn.gameObject.SetActive(true);

        await UniTask.Yield();
    }

    /// <summary>
    /// 버튼 클릭시 세팅
    /// </summary>
    /// <param name="mainImg"></param>
    /// <param name="name"></param>
    /// <param name="sliderValue"></param>
    public void goCookingClickEvent( Sprite mainImg , string name , float sliderValue = 0f)
    { 
        foodImg.sprite = mainImg;
        foodNameText.text = name;
        resultSlider.value = sliderValue;

        closeBtn.gameObject.SetActive(false);

        //닫기 버튼 클릭시 
        closeBtn.onClick.AddListener(() => 
        {
            transform.DOScale(0 , 0.2f);
        });
    
    }

    //팝업창 닫고 다음창열고 슬라이더 진행 
    public void popupWindowScale(Transform rs_Window ,List<curAction> list)
    {
        rs_Window.DOScale(0 , 0.2f);
        transform.DOScale(0.8f, 0.2f).OnComplete(() => 
        {
            sliderProcess(list).Forget();
        });
    }






}
