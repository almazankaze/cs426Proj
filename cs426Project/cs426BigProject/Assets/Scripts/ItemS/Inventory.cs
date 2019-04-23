using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour
{

    // wood variables
    [SyncVar(hook = "OnChangedWood")] private int woodAmount = 0;
    public Text woodDisplay;

    // Start is called before the first frame update
    void Start()
    {
        ChangeWoodText();
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
}
