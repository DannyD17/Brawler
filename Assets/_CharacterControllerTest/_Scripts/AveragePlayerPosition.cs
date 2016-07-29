using UnityEngine;
using System.Collections;

public class AveragePlayerPosition : MonoBehaviour
{

    GameObject[] _players;
    public float clamp_min = -1f;
    public float clamp_max = 1f;



    // Use this for initialization
    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 average = Vector3.zero;

        for (int i = 0; i < _players.Length; i++)
        {
            average += _players[i].transform.position;
        }
        average /= _players.Length;

        average.y = Mathf.Clamp(average.y, clamp_min, clamp_max);
        transform.position = average;

    }
}
