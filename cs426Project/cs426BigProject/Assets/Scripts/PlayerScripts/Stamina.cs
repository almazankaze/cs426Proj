using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Stamina : NetworkBehaviour
{
    [SyncVar(hook = "OnStaminaChanged")] private float stamina = 100;

    //health bar
    [SerializeField] private Slider staminabarSlider;
    [SerializeField] private Image staminabarFillImage;

    // take away stamina
    public void deductStamina()
    {
        stamina -= Time.deltaTime * 5;
    }

    // add stamina to player
    public void AddStamina()
    {
        stamina += Time.deltaTime;

        if (stamina >= 100)
            stamina = 100;
    }

    // whenever stamina changes, stamina bar
    void OnStaminaChanged(float amount)
    {
        stamina = amount;

        if (isLocalPlayer)
        {
            // if stamina is less than zero
            if (stamina <= 0)
            {
                stamina = 0;
            }
        }
        SetStaminabarUi();
    }

    private void SetStaminabarUi()
    {
        staminabarSlider.value = stamina;
    }

    public float GetStamina()
    {
        return stamina;
    }
}
