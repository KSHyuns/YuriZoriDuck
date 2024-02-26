using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    // fadeOut ??canvasGroup
    public CanvasGroup fadeGroup;

    // fadeOut 
    public FadeOut fadeOut;

    // SceneChange  
    public SceneChanger sceneChanger;

    //이메일보내기 
    public EmailSender emailSender;

   //세이브 로드용 게임데이터
    public GameData gameData;
    
  

    public override void Awake()
    {
        base.Awake();
        //뒤끝 초기화
        //ServerManager.BackEndInitialize();


        //로드 된게 없다면 
        gameData = new GameData() { nickname = "Test", storeRoom = new Inventory(16), lifeCnt = 5 };

        //로드된게 있다면
     //   gameData = new GameData() { nickname = "Test", storeRoom = storeRoom, lifeCnt = 5 };
        //로드된 데이터로 적용



        // var newData = new winnerSheet.Data();
        // newData.index = 0;
        // newData.nickname = "Gameple";
        // newData.address = "myAddress";
        // UnityGoogleSheet.Write(newData);

#if UNITY_ANDROID
        Login.LoginProcess(Init);
        #endif

    }


    private void OnGUI() 
    {
        GUI.Label(new Rect(20,20 ,200 ,500) , Login.st);  
        
    }


    private void Init()
    {
        sceneChanger = new SceneChanger();
        fadeOut = new FadeOut(fadeGroup);
    }


    public void LevelChange(string sceneName)
    {   
        fadeGroup.blocksRaycasts = true;
        fadeOut.Out(
        ()=>
        { 
            sceneChanger.Init(sceneName , ()=>{ fadeOut.In(fadeGroup); });
        });
    }



   
}
