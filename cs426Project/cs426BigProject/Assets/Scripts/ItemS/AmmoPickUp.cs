using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AmmoPickUp : NetworkBehaviour {

    public GameObject player;
    public AudioClip sound;
    PickUpState pickUpState;

    //public Text CountText;

    // Start is called before the first frame update
    void Start() {

        pickUpState = Object.FindObjectOfType<PickUpState>();
    }//ENd of start

    // Update is called once per frame
    void Update() {

    }//End of update

    //If player comes in contact with ammunition
    public void OnTriggerEnter(Collider other) {
        player = GameObject.FindGameObjectWithTag("Player");
        if (other.tag == "Player") {

            // get id of player that picked up ammo
            string id = other.transform.name;

            // calls function to tell server which player got the ammo
            CmdTellServerWhoGotAmmo(id, 6);

            //string id2 = other.transform.name;
            //CmdTellServerWhoGotAmmoPack(id2, 1);

            pickUpState.AmmoPickUp();

            Destroy(gameObject, 0);

            AudioSource.PlayClipAtPoint(sound, this.gameObject.transform.position);
        }//End of if Statement
    }//End of onTriggerEnter

    [Command]
    void CmdTellServerWhoGotAmmo(string id, int amount) {
        // find player with id that got shot
        GameObject go = GameObject.Find(id);

        // that player takes damage
        go.GetComponent<GunScript>().addAmmo(amount);
    }//End of CmdTellServerWhoGotAmmo

    //[Command]
    //void CmdTellServerWhoGotAmmoPack(string id, int amount2) {
    //    print("I GO HERE");
    //    GameObject go = GameObject.Find(id);

    //    go.GetComponent<Inventory>().AddAmmoPack(amount2);
    //}//End of CmdTellServerWhoGotAmmoPack

}//End of AmmoPickUP
