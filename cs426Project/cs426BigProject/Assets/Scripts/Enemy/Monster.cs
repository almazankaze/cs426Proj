using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Monster : NetworkBehaviour
{

    public GameObject player;
    public float movementSpeed = 2.0f;
    private Animator anim;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.2f;

        movementSpeed = 0;
        enemy.SetActive(false);
        StartCoroutine(Spwn());
    }//End of start function

    // Update is called once per frame
    void FixedUpdate() {

        player = GameObject.FindGameObjectWithTag("Player");

        // keep looking at player
        Vector3 lookTarget = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookTarget);

        // move towards player
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        anim.SetFloat("velocity", movementSpeed);

    }//End of update function

    //Function to get the bear to spawn
    public IEnumerator Spwn() {
        yield return new WaitForSeconds(70);
        movementSpeed = 6f;
        enemy.SetActive(true);
    }//End of IEnumerator

}//End of FollowPlayer script
