using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FollowPlayer : NetworkBehaviour {

    public GameObject player;
    public float movementSpeed = 2.0f;

    // Start is called before the first frame update
    void Start() {

    }//End of start function

    // Update is called once per frame
    void FixedUpdate() {

        player = GameObject.FindGameObjectWithTag("Player");

        // keep looking at player
        Vector3 lookTarget = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookTarget);

        // move towards player
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

    }//End of update function
}//End of FollowPlayer script
