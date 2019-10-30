using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPCard: Card {

    public  DPCard(){
        textType = MyResources.DP_TEXT;
        icon = MyResources.PM_SPRITE_ICON;
    }

    public DPCard(ColorType colorType)
    {
        this.colorType = colorType;
        textType = MyResources.DP_TEXT;
        icon = MyResources.PM_SPRITE_ICON;

        switch(colorType){
            case col:
                textInfo = MyResources.BP_GENERIC_TEXT;
                viewcolor = MyResources.GENERIC_COLOR;
                break;
            case 1: 
                textInfo = MyResources.BP_PM_TEXT;
                viewcolor = MyResources.PM_COLOR;
                break;
            case 2:
                textInfo = MyResources.BP_SI_TEXT;

                break;
            case 3:
                textInfo = MyResources.BP_IT_TEXT;
                break;
        }
    }

    //Input field
	// Use this for initialization
	void Start () {
        
      //  textType = MyResources.DP_TEXT;
      //  icon = MyResources.PM_SPRITE_ICON;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
