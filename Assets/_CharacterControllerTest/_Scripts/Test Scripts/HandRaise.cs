using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;

public class HandRaise : MonoBehaviour {

    // Set Positions of Transform

    public GameObject target;
    public float speed = 0f;


    
	void Update ()
    {


        if (Input.GetKey(KeyCode.Q))

           transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.transform.localPosition, Time.deltaTime + speed);
        if (Input.GetKeyUp(KeyCode.Q))
            transform.localPosition = new Vector3(0, 0, 0);

        // If player Crouches, lowers arms to normal position and prevents arms to raise while crouched.

        if (Input.GetKey(KeyCode.C))
            transform.localPosition = new Vector3(0, 0, 0);

    }

    public void OnRegainBalance()
    {

    }

    public void OnLoseBalance()
    {
     //   transform.localPosition = new Vector3(0, 0, 0);
    }
}
