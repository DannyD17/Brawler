using UnityEngine;
using System.Collections;


public class Pause : MonoBehaviour {

    public bool isPause = false;
    void Start ()
    {
       // GetComponent<Canvas>().enabled = false;
        GetComponentInParent<Canvas>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;

            if (isPause)
            {
                Debug.Log("game has been paused");
                GetComponentInParent<Canvas>().enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                GetComponentInParent<Canvas>().enabled = false;
                Time.timeScale = 1;
            }
        }
    }

    //public void Resume()
    //{
    //    GetComponent<Canvas>().enabled = false;
    //    Time.timeScale = 1;
    //}
    //public void ExitGame()
    //{
    //    {
    //        Application.Quit();

    //    }

    //}
}
