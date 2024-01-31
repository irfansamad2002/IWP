using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] HideMouseOnFocus focus;

    private void Start()
    {
        focus.ShowCursor();
    }

    private void OnEnable()
    {
        focus.ShowCursor();
    }

    private void Awake()
    {
        focus.ShowCursor();

    }
    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
