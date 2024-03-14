using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainProcess : MonoBehaviour
{
    [SerializeField] private Slider BGM_slider;
    [SerializeField] private Slider SFX_slider;
    [SerializeField] private Button gameOverBtn;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Button settingBtn;

    [SerializeField] private GameObject SettingWIndow;

    

    private Action ClickEvent = () => { };


    public TextMeshProUGUI nicnameTxt;

    public RawImage rawImage;


    private void Awake()
    {
        SettingWIndow.transform.localScale = Vector2.zero;

        ClickEvent = settingWindowOpen;
        settingBtn.onClick.AddListener(() => { ClickEvent?.Invoke(); });
        gameOverBtn.onClick.AddListener(GameOver);
        BGM_slider.onValueChanged.AddListener(SoundBgmVolume);
        SFX_slider.onValueChanged.AddListener(SoundSfxVolume);

        if (audioMixer.GetFloat("BGM", out float value)) BGM_slider.value = Mathf.Pow(10, (value / 20));

        if (audioMixer.GetFloat("SFX", out float value2)) SFX_slider.value = Mathf.Pow(10, (value2 / 20));

        SoundManager.Instance.Sound_Play("IntroMainBGM", true, Property.BGM);

        if (Social.localUser.authenticated)
        {
            nicnameTxt.text = Social.localUser.userName;
            rawImage.texture = Social.localUser.image;
        }

    }
  
    /// <summary>
    /// 퍼즐게임
    /// </summary>
    //퍼즐게임 씬
    public void Go_puzzleGame() => GameManager.Instance.LevelChange("GameScene");

    /// <summary>
    /// 저장고
    /// </summary>
    //저장고 확인용 씬
    public void Go_StoreRoom() => GameManager.Instance.LevelChange("StoreRoomScene");

    /// <summary>
    /// 요리하기(합성)
    /// </summary>
    //합성하는 장면 씬
    public void Go_Cooking() => GameManager.Instance.LevelChange("FarmScene");

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void GameOver()
    {
        Application.Quit();
    }

    /// <summary>
    /// BGM 사운드 슬라이더 조절이벤트
    /// </summary>
    /// <param name="volume"></param>
    private void SoundBgmVolume(float volume)
    {
        audioMixer.SetFloat("BGM" , Mathf.Log10(volume) *20);
    }

    /// <summary>
    /// SFX 사운드 슬라이더 조절이벤트
    /// </summary>
    /// <param name="volume"></param>
    private void SoundSfxVolume(float volume) 
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    //세팅 윈도우 오픈
    private void settingWindowOpen()
    {
        SettingWIndow.transform.DOScale(1f ,0.2f);
        SoundManager.Instance.Sound_Play("Popup" , false , Property.SFX);
        ClickEvent = settingWindowClose;
    }

    //세팅 윈도우 클로즈
    public void settingWindowClose()
    {
        SettingWIndow.transform.DOScale(0.0f, 0.2f);
        SoundManager.Instance.Sound_Play("Popup", false, Property.SFX);
        ClickEvent = settingWindowOpen;
    }


}
