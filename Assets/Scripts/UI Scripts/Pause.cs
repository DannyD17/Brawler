using UnityEngine;
using System.Collections;


public class Pause : MonoBehaviour {

    private bool isPause = false;
    public Canvas pauseMenu;
    

    void Start ()
    {
        
        pauseMenu.enabled = false;     // on Start have the game pause menu turned off.

    }
	
	// Update is called once per frame
	void Update () {

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!! must change keycode to controler input.

        if (Input.GetKeyDown(KeyCode.Escape))    // when esc pressed in game the game will go to the pause menu
        {
            isPause = !isPause;                  // changes the bool that describes if the game is paused

            if (isPause)                         //if the game is paused  (when paused this code is called)
            {
                Debug.Log("game has been paused");
                Time.timeScale = 0;                       // pauses the game 
                pauseMenu.enabled = true;  // turn on the canvas in front of the game
               
            }
            else
            {
                pauseMenu.enabled = false;   // turns of canvas
                Time.timeScale = 1;                       // resumes game
            }
        }
    }

    public void Resume()    // a resume script for the resume button on pause canvas.
    {
        isPause = !isPause;
        pauseMenu.enabled = false;  // turns pause screen off
        Time.timeScale = 1;                               // resumes time
    }

}
