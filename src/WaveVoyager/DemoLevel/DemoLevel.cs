using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoLevel : MonoBehaviour {

    private bool paused;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject finishPanel;
    public GameObject endTrigger;
    public Slider volume;
    public AudioSource song;

	// Use this for initialization

	void Start () {
        paused = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        Cursor.visible = true;
        finishPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame

    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (finishPanel.active == true)
        {
            paused = true;
        }

        if (paused)
        {
            song.Pause();
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Cursor.visible = true;

        } else if (!paused)
        {
            song.UnPause();
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            Cursor.visible = false;
        }

        song.volume = volume.value;
	}

    //Pause Menu functions

    public void Resume ()
    {
        paused = false;
    }

    public void Restart ()
    {
        SceneManager.LoadScene(4);
    }

    public void Settings ()
    {
        settingsPanel.SetActive(true);
    }

    public void Exit ()
    {
        SceneManager.LoadScene(0);
    }

    //Settings Menu functions

    public void SettingsApply()
    {
        settingsPanel.SetActive(false);
    }

    //Finish functions

    public void SongSelection ()
    {
        SceneManager.LoadScene(1);
    }
}
