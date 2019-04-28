using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

    public GameObject player;

    // wood variables
    [SyncVar(hook = "OnChangedWood")] private int woodAmount = 0;
    public Text woodDisplay;
    public GameObject woodItem;

    //Health Pack Variables
    //Ammo Pack Variables
    [SyncVar(hook = "OnChangedHealthPack")] private int HealthPackAmount = 0;
    public Text HealthPackDisplay;

    // Start is called before the first frame update
    void Start()
    {
        ChangeWoodText();
        ChangeHealthPackText();

    }

    // changes text of wood count
    public void ChangeWoodText()
    {

        if (isLocalPlayer)
        {
            // changes the number of wood item to display
            woodDisplay.GetComponent<Text>().text = "E Wood: " + woodAmount;
        }
    }

    // update wood count of player
    public void OnChangedWood(int amount)
    {
        woodAmount = amount;

        ChangeWoodText();
    }

    // give wood item to player
    public void AddWood(int amount)
    {
        woodAmount += amount;
    }

    /// <summary>
    /// Changes the health pack text.
    /// </summary>

    // changes text of ammo pack count
    public void ChangeHealthPackText() {
        if (isLocalPlayer) {
            // changes the number of ammo pack item to display
            HealthPackDisplay.GetComponent<Text>().text = "G Health Packs: " + HealthPackAmount;
        }
    }

    // update ammo pack count of player
    public void OnChangedHealthPack(int amount3) {
        HealthPackAmount = amount3;

        ChangeHealthPackText();
    }

    // give ammo pack item to player
    public void AddHealthPack(int amount3) {
        HealthPackAmount += amount3;
    }

    private void Update()
    {
        // check if using wood item
        IsUsingWoodItem();

        // check if using health pack
        IsUsingHealthPack();
    }

    void IsUsingWoodItem()
    {
        if (!isLocalPlayer)
            return;

        // if pressing the f button and has ammo
        if (Input.GetKeyDown(KeyCode.E) && woodAmount > 0)
        {
            
            ShootWood();

            // take away one bullet from ammo
            woodAmount -= 1;

            // no negative ammo
            if (woodAmount <= 0)
                woodAmount = 0;

            ChangeWoodText();
        }

        void ShootWood()
        {
            GameObject g = Instantiate(woodItem, transform.position + (transform.forward * 2), Quaternion.identity);
            g.transform.Rotate(90, 0, 0);
        }
    }

    // check if using the health item
    void IsUsingHealthPack()
    {
        if (!isLocalPlayer)
            return;

        // if pressing the g button and has packs
        if (Input.GetKeyDown(KeyCode.G) && HealthPackAmount > 0)
        {

            UsePack();

            // take away one pack from health
            HealthPackAmount -= 1;

            // no negative apacks
            if (HealthPackAmount <= 0)
                HealthPackAmount = 0;

            ChangeHealthPackText();
        }
    }

    // use health pack
    void UsePack()
    {
        // get id of player that used health pack
        string id = transform.name;
        CmdTellServerWhoUsedPack(id, 50);
    }

    [Command]
    void CmdTellServerWhoUsedPack(string id, int amount)
    {
        // find player with id that got shot
        GameObject go = GameObject.Find(id);

        // that player takes damage
        go.GetComponent<PlayerHealth>().AddHealth(amount);
    }//End of CmdTellServerWhoGotAmmo

}//End of Inventory
