using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Startscript : MonoBehaviour {
    public Scene level1;

    public void onStartClick()
    {
        SceneManager.LoadScene(1);  // scene one is the second in build priority
        
      //  SceneManager.SetActiveScene(level1);  // should set the game scene to the active scene.
       
        // Debug.Log("start button was pressed");

    }

}
