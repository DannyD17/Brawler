using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Startscript : MonoBehaviour {

    public void onStartClick()
    {  
        SceneManager.LoadScene(1);  // load scene by name
       
        // Debug.Log("start button was pressed");

    }

}
