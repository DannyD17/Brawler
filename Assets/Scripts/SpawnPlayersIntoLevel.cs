using UnityEngine;
using System.Collections;

public class SpawnPlayersIntoLevel : MonoBehaviour {

    public GameObject playerAvatar;
    public Transform spawnPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < GamePlayerManager.players.Length; i++)//InputManager.Devices.Count
            {
                GamePlayerManager.Instance.SpawnPlayer(i, playerAvatar, spawnPosition);
            }
        }
    }

}
