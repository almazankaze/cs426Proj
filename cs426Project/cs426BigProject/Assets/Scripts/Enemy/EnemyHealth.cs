using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyHealth : NetworkBehaviour
{
    [SyncVar(hook = "OnHealthChanged")] public int health = 100;
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private Image healthbarFillImage;
    [SerializeField] private Color maxHealthColor;
    [SerializeField] private Color noHealthColor;

    // deduct health
    public void DeductHealth(int dmg)
    {
        if (!isServer)
            return;

        health -= dmg;

        if (health <= 0)
            Destroy(gameObject);
        SetHealthbarUi();
    }

    // update health
    public void OnHealthChanged(int hlth)
    {
        health = hlth;
    }

    private void SetHealthbarUi()
    {
       // float healthbarPercentage = CalculateHeathPercentage();
        healthbarSlider.value = CalculateHeathPercentage();
    }

    private float CalculateHeathPercentage()
    {
      return  ((float)health / 100) * 100;
    }
}
