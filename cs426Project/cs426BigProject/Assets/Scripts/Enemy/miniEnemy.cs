using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class miniEnemy : NetworkBehaviour
{
    public GameObject player;
    public float movementSpeed = 7.0f;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.2f;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // keep looking at player
        Vector3 lookTarget = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookTarget);

        // move towards player
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        anim.SetFloat("velocity", movementSpeed);

    }//End of update function
}
