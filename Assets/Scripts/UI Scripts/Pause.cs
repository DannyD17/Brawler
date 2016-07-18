using UnityEngine;
using System.Collections;


public class Pause : MonoBehaviour {

    public bool isPause = false;
    public Canvas PauseMenu;
    void Start ()
    {
        
        PauseMenu.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;

            if (isPause)
            {
                Debug.Log("game has been paused");
                PauseMenu.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.enabled = false;
                Time.timeScale = 1;
            }
        }
    }

    public void Resume()
    {
        PauseMenu.enabled = false;
        Time.timeScale = 1;
    }
    //public void ExitGame()
    //{
    //    {
    //        Application.Quit();

    //    }

    //}
}
