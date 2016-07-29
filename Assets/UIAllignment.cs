using UnityEngine;
using System.Collections;

public class UIAllignment : MonoBehaviour
{
    public float percentOfHeight = .3f;
    public float percentOfLength = .3f;
    public RectTransform imageRect;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        float screenHeight = Screen.height;
        imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, percentOfLength * screenHeight);
        imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, percentOfHeight * screenHeight);
	}
}
