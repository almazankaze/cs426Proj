using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SunRotation : NetworkBehaviour {

    public Transform sun;
    public WinMessage winMess;
    private bool winner = false;
    public bool spawned = false;
    //public NetworkSpawnEnemy netSpwn;

    // Start is called before the first frame update
    void Start() {
        
    }//End of start

    // Update is called once per frame
    //Code to update sun and moon ratation
    void Update() {
        transform.RotateAround(Vector3.zero, Vector3.right, 1.5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);

        // if sun is at certain position, display win message
        if(sun.position.y >= 50 && sun.position.y <= 52 && sun.rotation.y == 0 && winner == false) {
            winMess.Winner();
            winner = true;
        }//End of if statement
        //if (sun.position.y <= -75 && sun.position.y >= -77 && sun.rotation.y == -180 && spawned == false) {
        //    //netSpwn.Spwn();
        //    spawned = true;
        //}

    }//End of update
}//End of SunRotation
