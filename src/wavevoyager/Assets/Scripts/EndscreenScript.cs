using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndscreenScript : MonoBehaviour
{
    public string sceneGameplay;
    public string sceneSongSelection;
    public string sceneMainMenu;

    public void Retry()
    {
        SceneManager.LoadScene(sceneGameplay);
    }

    public void SongSelection()
    {
        SceneManager.LoadScene(sceneSongSelection);
    }

    public void Exit()
    {
        SceneManager.LoadScene(sceneMainMenu);
    }
}