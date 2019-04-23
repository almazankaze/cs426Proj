using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnEnemy: NetworkBehaviour
{
    public GameObject enemyPrefab;
    public GameObject miniEnemy;
    private int myID = 0;

    // spawn timer
    public float spawnTimer;
    private float spawnCounter;
    private bool isNight = false;

    public GameObject spawner;

    // when server is created
    public override void OnStartServer()
    {
        spawnCounter = spawnTimer;

        // create big enemy
        GameObject enemy = (GameObject)Instantiate(enemyPrefab);

        enemy.tag = "enemy1";

        // spawn enemy when server is created
        NetworkServer.Spawn(enemy);

        // start timer 
        StartCoroutine(waitTillNight());
    }

    // Update is called once per frame
    void Update()
    {

        if (!isServer)
            return;

        if(isNight) { 

        // countdown to new enemy
        spawnCounter -= Time.deltaTime;

            // time to spawn
            if (spawnCounter <= 0)
            {
                // hold different spawn positions
                Vector3[] spawnPositions = new Vector3[]
                {
                new Vector3(-142.0f, 0f, -160.0f),
                new Vector3(93.0f, 0f, -250.0f),
                new Vector3(95.0f, 0f, 26.0f),
                new Vector3(-10.0f, 0f, -173.0f),
                };

                // get random side of map
                int i = Random.Range(0, spawnPositions.Length);

                // set position to spawn enemy
                Vector3 position = spawnPositions[i];

                // new enemy is created
                GameObject newEnemy = (GameObject)Instantiate(miniEnemy, position, spawner.transform.rotation);

                newEnemy.name = "enemy " + myID;

                NetworkServer.Spawn(newEnemy);

                spawnCounter = spawnTimer;
                myID++;
            }
        }
    }

    public IEnumerator waitTillNight()
    {
        yield return new WaitForSeconds(60);

        // it is now night time
        isNight = true;
    }//End of IEnumerator
}
