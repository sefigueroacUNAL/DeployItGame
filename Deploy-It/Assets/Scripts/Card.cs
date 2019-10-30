using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {


    //Input fields
    public enum ColorType { GENERIC, PM, SI, IT };
    ColorType colorType;
    public Color viewcolor;
    public string textType;
    public string textInfo;
    public string textSubInfo;
    public Sprite icon;
    public Font font;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
