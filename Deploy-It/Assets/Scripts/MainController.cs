using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    public enum State { INTRO, SET_GAME, PLAYING, FIRST_WIN, END }

    public enum PlayingState { NONE, SET_PLAYER, WAIT_ACTIONS, DO_ACIONS, GET_CARDS, NEXT_PLAYER };

    PlayingState playingState = PlayingState.NONE;

    State state = State.INTRO;

    public int currentPlayer = 0;

    public HandController handController;

    public Message message;

    public IntroScreen introScreen;

    public CardController currentCard;

    public List<CardController> selectedCards;

    public List<string> players;

    public CardController[] cardControllers;

    public DPPanelController[] panelControllers;

    public Transform prefabPanel;

    public Transform playersPanel;

    public Button discardButton;

    public Button playButton;

    public Button resetButton;

    public 

    List<Hand> hands;

    List<VSEGoals> VSEsGoals;

    Vector3 initCardScale;

    Vector3 initDPScale;

    int totalCardsSelected;

    public Deck deck;

    void SetState(State newState)
    {
        if (this.state != newState)
        {
            this.state = newState;
            OnStateChanged(this.state);
        }
    }

    void SetPlayingState(PlayingState newPlayingState)
    {
        if (this.playingState != newPlayingState)
        {
            this.playingState = newPlayingState;
            OnPlayingStateChanged(playingState);

        }
    }

    void OnStateChanged(State newState)
    {

        switch (newState)
        {
            case State.INTRO:
                introScreen.gameObject.SetActive(true);

                foreach(VSEGoals goals in VSEsGoals){
                    Destroy(goals.gameObject);
                }

                SetPlayingState(PlayingState.NONE);
                break;


            case State.SET_GAME:

                GetAllCardControllers();
                SetCardControllersListeners();

                deck.GenerateListModel();
                deck.GenerateRandomSort();
                hands.Clear();
                selectedCards.Clear();
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

                    vseGoals.DPPanels[0].colorType = Card.ColorType.PM;
                    vseGoals.DPPanels[1].colorType = Card.ColorType.SI;
                    vseGoals.DPPanels[2].colorType = Card.ColorType.IT;


                }

                initCardScale = handController.cardControllers[0].transform.localScale;
                initDPScale = VSEsGoals[0].DPPanels[0].transform.localScale;

                foreach (CardController cc in handController.cardControllers)
                {
                    cc.selected = false;
                    cc.bgImage.onClick.AddListener(delegate
                    {
                        SelectCardInHand(cc);

                    });
                }

             
                GetAllPanelControllers();
                SetAllPanelControllersListeners();

                message.SetTitle(MyResources.GAME_WILL_START);
                message.SetText(MyResources.GAME_WILL_START_SUB);
                message.ShowMessage();

                Utils.WaitForSeconds(this, 0.3f, () => { introScreen.gameObject.SetActive(false); });

                currentPlayer = 0;

                SetPlayingState(PlayingState.SET_PLAYER);

                break;
        }
    }

    void OnPlayingStateChanged(PlayingState newPlayingState)
    {

        switch (newPlayingState)
        {
            case PlayingState.SET_PLAYER:
                //Load cards on current hand
                totalCardsSelected = 0;
                handController.hand = hands[currentPlayer];
                handController.SetGraphics();

                foreach (CardController cc in handController.cardControllers){
                    cc.transform.localScale = initCardScale;
                    cc.selected = false;
                }
                    
                GetAllCardControllers();
                SetCardControllersListeners();
                selectedCards.Clear();
                ResetDPPanels();
                SetPlayingState(PlayingState.DO_ACIONS);

                break;

            case PlayingState.DO_ACIONS:

                break;

            case PlayingState.GET_CARDS:

                if(CheckWinner()){
                    
                    message.SetTitle(players[currentPlayer] + MyResources.WINNER_TITLE_SUFIX);
                    message.SetText(players[currentPlayer] + MyResources.WINNER_TEXT_SUFIX);
                    message.ShowMessage();
                    SetState(State.INTRO);
                    return;
                }

                GetCards();
                currentPlayer = (currentPlayer + 1) % players.Count;
                SetPlayingState(PlayingState.NEXT_PLAYER);
                break;
            case PlayingState.NEXT_PLAYER:
                message.SetTitle("Next VSE is " + players[currentPlayer]);
                message.SetText("");
                message.ShowMessage();
                SetPlayingState(PlayingState.SET_PLAYER);
                break;

        }

    }

    //EVENTS 

    void OnResetClicked(){
        SetState(State.INTRO);
    }

    void OnStarted(List<string> newPlayers)
    {
        Debug.Log("Game has started with players:" + newPlayers);
        players = newPlayers;

        SetState(State.SET_GAME); //When checkstate is not on update this must be the last instrucion;

        discardButton.onClick.AddListener(OnDiscard);

    }

    void OnDiscard()
    {
        Debug.Log("Discarded Cards");
        foreach (CardController cc in selectedCards)
        {
            hands[currentPlayer].cards.Remove(cc.card);
            deck.disposedCards.Add(cc.card);
        }

        SetPlayingState(PlayingState.GET_CARDS);

    }

    void OnPanelClicked(DPPanelController dPPanel)
    {
        Debug.Log("PanelClicked" + dPPanel.name);
        switch (playingState)
        {

            case PlayingState.DO_ACIONS:
                if (dPPanel.isHighLight && !dPPanel.isInmune)
                {
                    foreach (CardController cc in selectedCards)
                    {
                        cc.selected = false;
                        cc.transform.localScale = initCardScale;
                        //cc.transform.parent = dPPanel.transform;

                        hands[currentPlayer].cards.Remove(cc.card);

                        CardController newCC = Instantiate(cc, dPPanel.transform);

                        newCC.card = (Card)cc.card.Clone();
                        newCC.SetGraphics();
                        newCC.cardPointerEnter.AddListener(delegate
                        {

                            ShowCurrentCard(newCC.card);
                        });

                        totalCardsSelected--;
                        //selectedCards.Remove(cc);
                        cc.card = null;

                    }
                    handController.SetGraphics();
                    selectedCards.Clear();

                    SetPlayingState(PlayingState.GET_CARDS);
                }

                CheckPanel(dPPanel); 
                break;
        }
    }


    void SelectCardInHand(CardController cardController)
    {

        if (!cardController.selected)
        {
            cardController.transform.localScale = 1.3f * initCardScale;
            cardController.selected = true;
            selectedCards.Add(cardController);
            totalCardsSelected++;
        }
        else
        {
            cardController.transform.localScale = initCardScale;
            cardController.selected = false;
            selectedCards.Remove(cardController);
            totalCardsSelected--;
        }

        CheckPosibleActions();
    }

    void CheckPosibleActions()
    {

        ResetDPPanels();

        if (selectedCards.Count == 1)
        {
            //We should detect the type of card
            Card card = selectedCards[0].card;

            discardButton.interactable = true;

            if (card is DPCard)
            {
                DPAction(card);
            }
            else if (card is BPCard)
            {
                BPAction(card);
            }
            else if (card is GPCard)
            {
                GPAction(card);
            }
            else if (card is EVCard)
            {
                Debug.Log("Card is EV");
            }
        }

    }

    void DPAction(Card card)
    {

        Debug.Log("Card is DP");

        DPCard dPCard = (DPCard)card;
        VSEGoals goals = VSEsGoals[currentPlayer];

        switch (card.colorType)
        {
            case Card.ColorType.PM:
                if (!goals.HasDP(0))
                    goals.HighLight(0);
                break;

            case Card.ColorType.SI:
                if (!goals.HasDP(1))
                    goals.HighLight(1);
                break;

            case Card.ColorType.IT:
                if (!goals.HasDP(2))
                    goals.HighLight(2);
                break;
            case Card.ColorType.GENERIC:

                foreach (DPPanelController panel in VSEsGoals[currentPlayer].DPPanels)
                    if (!panel.HasDP())
                        panel.HightLight();

                break;

        }
    }

    void BPAction(Card card)
    {

        foreach (VSEGoals goals in VSEsGoals)
            foreach (DPPanelController dpp in goals.DPPanels)
            {
                DPCard dPCard = dpp.GetDP();
            if ((dpp.colorType == card.colorType || card.colorType == Card.ColorType.GENERIC) && dpp.HasDP()){
                    dpp.HightLight();
            }
                
            }
    }

  

    void GPAction(Card card)
    {

        foreach (VSEGoals goals in VSEsGoals)
            foreach (DPPanelController dpp in goals.DPPanels)
            {
               
                if ((dpp.colorType == card.colorType || card.colorType == Card.ColorType.GENERIC) && dpp.HasDP())
                {
                    dpp.HightLight();
                }

            }
    }
                            

    void ResetDPPanels()
    {
        foreach (VSEGoals goals in VSEsGoals)
            foreach (DPPanelController panel in goals.DPPanels)
                panel.UnHightLight();
    }

    void GetCards()
    {

        while (hands[currentPlayer].cards.Count < MyResources.CARDS_BY_HAND
               && deck.randomCards.Count > 0)
        {
            Card card = deck.randomCards[0];
            hands[currentPlayer].cards.Add(card);
            deck.randomCards.Remove(card);
        }
    }

    bool CheckWinner(){
        int AccomplishedDPP = 0;
        //We count the number of Deployment Packages thar not unacomplished
        foreach(DPPanelController dpp in VSEsGoals[currentPlayer].DPPanels){
            if( dpp.HasDP() && dpp.GetBPs().Count == 0){
                AccomplishedDPP++;
            }
        }
        if (AccomplishedDPP == MyResources.TARGET_DP_NUMBER){
            Debug.Log("We have a winner");
            return true;
        }
        return false;
    }

    void GetAllCardControllers()
    {
        cardControllers = FindObjectsOfType<CardController>();
    }

    void SetCardControllersListeners()
    {
        foreach (CardController cardController in cardControllers)
        {
            //cardController.bgImage.onClick.AddListener(delegate { ShowCurrentCard(cardController.card); });
            cardController.cardPointerEnter.AddListener(delegate { ShowCurrentCard(cardController.card); });
        }
    }

    void GetAllPanelControllers()
    {
        panelControllers = FindObjectsOfType<DPPanelController>();
    }

    void SetAllPanelControllersListeners()
    {
        foreach (DPPanelController dpp in panelControllers)
            dpp.DPClick.AddListener(delegate { OnPanelClicked(dpp); });
    }

    void ShowCurrentCard(Card card)
    {
        currentCard.card = card;
        currentCard.SetGraphics();
    }

    void CheckPanel(DPPanelController dPPanelController){

        List<CardController> BPs;
        List<CardController> GPs;
        BPs = dPPanelController.GetBPs();
        GPs = dPPanelController.GetGPs();

        //Bad and good practice
        if (BPs.Count == 1 &&  GPs.Count == 1){
            deck.disposedCards.Add((Card)BPs[0].card.Clone());
            deck.disposedCards.Add((Card)GPs[0].card.Clone());

            Destroy(BPs[0].gameObject);
            Destroy(GPs[0].gameObject);

            Debug.Log("Will destroy BP and GP");

        }

        //Bad practice added to imunne
        if(dPPanelController.isInmune && BPs.Count >= 1){
            foreach(CardController cc in BPs){
                deck.disposedCards.Add((Card)BPs[0].card.Clone());
                Destroy(BPs[0].gameObject);
            }
        }

        if(GPs.Count == 2 && BPs.Count == 0){
            dPPanelController.isInmune = true;
        }

        if(BPs.Count == 2 && GPs.Count == 0){
            foreach(CardController cc in dPPanelController.GetCards()){
                deck.disposedCards.Add((Card)cc.card.Clone());
                Destroy(cc);
            }
        }

      
    }

    // Use this for initialization
    void Start()
    {

        hands = new List<Hand>();
        selectedCards = new List<CardController>();
        VSEsGoals = new List<VSEGoals>();
        resetButton.onClick.AddListener(OnResetClicked);

        introScreen.gameObject.SetActive(true);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
                             

}
