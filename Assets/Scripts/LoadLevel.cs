using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
             SceneManager.LoadScene("PlayerSelect 2", LoadSceneMode.Single);

    }
}
