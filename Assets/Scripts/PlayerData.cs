using UnityEngine;
using System.Collections;

/*
 * This script is made to store player data such as Player number as well as health that can be used as a damage multiplier
 * this script should interact with the RespawnManager Singleton 
 * right now all this script does is store data on each character and should be connected to the character controler for each character
 * 
 */



public class PlayerData : MonoBehaviour {
    public float health = 100;
    public int playerNumber;   // the player number 1-4 this is used in the Respawn manager and respawnOnCollision scripts
    
}
