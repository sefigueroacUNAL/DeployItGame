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
        font = MyResources.DP_TEXT_FONT;
        icon = MyResources.BP_SPRITE_ICON;
       

       

        switch(colorType){
            
            case ColorType.GENERIC:
                textInfo = MyResources.DP_GENERIC_TEXT;
                viewcolor = MyResources.GENERIC_COLOR;
                icon = MyResources.GENERIC_SPRITE_ICON;
                break;

            case ColorType.PM: 
                textInfo = MyResources.DP_PM_TEXT;
                viewcolor = MyResources.PM_COLOR;
                icon = MyResources.PM_SPRITE_ICON;
                break;

            case ColorType.SI:
                textInfo = MyResources.DP_SI_TEXT;
                viewcolor = MyResources.SI_COLOR;
                icon = MyResources.SI_SPRITE_ICON;
                break;

            case ColorType.IT:
                textInfo = MyResources.DP_IT_TEXT;
                viewcolor = MyResources.IT_COLOR;
                icon = MyResources.SI_SPRITE_ICON;
                break;
        }

        Debug.Log("DP GENERATED:" + (Card)this);
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
