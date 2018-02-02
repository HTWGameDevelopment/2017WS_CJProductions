using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class ObstacleTriggerScript : MonoBehaviour {

    public PostProcessingProfile CC;
    public Collider player;
    public int intScore = 0;
    public Text scoreUI;
    public AudioSource song;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player")
        {
            intScore = intScore + 1 * 2;
            scoreUI.text = intScore.ToString();
            var threshhold = CC.bloom.settings;
            threshhold.bloom.threshold = 0.9f;
            CC.bloom.settings = threshhold;
            Invoke("reduceBloom", 0.1f);
        }
    }

    void reduceBloom()
    {
        var threshhold = CC.bloom.settings;
        threshhold.bloom.threshold = 1.3f;
        CC.bloom.settings = threshhold;
    }
}
