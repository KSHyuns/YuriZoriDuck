using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    public Button PrevBtn;

    private void Awake()
    {
        PrevBtn.onClick.AddListener(() => { GameManager.Instance.LevelChange("MainScene"); });
    }


}
