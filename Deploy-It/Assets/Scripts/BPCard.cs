using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPCard : Card {

    public BPCard()
    {
        textType = MyResources.BP_TEXT;
        icon = MyResources.BP_SPRITE_ICON;
    }

    public BPCard(ColorType colorType)
    {
        this.colorType = colorType;
        textType = MyResources.BP_TEXT;
        font = MyResources.BP_TEXT_FONT;
        icon = MyResources.BP_SPRITE_ICON;




        switch (colorType)
        {

            case ColorType.GENERIC:
                textInfo = MyResources.BP_GENERIC_TEXT;
                viewcolor = MyResources.GENERIC_COLOR;

                break;

            case ColorType.PM:
                textInfo = MyResources.BP_PM_TEXT;
                viewcolor = MyResources.PM_COLOR;

                break;

            case ColorType.SI:
                textInfo = MyResources.BP_SI_TEXT;
                viewcolor = MyResources.SI_COLOR;

                break;

            case ColorType.IT:
                textInfo = MyResources.BP_IT_TEXT;
                viewcolor = MyResources.IT_COLOR;

                break;
        }

        Debug.Log("BP GENERATED:" + (Card)this);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
