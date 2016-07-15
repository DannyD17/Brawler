using UnityEngine;
using System.Collections;

public class RespawnOnCollision : MonoBehaviour {

	public Transform[] spawn_positions;

	void OnCollisionEnter(Collision other)
	{
        if (other.collider.tag == "Player")
        {
            StartCoroutine(WaitToRespawn(other.collider));
        }
	}

	IEnumerator WaitToRespawn(Collider other)
	{
		yield return new WaitForSeconds (3f);

		int index = (int)(Random.Range (0f, spawn_positions.Length));
		other.transform.position = spawn_positions [index].position;
	}
}
