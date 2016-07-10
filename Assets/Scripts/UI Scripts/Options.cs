using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

    public bool isPause = false;
    public Canvas startMenu;
    public Canvas optionsMenu;

    void Start()
    {
        optionsMenu.enabled = false;     // on Start have the game pause menu turned off.

    }

    // Update is called once per frame
    void LoadCanvas()
    {
        optionsMenu.enabled = true;
        startMenu.enabled = false;
    }



}
	// control volume and other stuff

