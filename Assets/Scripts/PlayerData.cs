using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

    public int Lives = 3;
    public float health = 100;
    // Use this for initialization

    void OnCollisionEnter(Collision col)
    {
      if (col.collider.tag == "OutOfBounds")
        {
            Debug.Log("here i am");
            Lives--;
        }

    }

    // Update is called once per frame
    void Update () {
	
        if (Lives == 0)
        {
            Destroy(gameObject);  // i need to change this   when destroyed unity get very very mad!!!!!
        }
	}
}
