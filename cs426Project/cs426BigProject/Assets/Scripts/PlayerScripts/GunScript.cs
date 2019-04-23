using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GunScript : NetworkBehaviour
{
    // variabes for the gun
    private int damage = 5;
    private float fireRate = 15f;
    private float maxRange = 200f;
    private float reloadSpeed = 0.75f;
    private bool isReloading = false;
    private bool isFiring = false;
    public static bool hasGun = true;

    // objects that will be needed
    public GameObject impactEffect;
    public GameObject ammoDisplay;
    public GameObject gunObject;
    public AudioClip gunShootSnd;
    public AudioClip gunReloadSnd;
    public AudioClip emptyGunSnd;
    public ParticleSystem gunFlash;

    // the camera's transform component
    [SerializeField] private Transform camTransform;

    private RaycastHit hit;

    // ammo variables
    private int maxAmmo = 6;
    private int currentAmmo;
    [SyncVar(hook = "OnChangeAmmo")] public int reservedAmmo = 0;

    // how long before can fire again
    private float coolDown = 0f;

    private void Start()
    {
        // starting ammo
        currentAmmo = 0;
        reservedAmmo = 0;

        changeAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        // constantly check if player is shooting

        if (hasGun)
        {
            checkIfShooting();
        }
    }

    // checks for left mouse click
    void checkIfShooting()
    {
        if (!isLocalPlayer)
            return;

        // if pressing the fire button and waited cooldown and has ammo
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= coolDown && currentAmmo > 0 && !isReloading)
        {
            // shoot the gun
            Shoot();

            // counts down to when player can fire again
            coolDown = Time.deltaTime + 1f / fireRate;

            // take away one bullet from ammo
            currentAmmo -= 1;

            // no negative ammo
            if (currentAmmo <= 0)
                currentAmmo = 0;

            StartCoroutine(Firing());

            changeAmmoText();
        }
        // if presses fire button and waited cooldown and currentAmmo is 0
        else if (Input.GetButtonDown("Fire1") && Time.time >= coolDown && currentAmmo <= 0 && !isReloading)
        {
            // if you have extra ammo
            if (reservedAmmo > 0)
                StartCoroutine(Reload());

            // else play empty sound
            else
                AudioSource.PlayClipAtPoint(emptyGunSnd, this.gameObject.transform.position);
        }

        // if player presses reload button
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 6 && reservedAmmo > 0 && !isReloading && !isFiring)
        {
            StartCoroutine(Reload());
        }
    }

    // shoots the gun
    void Shoot()
    {
        // if we hit something
        if (Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, maxRange))
        {
            // debug mode. shows tag of object that got hit in command line
            //Debug.Log(hit.transform.tag);

            // create impact effect of bullet
            GameObject newImpact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            /*EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if(target != null)
            {
                target.deductHealth(damage);
            }*/

            // if bullet hits enemy
            if (hit.transform.tag == "Enemy")
            {
                string id = hit.transform.name;

                CmdTellServerMonsterWasShot(id, damage);
            }

            // if bullet hit another player
            if (hit.transform.tag == "Player")
            {
                string id = hit.transform.name;

                // calls function to tell server which player got shot
                CmdTellServerWhoWasShot(id, damage);
            }

            // destroy impact bullet effect
            Destroy(newImpact, 2f);
        }


        // create flash
        gunFlash.Play();

        // show recoil animation
        gunObject.GetComponent<Animator>().Play("gunFireAnim");

        // play gun sound
        AudioSource.PlayClipAtPoint(gunShootSnd, this.gameObject.transform.position);
    }

    // resets some stuff for the gun
    private IEnumerator Firing()
    {
        isFiring = true;

        // how long reload will take
        yield return new WaitForSeconds(Time.deltaTime + 1f / fireRate);

        gunObject.GetComponent<Animator>().Play("gunIdle");

        isFiring = false;
    }

    // reload gun
    private IEnumerator Reload()
    {
        // play reload sound
        AudioSource.PlayClipAtPoint(gunReloadSnd, this.gameObject.transform.position);

        // now reloading
        isReloading = true;

        // how long reload will take
        yield return new WaitForSeconds(reloadSpeed);

        // if currentAmmo is 0
        if (currentAmmo <= 0)
        {
            // fill gun with ammo
            if (reservedAmmo >= 6)
            {
                currentAmmo += maxAmmo;
                reservedAmmo -= maxAmmo;
            }
            else
            {
                currentAmmo += reservedAmmo;
                reservedAmmo = 0;
            }
        }

        // manual reload
        else
        {
            // how much gun needs to be full
            int neededAmmo = maxAmmo - currentAmmo;

            // reload what you need
            if (reservedAmmo >= neededAmmo)
            {
                currentAmmo = maxAmmo;
                reservedAmmo -= neededAmmo;
            }

            else
            {
                currentAmmo += reservedAmmo;
                reservedAmmo = 0;
            }
        }

        // done reloading
        isReloading = false;
        changeAmmoText();
    }

    [Command]
    void CmdTellServerWhoWasShot(string id, int dmg)
    {
        // find player with id that got shot
        GameObject go = GameObject.Find(id);

        // that player takes damage
        go.GetComponent<PlayerHealth>().deductHealth(dmg);
    }

    [Command]
    void CmdTellServerMonsterWasShot(string id, int dmg)
    {
        // find monster with tag that got shot
        GameObject go = GameObject.Find(id);

        // that player takes damage
        go.GetComponent<EnemyHealth>().DeductHealth(dmg);
    }

    // changes text of ammo count
    public void changeAmmoText()
    {

        if (isLocalPlayer) { 
            // changes the number of ammo to display
            ammoDisplay.GetComponent<Text>().text = "gun " + currentAmmo + "/" + reservedAmmo;
        }
    }

    // update ammo of player
    public void OnChangeAmmo(int amount)
    {
        reservedAmmo = amount;

        changeAmmoText();
    }

    // give ammo to player
    public void addAmmo(int amount)
    {
        reservedAmmo += amount;
    }
}
