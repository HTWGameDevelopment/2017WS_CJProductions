using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SongSelectionScript : MonoBehaviour {

    public void StartSong(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void BackToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
