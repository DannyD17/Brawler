using UnityEngine;
using System.Collections;

public class SpawnPlayersIntoLevel : MonoBehaviour {

    public GameObject playerAvatar;
    public Transform[] spawnPosition;
    public MultiplayerCamera cam;

    void Start()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        if (true)
        {
            for (int i = 0; i < GamePlayerManager.players.Length; i++)//InputManager.Devices.Count
            {
                GamePlayerManager.Instance.SpawnPlayer(i, playerAvatar, spawnPosition[i]);
            }
            cam.InitiateCamera();
        }
    }

}
