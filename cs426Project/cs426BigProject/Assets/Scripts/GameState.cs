using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameState : MonoBehaviour
{
    public Animator gameOverAnim;
    public AudioClip gameOversound;




    // show game over message
    public void GameOver()
    {
       
        AudioSource.PlayClipAtPoint(gameOversound, this.gameObject.transform.position);
        SceneManager.LoadScene("Credits");

    }
}
