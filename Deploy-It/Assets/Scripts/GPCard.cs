using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPCard : Card
{

    public GPCard()
    {
        textType = MyResources.GP_TEXT;
        icon = MyResources.GP_SPRITE_ICON;
    }

    public GPCard(ColorType colorType)
    {
        this.colorType = colorType;
        textType = MyResources.GP_TEXT;
        font = MyResources.GP_TEXT_FONT;
        icon = MyResources.GP_SPRITE_ICON;




        switch (colorType)
        {

            case ColorType.GENERIC:
                textInfo = MyResources.GP_GENERIC_TEXT;
                viewcolor = MyResources.GENERIC_COLOR;

                break;

            case ColorType.PM:
                textInfo = MyResources.GP_PM_TEXT;
                viewcolor = MyResources.PM_COLOR;

                break;

            case ColorType.SI:
                textInfo = MyResources.GP_SI_TEXT;
                viewcolor = MyResources.SI_COLOR;

                break;

            case ColorType.IT:
                textInfo = MyResources.GP_IT_TEXT;
                viewcolor = MyResources.IT_COLOR;

                break;
        }

        Debug.Log("GP GENERATED:" + (Card)this);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
