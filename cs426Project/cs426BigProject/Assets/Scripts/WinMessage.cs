using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMessage : MonoBehaviour
{

    public Animator winnerAnim;

    // show win message
    public void Winner()
    {
        winnerAnim.SetBool("Winner", true);
        StartCoroutine(playCredits());
    }

    private IEnumerator playCredits()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Credits");
    }
}
