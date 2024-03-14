using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Unity.Collections;
public class FadeOut
{
    private CanvasGroup canvasGroup;


    public FadeOut(CanvasGroup _canvasGroup)
    {
        canvasGroup = _canvasGroup;
    }


    /// <summary>
    /// ?ъ? ?대???㈃ ??㈃??蹂댁?吏??.
    /// </summary>
    /// <param name="complete"></param>
    public void In(CanvasGroup fadeGroup)
    {
        Sequence sequence = DOTween.Sequence().OnStart(()=>{}).
        Append(DOTween.To(()=> canvasGroup.alpha , x=> canvasGroup.alpha = x , 0,1.5f))
        .AppendCallback(()=>{fadeGroup.blocksRaycasts = false;});
    }


    /// <summary>
  
    /// ??㈃??媛?ㅼ?硫??ъ? ?대????.
    /// </summary>
    /// <param name="complete"></param>
    public void Out(Action complete = null)
    {
        Sequence sequence = DOTween.Sequence().OnStart(()=>{}).
        Append(DOTween.To(()=> canvasGroup.alpha , x=> canvasGroup.alpha = x , 1,1)).
        AppendCallback(()=>{ complete?.Invoke(); });

    }
}
