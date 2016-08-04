using UnityEngine;
using System.Collections;
using Brawler;

public class RespawnOnCollision : MonoBehaviour
{
    public GameObject PlayerAvatar;
    public Transform[] spawn_positions;
    public float seconds = 3;
    

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            CheckLives(col);
        }
        else if (col.gameObject.GetComponent<Rigidbody>() != null)
        {
            col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            int index = (int)(Random.Range(0f, spawn_positions.Length));
            col.gameObject.transform.position = spawn_positions[index].position;
        }
    }
    IEnumerator WaitToRespawn(Collision other)
    {
        int playerNum = other.gameObject.GetComponent<AnimatorCharacterController>().playerNum;
        Destroy(other.gameObject.transform.parent.gameObject);
        yield return new WaitForSeconds(seconds);
        int index = (int)(Random.Range(0f, spawn_positions.Length));
        GamePlayerManager.Instance.SpawnPlayer(playerNum, PlayerAvatar, spawn_positions[index]);
        //Debug.Log(other.name + " name of the object");
        //other.position = spawn_positions[index].position;

    }
    void CheckLives(Collision col)
    {
       
        int PlayerNum = col.gameObject.GetComponent<AnimatorCharacterController>().playerNum;
        //GameObject charControl = col.collider.gameObject;
        //PlayerData playerData = charControl.GetComponent<PlayerData>();

        RespawnManager.instance.PlayerDeath(PlayerNum);            // player has died call player death in Respawn Manager
                                                                                     // PlayerNum = playerData.playerNumber;
       // Debug.Log("player lives = " + RespawnManager.instance.PlayerLives[playerData.playerNumber]);
        if (RespawnManager.instance.PlayerLives[PlayerNum] < 1)   // checks if there is lives
        {
            //the player is dead now i can spawn them in a location for dead players off or on camera. this shoudl be a place outside of the camera scripts adjustment area.
            Destroy(col.gameObject.transform.parent.gameObject);
        }
        else
        {
            StartCoroutine(WaitToRespawn(col));
        //    StartCoroutine(WaitToRespawn(col.collider));
        //    // call respawn script 
        }

    }
}
