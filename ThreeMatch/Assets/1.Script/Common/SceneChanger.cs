using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
    public SceneChanger(){}


    public void Init(string sceneName , Action complete = null)
    {
        SceneManager.LoadSceneAsync(sceneName).completed += (ao)=>{complete?.Invoke();};
    }

    public void Init(int sceneIdx , Action complete = null)
    {
        SceneManager.LoadSceneAsync(sceneIdx).completed += (ao)=>{complete?.Invoke();};
    }
}
