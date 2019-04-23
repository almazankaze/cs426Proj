using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPatrol : MonoBehaviour
{

    public float speed;             // movement speed
    private float wait;             // how long to stay at position
    public float startWaitTime;     // how long to stay at position
    private bool isWaiting = false;

    public Transform[] spots;       // contains all spots where object will move to
    private int randomSpot;

    public GameObject bird;

    // Start is called before the first frame update
    void Start()
    {
        wait = startWaitTime;

        // get number between 0 and length of array. This number represents a random location to move to
        randomSpot = Random.Range(0, spots.Length);
    }

    // Update is called once per frame
    void Update()
    {

        // move towards new spot
        transform.position = Vector3.MoveTowards(transform.position, spots[randomSpot].position, speed * Time.deltaTime);

        transform.LookAt(spots[randomSpot]);

        // checks if AI has arrived at the new spot
        if (Vector2.Distance(transform.position, spots[randomSpot].position) < 0.2f)
        {
            // if done waiting
            if (wait <= 0)
            {
                // get new location AI will move to
                randomSpot = Random.Range(0, spots.Length);
                wait = startWaitTime;
                isWaiting = false;
            }

            // else keep waiting at spot
            else
            {
                wait -= Time.deltaTime;
                isWaiting = true;
            }
        }

        if(isWaiting == true)
            bird.GetComponent<Animator>().Play("IdleState");
        else
            bird.GetComponent<Animator>().Play("FlyingState");
    }
}
