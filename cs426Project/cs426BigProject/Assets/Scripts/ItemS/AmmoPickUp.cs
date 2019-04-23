using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AmmoPickUp : NetworkBehaviour {

    public GameObject player;
    public AudioClip sound;
    PickUpState pickUpState;

    public int count = 0;
    public int onHold = 0;
    public GameObject ammoPoints;
    //public Text CountText;

    // Start is called before the first frame update
    void Start() {

        pickUpState = Object.FindObjectOfType<PickUpState>();
    }//ENd of start

    // Update is called once per frame
    void Update() {
        ChangeAmmoPackText();
    }//End of update

    //If player comes in contact with ammunition
    public void OnTriggerEnter(Collider other) {
        player = GameObject.FindGameObjectWithTag("Player");
        if (other.tag == "Player") {

            // get id of player that picked up ammo
            string id = other.transform.name;

            // calls function to tell server which player got the ammo
            CmdTellServerWhoGotAmmo(id, 6);

            pickUpState.AmmoPickUp();

            Destroy(gameObject);

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

    //Print ammo pack in GUI
    //Place the obeject
    public void ChangeAmmoPackText() {
        if (isLocalPlayer) {
            ammoPoints.GetComponent<Text>().text = "Ammo Pack(s):  " + onHold;
        }//End of if statement
    }//End of ChangeAmmoPackText
}//End of AmmoPickUP
