using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public enum State{ INTRO, SET_GAME, PLAYING, FIRST_WIN, END}

    State state = State.INTRO;

    public CardController currentCard;

    public List<string> players;
    List<Hand> hands;
    List<VSEGoals> VSEGoals;

    public CardController[] cardControllers;

    Deck deck;
   

    void SetState(State newState){
        if (this.state != newState){
            this.state = newState;
            OnStateChanged(this.state);
        }
    }

    void OnStateChanged(State newState){

        switch(newState){
            case State.SET_GAME:
                deck.GenerateListModel();
                deck.GenerateRandomSort();
                hands.Clear();
                VSEGoals.Clear();
                foreach(string vse in players){
                    hands.Add(new Hand(vse));

                }

                break;
        }
    }

   

    void OnStarted(List<string> newPlayers){
        Debug.Log("Game has started with players:" + newPlayers);
        players = newPlayers;
        SetState(State.SET_GAME);
       

    }

    void GetAllCardControllers(){
        cardControllers = FindObjectsOfType<CardController>();
    }

    void SetCardControllersListeners(){
        foreach(CardController cardController in cardControllers){
            cardController.bgImage.onClick.AddListener(delegate { ShowCurrentCard(cardController.card); });
        }
    }

    void ShowCurrentCard(Card card){
        currentCard.card = card;
        currentCard.SetGraphics();
    }

	// Use this for initialization
	void Start () {

        hands = new List<Hand>();
        VSEGoals = new List<VSEGoals>();

        GetAllCardControllers();
        SetCardControllersListeners();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
