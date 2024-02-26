using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FarmCookingSlot : MonoBehaviour
{

    public int idx;

    private Button btn;
    
    private Image slotImg;

    private Image cookingImg;

    private TextMeshProUGUI state_text;

    private FarmSceneProcess process;

    [SerializeField] private CookingKind cookingKind;
    // Start is called before the first frame update
    private void Awake()
    {
        idx = transform.GetSiblingIndex();

        btn = GetComponent<Button>();

        slotImg = GetComponent<Image>(); 

        process = FindObjectOfType<FarmSceneProcess>();

        cookingImg = transform.GetChild(0).GetComponent<Image>();

        state_text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        btn.onClick.AddListener(onclick);
    }


    public Button Getbtn() => btn;

    public Image getimg(string st)
    { 
        if(st == "slot") return slotImg;
        if(st == "cooking") return cookingImg;

        return null;
    }

    public TextMeshProUGUI getText() => state_text;


    private void onclick()
    {
        process.farmFoodData = this;
    }
 }
