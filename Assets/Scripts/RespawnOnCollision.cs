using UnityEngine;
using System.Collections;

public class RespawnOnCollision : MonoBehaviour {

	public Transform[] spawn_positions;

	void OnCollisionEnter(Collision col)
	{
        if (col.collider.tag == "Player")
        {
            CheckLives(col);
        }
	}

	IEnumerator WaitToRespawn(Collider other)
	{
		yield return new WaitForSeconds (3f);

		int index = (int)(Random.Range (0f, spawn_positions.Length));
		other.transform.position = spawn_positions [index].position;
	}


    void CheckLives(Collision col)
     {
         //int PlayerNum;
         GameObject charControl = col.collider.gameObject;
         PlayerData playerData = charControl.GetComponent<PlayerData>();
 
         RespawnManager.instance.PlayerDeath(playerData.playerNumber);                // player has died call player death in Respawn Manager
                                                                                      // PlayerNum = playerData.playerNumber;
         Debug.Log("player lives = " + RespawnManager.instance.PlayerLives[playerData.playerNumber]);
         if (RespawnManager.instance.PlayerLives[playerData.playerNumber - 1] < 1)   // checks if there is lives
         {
             //the player is dead now i can spawn them in a location for dead players off or on camera. this shoudl be a place outside of the camera scripts adjustment area.
             
         }
         else
         {
             StartCoroutine(WaitToRespawn(col.collider));
             // call respawn script 
         }
     }
}
