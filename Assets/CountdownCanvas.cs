using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownCanvas : MonoBehaviour {

    float time;
    public float time_limit;
    public Text text;
    public Transform camera;

    void OnEnable()
    {
        time = time_limit;
    }

    //void OnGUI()
    //{
    //    GUI.skin.label.fontSize = 24;
    //    GUI.Label(new Rect(10f, 10f, 200f, 100f), time.ToString("N1"));
    //}

    void Update()
    {
        time -= Time.deltaTime;
        text.text = "Start: " + ((int)time + 1);
        transform.LookAt(camera);
        Debug.Log(time);

    }


  
}
