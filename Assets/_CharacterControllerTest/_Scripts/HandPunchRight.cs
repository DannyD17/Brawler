using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;

public class HandPunchRight : MonoBehaviour {


    public GameObject target;
    public float speed = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.transform.localPosition, Time.deltaTime + speed);

        if (Input.GetKeyUp(KeyCode.V))
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
