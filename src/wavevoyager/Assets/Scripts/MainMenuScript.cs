using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    public GameObject settingsCanvas;
    public GameObject exitPopup;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
    }

    public void OpenExitPopup()
    {
        exitPopup.SetActive(true);
    }

    public void CloseExitPopup()
    {
        exitPopup.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
