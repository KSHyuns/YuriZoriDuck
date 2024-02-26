using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EmailSender : MonoBehaviour
{
   
    public void Send()
    {
        string mailto = "vhstm2@gmail.com";                      //전송할 메일
        string subject = EscapeURL("요리 추첨자");                //메일 제목
        string body = EscapeURL("내 이름 및 주소");               //메일 내용
        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);

    }


   private string EscapeURL(string url)
   {
        return UnityWebRequest.EscapeURL(url).Replace("+" , "%20");
   }
}
