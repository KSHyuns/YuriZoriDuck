using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
   [SerializeField] private CookingKind cookingkind;
   [SerializeField] private Image cookingImage;
   [SerializeField] private TextMeshProUGUI cookingCnt;

    private int cnt;

    public int Cnt
    {
        get => cnt;
        set
        {
            cnt = value;
            cookingCnt.text = cnt.ToString();
        }
    }


    private void Awake()
    {
        if (transform.GetChild(0).TryGetComponent(out Image _cookingImage))
            cookingImage = _cookingImage;

        if (transform.GetChild(1).TryGetComponent(out TextMeshProUGUI _cooingCnt))
            cookingCnt = _cooingCnt;

    }


    public void Set(CookingKind kind ,Sprite sprite , int cnt)
    {
        cookingkind = kind;
        cookingImage.sprite = sprite;
        Cnt = cnt;
    }

}
