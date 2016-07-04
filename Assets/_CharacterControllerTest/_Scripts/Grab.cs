using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour {

    //      grabber = tranform.find("bones").find("shoulder").find
    //     ( ... and then grabber.GetComponent("hinge").connectedBody = otherRB.
    //
    //
    //  GRAB : Instantiate Child Kinematic Hingejoint connected to hand joint when Grab button is pressed. 
    //         Destroyed when Grab is let go.
    //
    //  Hand Movement : Raycast to detect closest Grabbable object. Hands hover by a little bit towards the detected object.
    //
    //  Arm Swinging : Players Press button to Extend arms and then can rotate character to apply enough force to knock out opponents.
    //  
    //  Arm Swinging Combo 1:  Players jump and while in mid-air extend arms out to perform superman spearing attack.
    //
    //  Arm Swinging Combo 2:  Players run towards a thin object and grabs on to it with one hand. With enough speed
    //                         the player character will spin around and come back with a strong force attack.

    


    void Start () {
	
	}
	

	void Update () {
	
	}
}
