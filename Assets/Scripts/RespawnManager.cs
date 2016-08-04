using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour
{

    /*
     * this singleton is meant to move between scenes so that in the start up scene we can manage how many players and then in the Game scene we can manage respawn and lives.
     * finally in the after game scene we can look at who is the winner.
     *
     */

    private static RespawnManager _instance;

    public static RespawnManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<RespawnManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public int[] PlayerLives = new int[4];
    public int NumberOfLives = 3;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerLives[i] = NumberOfLives;  // initialize the number of lives each player has. 
        }

    }
    /// <summary>
    /// Called on Player Death. ususally a collison with OutofBounds Terrain
    /// </summary>
    /// <param name="PlayerNumber"> The Player Number 1-4</param>
    public void PlayerDeath(int PlayerNumber)
    {
       

        if (PlayerNumber < 0 || PlayerNumber > 3)
        {
            throw new System.Exception("Character Numbers are off make sure your Characters are numbered 1-4");
        }
      
        PlayerLives[PlayerNumber]--;    //adjust the index down 1 and decrease the players lives by 1

        // i need to add a return that is probably the respawn location

    }
    // need to create a method that destroys lives of characters who are not playing.

}