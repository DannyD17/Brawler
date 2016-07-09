using UnityEngine;
using System.Collections;


public class Pause : MonoBehaviour {

    public bool isPause = false;

    void Start ()
    {
        GetComponent<Canvas>().enabled = false;     // on Start have the game pause menu turned off.

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))    // when esc pressed in game the game will go to the pause menu
        {
            isPause = !isPause;                  // changes the bool that describes if the game is paused

            if (isPause)                         //if the game is paused  (when paused this code is called)
            {
                Debug.Log("game has been paused");
                Time.timeScale = 0;                       // pauses the game 
                GetComponent<Canvas>().enabled = true;  // turn on the canvas in front of the game
               
            }
            else
            {
                GetComponent<Canvas>().enabled = false;   // turns of canvas
                Time.timeScale = 1;                       // resumes game
            }
        }
    }

  
}
