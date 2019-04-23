using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WoodPickUp : NetworkBehaviour
{
    public GameObject woodPickUp;

    // when player collides with ammo box
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // destroy item
            Destroy(woodPickUp, 0);

            // get id of player that picked up ammo
            string id = other.transform.name;

            // calls function to tell server which player got the ammo
            CmdTellServerWhoGotItem(id, 1);
        }
    }

    [Command]
    void CmdTellServerWhoGotItem(string id, int amount)
    {
        // find player with id that got shot
        GameObject go = GameObject.Find(id);

        // that player takes damage
        go.GetComponent<Inventory>().AddWood(amount);
    }//End of CmdTellServerWhoGotAmmo
}

