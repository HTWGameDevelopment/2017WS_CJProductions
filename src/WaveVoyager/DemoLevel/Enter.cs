using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter : MonoBehaviour {

    public GameObject finishPanel;

    private void OnTriggerEnter(Collider other)
    {
        Cursor.visible = true;
        finishPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
