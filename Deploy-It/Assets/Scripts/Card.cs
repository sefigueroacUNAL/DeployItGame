using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card  {


    //Input fields
    public enum ColorType { GENERIC, PM, SI, IT };
    public ColorType colorType;
    public Color viewcolor;
    public string textType;
    public string textInfo;
    public string textSubInfo;
    public int icon;
    public int font;
    public Object where;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public string  ToString(){
        return string.Format("Card: [ colorType:{0}, textInfo{1},textType{2} ]", colorType, textInfo, textType); 
    }
}
