using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start() {
        
    }//End of Start Function 

    // Update is called once per frame
    void Update() {

        //when player presses esc button pause
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                isPaused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f; //Resumes time 
            }//End of inner if statement
            else {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f; //stops time when paused
            }//End of else statement
        }//End of if statement
    }//End of update function

    //Function to Resume the game
    public void ResumeGame() {

    }//End of Resume function

    public void QuitGame() {

    }//End of QuitGame function 
}//End of PausedMenu
