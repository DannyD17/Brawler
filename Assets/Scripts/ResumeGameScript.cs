using UnityEngine;
using System.Collections;

public class ResumeGameScript : MonoBehaviour {

    public void Resume()
    {
        GetComponentInParent<Canvas>().enabled = false;
        Time.timeScale = 1;   
    }
    
}
