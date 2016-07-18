using UnityEngine;
using System.Collections;
using Brawler;

public class Player {

    public int playerNumber { get; private set; }
    public Color playerColor { get; private set; }
    public bool _initialized = false;
    public GameObject player { get; set; }
    
    
    public Player(int playerIDNumber)
    {
        
        playerNumber = playerIDNumber;
        playerColor = Random.ColorHSV();
        _initialized = true;
        Debug.Log("Player Number: " + playerNumber + " player color: " + playerColor + "Initialized : " + _initialized);
        
    }

    public void ChangeColor()
    {
        playerColor = Random.ColorHSV();
        SetColor();
    }

    public void SetColor() {

        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();

        foreach (Renderer ren in renderers)
        {
            ren.material.color = playerColor;
        }

        Debug.Log(playerColor);
    }

    private void InitializePlayer()
    {

    }

    public void RemovePlayer()
    {
        
    }
}
