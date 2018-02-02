using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance;
    private float lerpTime;
    private float currentLerpTime;
    private float songLength;
    public AudioSource song;
    private float progress;
    public Slider progressBar;

    private void Start()
    {
        progressBar.value = 0;
        lerpTime = getSongLength();
        distance = lerpTime * 100f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        startPos = player.transform.position;
        endPos = player.transform.position + Vector3.forward * distance;
    }

    private void Update()
    {
        progress = (((song.time) + 5f) * 100f) / ((song.clip.length +5f) * 100f);
        progressBar.value = progress;

        currentLerpTime += Time.deltaTime;
        if(currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float percentage = currentLerpTime / lerpTime;
        player.transform.position = Vector3.Lerp(startPos, endPos, percentage);
        transform.Rotate(0, 0, Time.deltaTime * 20);
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    float getSongLength()
    {
        songLength = song.clip.length +5f;
        return songLength;
    }
}
