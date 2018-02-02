using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameplayScript : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject settingsCanvas;
    public AudioSource song;
    private bool paused;

    void Start()
    {
        //song.PlayDelayed(5f+ Time.deltaTime);
        //BeatDetector detector = BeatDetector.Instance();
        //detector.loadSystem();
        //detector.LoadSong(1024, "D:/Programme/Unity/Projects/wavevoyager/Assets/Songs/Nimo - Lass mich wissen.mp3");
        Invoke("playDelayed", 5f);
        paused = false;
        //detector.setStarted(true);
        //detector.update();
    }

    void playDelayed()
    {
        song.Play();
    }

    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            paused = !paused;
            pauseMenu.SetActive(paused);
        }

        if (paused == true)
        {
            Time.timeScale = 0;
            song.Pause();
            Cursor.visible = true;
        }
        else
        {
            settingsCanvas.SetActive(false);
            Time.timeScale = 1;
            song.UnPause();
            Cursor.visible = false;
        }
	}

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }

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
}
