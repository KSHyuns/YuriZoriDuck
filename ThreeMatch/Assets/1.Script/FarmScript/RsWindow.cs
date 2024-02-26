using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RsWindow : MonoBehaviour
{
    public int idx;

    public Image foodImg;

    public TextMeshProUGUI foodname;

    public idts[] idts;

    public Button goCooking;

    public Button close;
}

[System.Serializable]
public struct idts
{
    public Image img;
    public TextMeshProUGUI text;
}