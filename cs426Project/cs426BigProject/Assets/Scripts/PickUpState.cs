using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpState : MonoBehaviour
{

    public Animator ammoPickUpAnim;
    // Start is called before the first frame update
    public void AmmoPickUp() {
        ammoPickUpAnim.SetBool("AmmoPickUp", true);
    }
}
