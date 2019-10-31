using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour {


    public Deck deck;
    public List<CardController> cardControllers = null;

    public Transform parent;
    public Transform prefab;
   

	// Use this for initialization
	void Start () {
        
        if( deck.cards == null){
            deck.GenerateListModel();
        }

        InstantiateFromModel();
	}


    void InstantiateFromModel(){

        if (cardControllers == null)
            cardControllers = new List<CardController>();
        foreach(Card card in deck.cards){
            CardController cardController = Instantiate(prefab, parent).GetComponent<CardController>();
            cardController.card = card;
            cardController.SetGraphics();
            cardControllers.Add(cardController);
        }
    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
