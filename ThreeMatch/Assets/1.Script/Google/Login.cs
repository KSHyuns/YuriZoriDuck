using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using System;
using UnityEngine.SocialPlatforms;


public class Login
{

    public static string st;

    public static void LoginProcess(Action action = null)
    {
        Social.localUser.Authenticate((success)=>
        {
            if(success)     //로그인 성공
            {
                Debug.Log($"{Social.localUser.userName} 님 환영합니다.");
                Debug.Log($"당신의 ID 는  {Social.localUser.id} 입니다");
                st = $"{Social.localUser.userName} 님 환영합니다.";
                action?.Invoke();
            }
            else //로그인 실패
            {
                Debug.Log("로그인 실패");
                st = "로그인 실패";
                action?.Invoke();
            }
            
        });
    }

    public static void LogOut()
    {
       //((PlayGamesPlatform)Social.Active).SignOut();
    }







   
}
