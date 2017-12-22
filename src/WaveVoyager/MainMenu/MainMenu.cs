using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject exitPopUp;
    public GameObject settingsPanel;

	public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void Shop ()
    {
        SceneManager.LoadScene(2);
    }

    public void ScoreBoard ()
    {
        SceneManager.LoadScene(3);
    }

    //Settings functions

    public void Settings ()
    {
        settingsPanel.SetActive(true);
    }

    public void SettingsApply ()
    {
        settingsPanel.SetActive(false);
    }

    // Exit functions

    public void Exit ()
    {
        exitPopUp.SetActive(true);
    }

    public void ExitYes ()
    {
        Application.Quit();
    }

    public void ExitNo ()
    {
        exitPopUp.SetActive(false);
    }
}