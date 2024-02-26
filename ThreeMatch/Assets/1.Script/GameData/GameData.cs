using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    //닉네임
    public string nickname;
    //인벤토리 아이템s
    public Inventory storeRoom;
    //생명수치
    public int lifeCnt;

    public GameData() { }

    public GameData(string _nickname, Inventory _storeRoom, int _lifeCnt)
    { 
        nickname = _nickname;
        storeRoom = _storeRoom;
        lifeCnt = _lifeCnt;
    }

}








