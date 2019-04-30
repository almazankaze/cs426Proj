using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class miniEnemy : NetworkBehaviour
{
    public GameObject[] players;
    public float movementSpeed = 7.0f;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.2f;

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (players.Length < 2)
        {
            // keep looking at player
            Vector3 lookTarget = new Vector3(players[0].transform.position.x, transform.position.y, players[0].transform.position.z);
            transform.LookAt(lookTarget);

            // move towards player
            transform.position += transform.forward * movementSpeed * Time.deltaTime;

            anim.SetFloat("velocity", movementSpeed);

        }

        else
        {
            float dist1 = Vector3.Distance(players[0].transform.position, transform.position);
            float dist2 = Vector3.Distance(players[1].transform.position, transform.position);

            if (dist1 <= dist2)
            {
                // keep looking at player
                Vector3 lookTarget = new Vector3(players[0].transform.position.x, transform.position.y, players[0].transform.position.z);
                transform.LookAt(lookTarget);

                // move towards player
                transform.position += transform.forward * movementSpeed * Time.deltaTime;

                anim.SetFloat("velocity", movementSpeed);

            }

            else
            {
                // keep looking at player
                Vector3 lookTarget = new Vector3(players[1].transform.position.x, transform.position.y, players[1].transform.position.z);
                transform.LookAt(lookTarget);

                // move towards player
                transform.position += transform.forward * movementSpeed * Time.deltaTime;

                anim.SetFloat("velocity", movementSpeed);

            }
        }

    }//End of update function
}
