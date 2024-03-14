using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RsWindow : MonoBehaviour
{
    public int idx;

    public Image foodImg;

    public TextMeshProUGUI foodname;

    public RsIngredients[] ingredients;

    public Button goCooking;

    public Button close;

    public Transform ingredientParent;

    public void popupWindowScale()
    {
        SoundManager.Instance.Sound_Play("Popup", false, Property.SFX);
        transform.DOScale(0, 0.2f);
    }


    public void IngredReset()
    {
        if (ingredients.Length > 0)
        {
            foreach (var item in ingredients)
            {
                Destroy(item.gameObject);
            }
            ingredients = null;
        }
    }

}

