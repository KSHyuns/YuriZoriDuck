using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class ServerManager
{

#region 구글 해쉬키

/// <summary>
/// 해쉬키 뽑아오기
/// </summary>
/// <returns></returns>
public static string hashKey()
{
   return Backend.Utils.GetGoogleHash();
}
#endregion



#region 뒤끝 초기화
   /// <summary>
   /// 뒤끝 초기화
   /// </summary>
   public static void BackEndInitialize()
   {
      //뒤끝 초기화
      var bro = Backend.Initialize(true);
   
      //초기화 응답값
      if(bro.IsSuccess())
      {
         //초기화 성공시 statusCode : 204
         Debug.Log($"초기화 성공 {bro}");
      }
      else
      {
         //초기화 실패시 statusCode : 400
         Debug.LogError($"초기화 실패{bro}");
      }
   }
#endregion




#region 뒤끝 회원가입

#endregion

#region  뒤끝 로그인

#endregion





}
