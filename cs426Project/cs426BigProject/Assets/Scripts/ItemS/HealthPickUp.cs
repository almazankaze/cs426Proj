using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthPickUp : NetworkBehaviour {
    public GameObject healthPickUp;

    // when player collides with ammo box
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {

            // destroy item
            Destroy(healthPickUp, 0);

            string id = other.transform.name;

            // calls function to tell server which player got the ammo
            CmdTellServerWhoGotItem(id, 1);
        }//End of if statement
    }//End of OnTriggerEnter
    void CmdTellServerWhoGotItem(string id, int amount)
    {
        // find player with id that got shot
        GameObject go = GameObject.Find(id);

        // that player takes damage
        go.GetComponent<Inventory>().AddHealthPack(amount);
    }//End of CmdTellServerWhoGotAmmo
}//End of HealthPickUP
