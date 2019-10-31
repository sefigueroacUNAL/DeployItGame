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
    public Button bgImage;
    public Image iconImage;
    public Text textInfoUI;
    public Text textSubInfoUI;

    public bool autoUpdate = false;

    public Sprite[] sprites;

    public Font[] fonts;
 
    public void SetGraphics(){

        ColorBlock colorBlock = bgImage.colors;
        colorBlock.normalColor = card.viewcolor;
        colorBlock.highlightedColor = card.viewcolor;
        //colorBlock.pressedColor = card.viewcolor;
        //colorBlock.disabledColor = card.viewcolor;
        bgImage.colors = colorBlock;
        iconImage.sprite = sprites[card.icon];
        textInfoUI.font = fonts[card.font];
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
