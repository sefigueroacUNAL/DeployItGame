using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public enum State{ INTRO, SET_GAME, PLAYING, FIRST_WIN, END}

    public enum PlayingState { NONE,SET_PLAYER, WAIT_ACTIONS, DO_ACIONS, GET_CARDS, NEXT_PLAYER };

    PlayingState playingState = PlayingState.NONE;

    State state = State.INTRO;

    public int currentPlayer = 0;

    public HandController handController;

    public Message message;

    public IntroScreen introScreen;

    public CardController currentCard;

    public List<string> players;

    public CardController[] cardControllers;

    public Transform prefabPanel;

    public Transform playersPanel;

    List<Hand> hands;
    List<VSEGoals> VSEsGoals;

    Vector3 initCardScale;



    public Deck deck;
   

    void SetState(State newState){
        if (this.state != newState){
            this.state = newState;
            OnStateChanged(this.state);
        }
    }

    void SetPlayingState(PlayingState newPlayingState){
        if (this.playingState != newPlayingState){
            this.playingState = newPlayingState;
            OnPlayingStateChanged(playingState);

        }
    }

    void OnStateChanged(State newState){

        switch(newState){
            case State.SET_GAME:
                
                deck.GenerateListModel();
                deck.GenerateRandomSort();
                hands.Clear();
                VSEsGoals.Clear();
                for (int p = 0; p < players.Count; p++)
                {
                    string vse = players[p];

                    Hand hand = new Hand(vse);

                    //Give Hands
                  
                        for (int i = 0; i < MyResources.CARDS_BY_HAND; i++)
                        {
                            Card card = deck.randomCards[0];
                            hand.cards.Add(card);
                            deck.randomCards.Remove(card);
                        Debug.Log("Card:" + card.colorType + " Added to hand:" + p);
                        }

                        
                    hands.Add(hand);
                    VSEGoals vseGoals = Instantiate(prefabPanel, playersPanel).GetComponent<VSEGoals>();
                    vseGoals.SetName(vse);
                    VSEsGoals.Add(vseGoals);
                }

                initCardScale = handController.cardControllers[0].transform.localScale;

                handController.cardControllers[0].bgImage.onClick.AddListener(delegate {
                    SelectCardInHand(handController.cardControllers[0]);
                });

                handController.cardControllers[1].bgImage.onClick.AddListener(delegate {
                    SelectCardInHand(handController.cardControllers[1]);
                });

                handController.cardControllers[2].bgImage.onClick.AddListener(delegate {
                    SelectCardInHand(handController.cardControllers[2]);
                });

              

                message.SetTitle(MyResources.GAME_WILL_START);
                message.SetText(MyResources.GAME_WILL_START_SUB);
                message.ShowMessage();

                Utils.WaitForSeconds(this,0.3f, () => { introScreen.gameObject.SetActive(false); });

                currentPlayer = 0;

                SetPlayingState(PlayingState.SET_PLAYER);

                break;
        }
    }

    void OnPlayingStateChanged(PlayingState newPlayingState){

        switch( newPlayingState){
            case PlayingState.SET_PLAYER:
                //Load cards on current hand
                handController.hand = hands[currentPlayer];
                handController.SetGraphics();
                GetAllCardControllers();
                SetCardControllersListeners();

                break;
        }
        
    }
   

    void OnStarted(List<string> newPlayers){
        Debug.Log("Game has started with players:" + newPlayers);
        players = newPlayers;

        SetState(State.SET_GAME); //When checkstate is not on update this must be the last instrucion;
       

    }

    void GetAllCardControllers(){
        cardControllers = FindObjectsOfType<CardController>();
    }

    void SetCardControllersListeners(){
        foreach (CardController cardController in cardControllers)
        {
            //cardController.bgImage.onClick.AddListener(delegate { ShowCurrentCard(cardController.card); });
            cardController.cardPointerEnter.AddListener(delegate { ShowCurrentCard(cardController.card); });
        }
    }

    void ShowCurrentCard(Card card){
        currentCard.card = card;
        currentCard.SetGraphics();
    }

    void SelectCardInHand(CardController cardController){
        foreach( CardController cController in handController.cardControllers)
        {
            if( cardController == cController){
                cardController.transform.localScale = 1.3f * initCardScale;
            }else{
                cController.transform.localScale = initCardScale;
            }
        }
       
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
