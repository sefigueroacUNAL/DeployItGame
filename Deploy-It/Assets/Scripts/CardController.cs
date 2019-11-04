using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class CardController : MonoBehaviour , IPointerEnterHandler {


    //Input fields
    public  Card card;

    //Card GUI items
    public Text textTypeUI;
    public Button bgImage;
    public Image iconImage;
    public Text textInfoUI;
    public Text textSubInfoUI;
    public bool selected = false;
    DPPanelController parent;

    Vector3 initScale;

    public bool autoUpdate = false;

    delegate  void OnCardPointerEnter();

    public Sprite[] sprites;

    public Font[] fonts;

    public UnityEvent cardPointerEnter;

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardPointerEnter.Invoke(); 
    }

    public void SetGraphics(){

        if (card != null )
        {
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
            textSubInfoUI.font = fonts[card.subfont];
            textSubInfoUI.fontSize = 22;
            initScale = transform.localScale;
            parent = GetComponentInParent<DPPanelController>();

            if (card.textSubInfo == "")
                textSubInfoUI.fontSize = 0;
            else
                textSubInfoUI.fontSize = 22;
        }else{
            textInfoUI.text = "NULL";
        }
    }

    public void HighLight(){

        transform.localScale = 1.3f * initScale;
        selected = true;
        
    }

    public void UnHighLight(){
        transform.localScale = initScale;
        selected = false;
    }

   


	// Use this for initialization
	void Start () {

        initScale = transform.localScale;

        bgImage.onClick.AddListener(CardClicked);
	}

    void CardClicked(){
        
        Debug.Log("Card Clicked" + card);
        parent = GetComponentInParent<DPPanelController>();
        if (parent != null){
            
            parent.DPClick.Invoke(); 
        }
    }
	// Update is called once per frame
	void Update () {

        if (autoUpdate) SetGraphics();
		
	}
}
