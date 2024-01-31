using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeTextColor : MonoBehaviour
{
    public Color hoverColor;
    public Color orignalColor;

    TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void ChangeToHoverColor()
    {
        tmpText.color = hoverColor;
    }

    public void BackToOrignalColor()
    {
        tmpText.color = orignalColor;
    }

}
