using UnityEngine;
using System.Collections;

public class Crane : MonoBehaviour
{
    public GameObject craneOperator;
    public float craneSwingSpeed;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Quaternion craneRotation = Quaternion.Euler(new Vector3(0f, 1f * craneSwingSpeed, 0f));
        //craneOperator.transform.Rotate(Vector3.up * Time.deltaTime * craneSwingSpeed);
        //craneOperator.GetComponent<Rigidbody>().AddTorque(Vector3.up * craneSwingSpeed);
        //craneOperator.GetComponent<Rigidbody>().angularVelocity.Set(0f, 1f * craneSwingSpeed, 0f);
        craneOperator.GetComponent<Rigidbody>().rotation *= craneRotation;
    }
}
