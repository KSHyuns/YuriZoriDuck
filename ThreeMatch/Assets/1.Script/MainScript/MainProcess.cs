using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProcess : MonoBehaviour
{




    /// <summary>
    /// 퍼즐게임
    /// </summary>
    public void Go_puzzleGame()
    {
        //퍼즐게임 씬이 따로있슴
        GameManager.Instance.LevelChange("GameScene");

    }


    /// <summary>
    /// 저장고
    /// </summary>
    public void Go_StoreRoom()
    {
        //저장고 확인용 씬
        GameManager.Instance.LevelChange("StoreRoomScene");
    }


    /// <summary>
    /// 요리하기(합성)
    /// </summary>
    public void Go_Cooking()
    {
        //합성하는 장면 씬
        GameManager.Instance.LevelChange("FarmScene");
    }

}
