using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// THIS SCRIPT ASSIGNS A UNIQUE ID/NAME TO EACH PLAYER

public class PlayerID : NetworkBehaviour
{
    // variables that will be needed
    [SyncVar] public string playerName;
    private NetworkInstanceId playerID;
    private Transform myTransform;

    // when new game is started
    public override void OnStartLocalPlayer()
    {
        GetID();
        SetID();
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the transform component of the player
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // set the id/name when new client joins the server
        if(myTransform.name == "" || myTransform.name == "Player(Clone)")
        {
            SetID();
        }
    }

    [Client]
    void GetID()
    {
        // get name for client
        playerID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    // set the new name/id
    void SetID()
    {
        if(!isLocalPlayer)
        {
            myTransform.name = playerName;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    // actually makes the new name/id
    string MakeUniqueIdentity()
    {
        string uniqueName = "Player " + playerID.ToString();
        return uniqueName;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerName = name;
    }
}
