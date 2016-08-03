using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using Brawler;

public class GamePlayerManager : MonoBehaviour {

    public GameObject playerAvatar;
    public Transform spawnPosition;
    public static int playerNum = 0;
    public static Player[] players = new Player[4];
    public static GamePlayerManager Instance;

    //public static List<InputManager> controllers;

    //private static int _prevDeviceCount = 0;

    void Start() {
        Instance = this;
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = null;
        }
    }

        void Update() {

        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i].Action1.WasPressed && players[i] == null)
            {
                players[i] = new Player(i);
                SpawnPlayer(i, playerAvatar, spawnPosition);
                playerNum += 1;
                Debug.Log(players[i]);
            }
            if (InputManager.Devices[i].RightBumper.WasPressed)
            {
                players[i].ChangeColor();
            }
            if (InputManager.Devices[i].Action2.WasPressed)
            {
                Destroy(players[i].player);
                players[i] = null;
                playerNum -= 1;
            }

            if (InputManager.Devices[i].LeftBumper.WasPressed && InputManager.Devices[i].Action4.WasPressed && InputManager.Devices[i].RightTrigger.WasPressed)
            {
                SpawnPlayer(i, playerAvatar, spawnPosition);
            }

        }

    }

    public void SpawnPlayer(int index, GameObject player, Transform position) {
        if (players[index] == null)
            return;

        players[index].player = Instantiate(player, position.position, position.rotation) as GameObject;
        players[index].SetColor();
        players[index].player.GetComponentInChildren<AnimatorCharacterController>().playerNum = players[index].playerNumber;
    }

}
