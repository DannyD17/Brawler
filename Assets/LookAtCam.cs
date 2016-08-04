using UnityEngine;
using System.Collections;

public class LookAtCam : MonoBehaviour {

    public Transform cam;

    void Update() {
        transform.LookAt(cam);
    }
}
