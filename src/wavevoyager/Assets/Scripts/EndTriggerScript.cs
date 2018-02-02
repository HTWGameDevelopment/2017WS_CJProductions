using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTriggerScript : MonoBehaviour {

    public string sceneEndscreen;

    private void OnTriggerEnter(Collider other)
    {
        Invoke("ChangeScene", 4f);
    }

    public void ChangeScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(sceneEndscreen);
    }
}