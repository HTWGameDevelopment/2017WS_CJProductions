using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider volume;
    public AudioSource music;

    private void Start()
    {
        volume.value = 0.2f;
    }

    private void Update()
    {
        music.volume = volume.value;
    }
}
