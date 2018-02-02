using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    //Rotation
    public GameObject player;
    bool shouldRotate = false;
    bool isRotating = false;
    bool hadCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        //player.transform.Rotate(new Vector3(0,0,90));
        if (!isRotating)
        {
            isRotating = true;
            shouldRotate = true;
            hadCollision = true;
            Invoke("ReturnToNormal", 5f);
        }
    }

    void ReturnToNormal()
    {
        shouldRotate = false;
        player.transform.Rotate(new Vector3(0, 0, -90));
        isRotating = false;
    }

    private void Update()
    {
        if (shouldRotate && isRotating)
        {
            player.transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime));
        }
    }
}
