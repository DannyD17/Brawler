using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToStartMenu : MonoBehaviour {

    public void onBackToStartPress ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0,LoadSceneMode.Single);  // return to the start game scene.
	}
}
