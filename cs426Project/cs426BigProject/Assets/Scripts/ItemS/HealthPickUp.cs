using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthPickUp : NetworkBehaviour
{
    public GameObject healthPickUp;

    // when player collides with ammo box
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // destroy item
            Destroy(healthPickUp, 0);
        }
    }
}
