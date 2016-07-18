using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using Brawler;

public class GamePlayerManager : MonoBehaviour {

    public GameObject playerAvatar;
    public static int playerNum = 0;
    public static Player[] player = new Player[4];

    //public static List<InputManager> controllers;

    //private static int _prevDeviceCount = 0;


        void Update() {

        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i].Action1.WasPressed && player[i] == null)
            {
                player[i] = new Player(i);
                player[i].player = Instantiate(playerAvatar, Vector3.zero, Quaternion.identity) as GameObject;
                player[i].ChangeColor();
                player[i].player.GetComponentInChildren<AnimatorCharacterController>().playerNum = player[i].playerNumber;
                playerNum += 1;
                Debug.Log(player[i]);
            }
            if (InputManager.Devices[i].RightBumper.WasPressed)
            {
                player[i].ChangeColor();
            }
            if (InputManager.Devices[i].Action2.WasPressed)
            {
                Destroy(player[i].player);
                player[i] = null;
                playerNum -= 1;
            }

            if (InputManager.Devices[i].LeftBumper.WasPressed)
            {
                Instantiate(player[i].player, Vector3.zero, Quaternion.identity);
            }

        }

        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

    }

}
