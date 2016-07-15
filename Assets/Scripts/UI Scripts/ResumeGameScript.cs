using UnityEngine;
using System.Collections;

public class ResumeGameScript : MonoBehaviour {

    public void Resume()    // a resume script for the resume button on pause canvas.
    {
        GetComponentInParent<Canvas>().enabled = false;  // turns pause screen off
        Time.timeScale = 1;                               // resumes time
    }   
}
