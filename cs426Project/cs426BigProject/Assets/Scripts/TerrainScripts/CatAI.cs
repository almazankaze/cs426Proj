using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour {
    public float speed;             // movement speed
    private float wait;             // how long to stay at position
    public float startWaitTime;     // how long to stay at position
    private bool isWaiting = false;

    public Transform[] spots;       // contains all spots where object will move to
    private int randomSpot;

    public GameObject cat;          //cat game object
    public AudioClip meow;          //Audio clip for the cat

    // Start is called before the first frame update
    void Start() {
        wait = startWaitTime;

        // get number between 0 and length of array. This number represents a random location to move to
        randomSpot = Random.Range(0, spots.Length);

        StartCoroutine(MeowSound()); //Calls the Meow sound function on start
    }//End of Start

    // Update is called once per frame
    void Update() {
        // move towards new spot
        transform.position = Vector3.MoveTowards(transform.position, spots[randomSpot].position, speed * Time.deltaTime);

        transform.LookAt(spots[randomSpot]);

        // checks if AI has arrived at the new spot
        if (Vector2.Distance(transform.position, spots[randomSpot].position) < 0.2f) {
            // if done waiting
            if (wait <= 0) {
                // get new location AI will move to
                randomSpot = Random.Range(0, spots.Length);
                wait = startWaitTime;
                isWaiting = false;
            }//End of if statement

            // else keep waiting at spot
            else {
                wait -= Time.deltaTime;
                isWaiting = true;
            }//End of else statement
        }

        //Get animations to work while idle and walking
        if (isWaiting == true) {
            cat.GetComponent<Animator>().Play("Idle");
        }//End of if statement
        else {
            cat.GetComponent<Animator>().Play("Walk");
        }//End of else statement
    }//End of update

    //Function to play the sound
    public IEnumerator MeowSound() {
        while (true) {
            AudioSource.PlayClipAtPoint(meow, this.gameObject.transform.position);
            yield return new WaitForSeconds(35);
        }//End of while loop
    }//End of IEnumerator
}//End of CatAI 
