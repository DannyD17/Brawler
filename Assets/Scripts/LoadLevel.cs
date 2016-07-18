using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))  // load the game level when the key is pressed
        {
            //SceneManager.LoadScene("PlayerSelect 2", LoadSceneMode.Single);
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}

/*
 * I would like to create just a load next scene script and one back to start scene script so that we dont have as many scripts
 */ 
 