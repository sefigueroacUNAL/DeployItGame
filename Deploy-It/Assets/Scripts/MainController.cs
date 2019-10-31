using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public enum State{ INTRO, SET_GAME, PLAYING, FIRST_WIN, END}

    State state = State.INTRO;

    public Message message;

    public IntroScreen introScreen;

    public CardController currentCard;

    public List<string> players;

    public CardController[] cardControllers;

    public Transform prefabPanel;

    public Transform playersPanel;

    List<Hand> hands;
    List<VSEGoals> VSEsGoals;



    public Deck deck;
   

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
                VSEsGoals.Clear();
                foreach(string vse in players){
                    hands.Add(new Hand(vse));
                    VSEGoals vseGoals = Instantiate(prefabPanel, playersPanel).GetComponent<VSEGoals>();
                    vseGoals.SetName(vse);
                    VSEsGoals.Add(vseGoals);

                }
                message.SetTitle(MyResources.GAME_WILL_START);
                message.SetText(MyResources.GAME_WILL_START_SUB);
                message.ShowMessage();
                WaitForSeconds(1f, () => { introScreen.gameObject.SetActive(false); });



                break;
        }
    }

    void WaitForSeconds(float time, System.Action action){
        StartCoroutine(WaitForSecondsCoroutine(time, action));
        //Helper function to wait;
      
    }
    IEnumerator WaitForSecondsCoroutine(float time, System.Action action){
        yield return new WaitForSeconds(time);
        action();
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
        VSEsGoals = new List<VSEGoals>();

        GetAllCardControllers();
        SetCardControllersListeners();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
