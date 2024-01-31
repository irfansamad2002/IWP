using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideMouseOnFocus : MonoBehaviour
{
    private bool isCursorVisible = false;
    [SerializeField] private Transform defaultLocaltionForMouse;

    private void Start()
    {
        HideCursor();
    }

    private void OnApplicationFocus(bool focus)
    {
        SetCursorState();
    }

    private void SetCursorState()
    {
        if (isCursorVisible)
        {
            Cursor.lockState = CursorLockMode.Confined;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ToggleCursorVisibility()
    {
        isCursorVisible = !isCursorVisible;
        Cursor.visible = isCursorVisible;
        SetCursorState();
    }

    public void ShowCursor()
    {
        isCursorVisible = true;
        Cursor.visible = true;
        SetCursorState();

    }

    public void HideCursor()
    {
        isCursorVisible = false;
        Cursor.visible = false;
        SetCursorState();

    }


}
