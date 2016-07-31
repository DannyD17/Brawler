using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour {

    public float rotation_speed = 1f;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotation_speed);
	}
}
