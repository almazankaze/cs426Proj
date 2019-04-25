using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// THIS SCRIPT HANDLES THE PLAYER'S HEALTH

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar (hook = "OnHealthChanged")] private int health = 100;
    private Text healthText;
    public GameState gameState;

    //health bar
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private Image healthbarFillImage;



    // Start is called before the first frame update
    void Start()
    {
        // set initial health text
        healthText = GameObject.Find("Health").GetComponent<Text>();
        SetHealthText();
        Debug.Log("got into start");
    }

    // changs the text of health
    void SetHealthText()
    {
        if(isLocalPlayer)
        {
            healthText.text = "Health" + health.ToString();
        }
    }

    // take away health
    public void deductHealth(int dmg)
    {
        health -= dmg;
    }

    // add health to player
    public void AddHealth(int hlth)
    {
        health += hlth;
    }

    // whenever health changes, update text and health
    void OnHealthChanged(int hlth)
    {
        health = hlth;

        if (isLocalPlayer)
        {
            // if health is less than zero, game over
            if (health <= 0)
            {
                gameState.GameOver();
            }
        }

     //   SetHealthText();
        SetHealthbarUi();
    }

    // when monster hits user, player takes damage
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Enemy")
        {

            deductHealth(1);
            OnHealthChanged(health);
        }

    }


    private void SetHealthbarUi()
    {
        // float healthbarPercentage = CalculateHeathPercentage();
        healthbarSlider.value = health;
    }


}
