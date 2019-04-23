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

    //Ammo Pack Variables
    [SyncVar(hook = "OnChangedAmmoPack")] private int AmmoPackAmount = 0;
    public Text AmmoPackDisplay;

    //Health Pack Variables
    //Ammo Pack Variables
    [SyncVar(hook = "OnChangedHealthPack")] private int HealthPackAmount = 0;
    public Text HealthPackDisplay;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAmmoPackText();
        ChangeWoodText();
        ChangeHealthPackText();

    }

    // changes text of wood count
    public void ChangeWoodText()
    {

        if (isLocalPlayer)
        {
            // changes the number of wood item to display
            woodDisplay.GetComponent<Text>().text = "Wood: " + woodAmount;
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
    /// Changes the ammo pack text.
    /// </summary>

    // changes text of ammo pack count
    public void ChangeAmmoPackText() {

        if (isLocalPlayer) {
            // changes the number of ammo pack item to display
            AmmoPackDisplay.GetComponent<Text>().text = "Ammo Pack: " + AmmoPackAmount;
        }
    }

    // update ammo pack count of player
    public void OnChangedAmmoPack(int amount2) {
        AmmoPackAmount = amount2;

        ChangeAmmoPackText();
    }

    // give ammo pack item to player
    public void AddAmmoPack(int amount2) {
        AmmoPackAmount += amount2;
    }

    /// <summary>
    /// Changes the health pack text.
    /// </summary>

    // changes text of ammo pack count
    public void ChangeHealthPackText() {
        if (isLocalPlayer) {
            // changes the number of ammo pack item to display
            HealthPackDisplay.GetComponent<Text>().text = "Health Packs: " + HealthPackAmount;
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
            Quaternion playerRotation = player.transform.rotation;
            Instantiate(woodItem, transform.position + (transform.forward * 2), playerRotation);
        }
    }

}//End of Inventory
