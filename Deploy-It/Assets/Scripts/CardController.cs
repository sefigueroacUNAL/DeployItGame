using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class CardController : MonoBehaviour {


    //Input fields
    public Card card;

    //Card GUI items
    public Text textTypeUI;
    public Image bgImage;
    public Image iconImage;
    public Text textInfoUI;
    public Text textSubInfoUI;

    public bool autoUpdate = false;



    void SetGraphics(){
        
        bgImage.color = card.viewcolor;
        iconImage.sprite = card.icon;
        textInfoUI.font = card.font;
        textInfoUI.text = card.textInfo;
        textTypeUI.text = card.textType;
        textSubInfoUI.text = card.textSubInfo;

        if (card.textSubInfo == "")
            textSubInfoUI.fontSize = 0;
        else
            textSubInfoUI.fontSize = 12;
    }


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (autoUpdate) SetGraphics();
		
	}
}
