using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMessage : MonoBehaviour
{

    public Animator winnerAnim;

    // show win message
    public void Winner()
    {
        winnerAnim.SetBool("Winner", true);
    }
}
