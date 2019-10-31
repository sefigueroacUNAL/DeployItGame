using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public List<Card> cards = null;

    int DPCardsByType = 5;
    int DPCardsGeneric = 1;
    int GPCardsByType = 4;
    int GPCardsGeneric = 1;
    int BPCardsByType = 4;
    int BPCardsGeneric = 1;
    int EventCardsQSE = 2;
    int EventCardsGoodEmployees = 2;
    int EventCardCompetitionBP = 2;
    int EventCardSPI = 2;

    public void GenerateListModel()
    {
        cards = new List<Card>();
        Debug.Log("Will generate Cards");

        //DP Cards
        for (int i = 0; i < DPCardsByType; i++)
        {
            cards.Add(new DPCard(Card.ColorType.PM));
            cards.Add(new DPCard(Card.ColorType.SI));
            cards.Add(new DPCard(Card.ColorType.IT));
        }

        //DP Cards Generic
        for (int i = 0; i < DPCardsGeneric; i++)
        {
            cards.Add(new DPCard(Card.ColorType.GENERIC));
        }


        //BP Cards
        for (int i = 0; i < BPCardsByType; i++)
        {
            cards.Add(new BPCard(Card.ColorType.PM));
            cards.Add(new BPCard(Card.ColorType.SI));
            cards.Add(new BPCard(Card.ColorType.IT));
        }

        //BP Cards Generic
        for (int i = 0; i < GPCardsGeneric; i++)
        {
            cards.Add(new GPCard(Card.ColorType.GENERIC));
         
        }
        //GP Cards
        for (int i = 0; i < GPCardsByType; i++)
        {
            cards.Add(new GPCard(Card.ColorType.PM));
            cards.Add(new GPCard(Card.ColorType.SI));
            cards.Add(new GPCard(Card.ColorType.IT));
        }

        //GP Cards Generic
        for (int i = 0; i < BPCardsGeneric; i++)
        {
            cards.Add(new GPCard(Card.ColorType.GENERIC));

        }



        Debug.Log("Tota cards are" + cards.Count);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
