using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject[] playerIcons;

	void Start ()
    {
        for (int i = 0; i < GamePlayerManager.players.Length; i++)
        {
            if (GamePlayerManager.players[i] == null)
                break;
            playerIcons[i].GetComponent<Image>().color = GamePlayerManager.players[i].playerColor;
            Color playerUIAlpha = playerIcons[i].GetComponent<Image>().color;
            playerUIAlpha.a = 0.59f;
            playerIcons[i].GetComponent<Image>().color = playerUIAlpha;
        }

        for (int i = 0; i < 4; i++)
        {
            if (GamePlayerManager.players[i] == null)
                RemovePlayerIcon(i);
        }
        
    }

    public void RemovePlayerIcon(int playerID)
    {
        Debug.Log("in Method");
        playerIcons[playerID].SetActive(false);
    }
}
