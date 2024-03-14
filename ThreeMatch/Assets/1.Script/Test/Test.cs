using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   public void EmailSender()
   {
        GameManager.Instance.emailSender.Send();
   }


    public void GameStart()
    {
        GameManager.Instance.LevelChange("GameScene");
    }

}
