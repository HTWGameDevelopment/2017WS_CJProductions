using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplaySpawner : MonoBehaviour {

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

    private void Start()
    {
        //Select the instance of SongAnalyzer and pass a reference
        //to this object
        SongAnalyzer analyzer = FindObjectOfType<SongAnalyzer>();
        analyzer.onBeat.AddListener(onOnbeatDetected);

        spawnObstacles(CreateSpawnLocationList(GetSongData()));
    }

    void spawnObstacles(List<Vector3> spawnLocations)
    {
        //for(int i=0; i < spawnLocations.Length; i++)
        foreach (Vector3 spawn in spawnLocations)
        {
            obstacleStyle = UnityEngine.Random.Range(0, 2);
            //Debug.Log(obstacleStyle);
            if (midWasSet == true && obstacleStyle == 0)
            {
                clones.Add(Instantiate(columnObstacles[UnityEngine.Random.Range(0, 3)], spawn, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))) as GameObject);
                midWasSet = false;
                //Debug.Log("Col");
            } else if (midWasSet == true && obstacleStyle == 1)
            {
                clones.Add(Instantiate(rowObstacles[UnityEngine.Random.Range(0, 3)], spawn, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))) as GameObject);
                midWasSet = false;
                //Debug.Log("Row");
            } else if ((midWasSet == false && obstacleStyle == 0) || midWasSet == false && obstacleStyle == 1)
            {
                clones.Add(Instantiate(midObstacles[UnityEngine.Random.Range(0, 1)], spawn, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))) as GameObject);
                midWasSet = true;
                //Debug.Log("Mid");
            }
        }
        Instantiate(endTrigger, new Vector3(0, 0, 1300), Quaternion.Euler(0, 0, 0));
    }

    List<float> GetSongData()
    {
        List<float> ts = new List<float>();

        ts.Add(455f);
        ts.Add(495f);
        ts.Add(672.5f);
        ts.Add(727.5f);
        ts.Add(782.5f);
        ts.Add(835f);
        ts.Add(1010f);
        ts.Add(1065f);
        ts.Add(1120f);
        ts.Add(1172.5f);
        ts.Add(1347.5f);
        ts.Add(1402.5f);
        ts.Add(1457.5f);

        return ts;
    }

    List<Vector3> CreateSpawnLocationList(List<float> timestamps)
    {
        foreach (float timestamp in timestamps)
        {
            spawnLocations.Add(new Vector3(0, 0, timestamp));
        }

        return spawnLocations;
    }

    //this event will be called every time a beat is detected.
    //Change the threshold parameter in the inspector
    //to adjust the sensitivity
    public void onOnbeatDetected()
    {
        float curMs = song.time * 100f;
        timestamps.Add(curMs);
        Debug.Log("Beat at " + curMs);
        spawnObstacles(CreateSpawnLocationList(timestamps));
    }

    //This event will be called every frame while music is playing
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
