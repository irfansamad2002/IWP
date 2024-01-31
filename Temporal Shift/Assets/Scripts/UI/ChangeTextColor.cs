using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    public Color hoverColorText;
    public Color orignalColorText;

    public Color hoverColorButton;
    public Color orignalColorButton;

    TMP_Text tmpText;
    [SerializeField] Image buttonImage;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void ChangeToHoverColor()
    {
        tmpText.color = hoverColorText;
        buttonImage.color = hoverColorButton;
    }

    public void BackToOrignalColor()
    {
        tmpText.color = orignalColorText;
        buttonImage.color = orignalColorButton;
    }

}
