using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

    public bool isPause = false;
    public Canvas startMenu;
    public Canvas optionsMenu;

    void Start()
    {
        optionsMenu.enabled = false;     // on Start have the options menu turned off.

    }

    public void LoadCanvas()               // brings the options menu up
    {
        Debug.Log(" i am here");
        optionsMenu.enabled = true;
        startMenu.enabled = false;
    }

    public void backToStartCanvas ()  // bring the start menu back up
    {
        Debug.Log(" i am now 2 here");
        startMenu.enabled = true;
        optionsMenu.enabled = false;
    }



}
	// control volume and other stuff. i would like this script to be called both in the pause menu and the pregame menu. i am going to start with it just in pregame.

