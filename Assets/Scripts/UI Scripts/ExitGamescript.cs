using UnityEngine;
using System.Collections;

public class ExitGamescript : MonoBehaviour {

    public void ExitGame()
    {   
         Application.Quit(); // this does not work in unity editor and in web applications
    }
}
