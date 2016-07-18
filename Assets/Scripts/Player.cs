using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int playerNumber { get; private set; }
    public Color playerColor { get; private set; }
    public bool _initialized = false;
    public GameObject player { get; set; }
    private Renderer[] renderers;
    
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

    private void SetColor() {

        renderers = player.GetComponentsInChildren<Renderer>();

        foreach (Renderer ren in renderers)
        {
            ren.material.color = playerColor;
        }

        Debug.Log(playerColor);
    }

    public void RemovePlayer()
    {
        
    }
}
