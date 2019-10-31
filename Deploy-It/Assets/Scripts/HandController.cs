using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour {


    public Hand hand;

    public Text handName;

    public Transform handPanel;

    public CardController[] cardControllers;

    public void SetGraphics(){

        SetName(hand.playerName);

        for (int i = 0; i < MyResources.CARDS_BY_HAND; i++){
            if (i < hand.cards.Count)
            {
                cardControllers[i].card = hand.cards[i];
                cardControllers[i].SetGraphics();
                cardControllers[i].gameObject.SetActive(true);
            }else{
                cardControllers[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetName(string name){
        handName.text = name + " Hand";
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
