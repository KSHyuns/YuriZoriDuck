using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;


public class OneTabProcess : MonoBehaviour
{
    public UnityEvent Process;

    [SerializeField] private TextMeshProUGUI tap_text;

    DG.Tweening.Sequence sequence;
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += Down;
       
    }
    private void Start()
    {
        tapAnim();


        SoundManager.Instance.Sound_Play("IntroMainBGM", true, Property.BGM);
    }

    private void tapAnim()
    {
        sequence = DOTween.Sequence()
       .OnStart(() => { })
       .Append(tap_text.DOFade(0, 1))
       .Append(tap_text.DOFade(1, 1))
       .SetLoops(-1);
    }


    void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= Down;
        EnhancedTouchSupport.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.Sound_Play("Btn", false, Property.SFX);
            Process?.Invoke();
        }
        #endif
    }

    private void Down(Finger finger)
    {
        SoundManager.Instance.Sound_Play("Btn", false, Property.SFX);
        {
            Process?.Invoke();
        }
    }
}
