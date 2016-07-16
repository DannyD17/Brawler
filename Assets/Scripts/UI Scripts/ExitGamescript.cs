using UnityEngine;
using System.Collections;

public class ExitGamescript : MonoBehaviour {

    public void ExitGame()
    {   
         Application.Quit(); // this does not work in unity editor and in web applications because it would cause the editor to crash or the website to crash.
    }
}
