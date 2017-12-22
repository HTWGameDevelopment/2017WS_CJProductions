using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelection : MonoBehaviour {

    public void Back ()
    {
        SceneManager.LoadScene(0);
    }

    public void DemoLevel ()
    {
        SceneManager.LoadScene(4);
    }
	
}
