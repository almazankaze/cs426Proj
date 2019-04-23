using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawner : NetworkBehaviour
{
    public GameObject[] items;

    // when server is created
    public override void OnStartServer()
    {

        for (int i = 0; i < items.Length; i++)
        {
            // create item
            GameObject item = (GameObject)Instantiate(items[i]);

            // spawn item when server is created
            NetworkServer.Spawn(item);
        }
    }
}
