using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public List<Card> cards = null;

    public List<Card> randomCards = null;

    public List<Card> disposedCards = null;


    int DPCardsByType = 4;
    int DPCardsGeneric = 1;
    int GPCardsByType = 3;
    int GPCardsGeneric = 3;
    int BPCardsByType = 2;
    int BPCardsGeneric = 1;
    int EventCardsQSE = 2;
    int EventCardsGoodEmployees = 2;
    int EventCardCompetitionBP = 2;
    int EventCardSPI = 2;

    public void GenerateListModel()
    {
        cards = new List<Card>();
        disposedCards = new List<Card>();
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
            cards.Add(new BPCard(Card.ColorType.GENERIC));

        }

        for (int i = 0; i < EventCardSPI; i++) cards.Add(new EVCard(EVCard.EventType.SPI_EV));
        for (int i = 0; i < EventCardsQSE; i++) cards.Add(new EVCard(EVCard.EventType.SW_QUALITY_EV));
        for (int i = 0; i < EventCardCompetitionBP; i++) cards.Add(new EVCard(EVCard.EventType.COMP_BP_EV));
        for (int i = 0; i < EventCardsGoodEmployees; i++) cards.Add(new EVCard(EVCard.EventType.GOOD_EMP_EV));



        Debug.Log("Tota cards are" + cards.Count);
    }

    Random random = new Random();
    public void GenerateRandomSort(){
        randomCards = new List<Card>();
        while (cards.Count > 0){
            int val = Random.Range(0, cards.Count);
            randomCards.Add(cards[val]);
            cards.RemoveAt(val);
        }
    }

    void Clear(){
        cards.Clear();
        randomCards.Clear();
        disposedCards.Clear();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
