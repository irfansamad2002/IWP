using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseOnFocus : MonoBehaviour
{
    private void OnApplicationFocus(bool focus)
    {
        SetCursorState(focus);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
