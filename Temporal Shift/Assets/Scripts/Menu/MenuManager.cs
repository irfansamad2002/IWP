using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

   
}
