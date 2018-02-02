using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {


    public List<float> timestamps;
    //public Vector3[] spawnLocations;
    public List<Vector3> spawnLocations;
    public GameObject[] columnObstacles;
    public GameObject[] rowObstacles;
    public GameObject[] midObstacles;
    public List<GameObject> clones;
    public GameObject endTrigger;
    public SongAnalyzer analyzer;
    public AudioSource song;

    private int obstacleStyle;
    private bool midWasSet = true;

    // Use this for initialization
    void Start () {
        clones.Add(Instantiate(endTrigger, new Vector3(0, 0, (song.clip.length + 1f) * 100f), Quaternion.Euler(0, 0, 0)));
    }
	 
	// Update is called once per frame
	void Update () {
		
	}

    void spawnObstacle(Vector3 spawnLocation)
    {
            obstacleStyle = UnityEngine.Random.Range(0, 2);

            if (midWasSet == true && obstacleStyle == 0)
            {
                clones.Add(Instantiate(columnObstacles[UnityEngine.Random.Range(0, 3)], spawnLocation, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))) as GameObject);
                midWasSet = false;
            }
            else if (midWasSet == true && obstacleStyle == 1)
            {
                clones.Add(Instantiate(rowObstacles[UnityEngine.Random.Range(0, 3)], spawnLocation, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))) as GameObject);
                midWasSet = false;
            }
            else if ((midWasSet == false && obstacleStyle == 0) || midWasSet == false && obstacleStyle == 1)
            {
                clones.Add(Instantiate(midObstacles[UnityEngine.Random.Range(0, 1)], spawnLocation, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))) as GameObject);
                midWasSet = true;
            }

            //Instantiate(endTrigger, new Vector3(0, 0, 1300), Quaternion.Euler(0, 0, 0));
    }

    public void OnBeatDetected()
    {
        float curMs = ((song.time + 5f) * 100f);
        curMs = curMs + 5;

        if (curMs > 0.001 && curMs < (((song.clip.length + 5f) * 100)))
        {
            spawnObstacle(new Vector3(0, 0, curMs));
            //Debug.Log("Beat at: " + curMs);
        }
    }

    public void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }
}