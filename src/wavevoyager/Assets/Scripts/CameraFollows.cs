using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour {

    public GameObject camera;
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance;
    private float lerpTime;
    private float currentLerpTime;
    public AudioSource song;

    private void Start()
    {
        Cursor.visible = false;
        lerpTime = song.clip.length + 5f;
        distance = lerpTime * 100f;

        startPos = camera.transform.position;
        endPos = camera.transform.position + Vector3.forward * distance;
    }

    private void Update()
    {
        float curMs = song.time;
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float percentage = currentLerpTime / lerpTime;
        camera.transform.position = Vector3.Lerp(startPos, endPos, percentage);
        transform.Rotate(0, 0, Time.deltaTime*20);
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
}
