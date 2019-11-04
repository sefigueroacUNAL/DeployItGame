using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    //Finit state machiness
    public enum State { INTRO, SET_GAME, PLAYING, FIRST_WIN, END }

    //State machine for the game
    public enum PlayingState { NONE, SET_PLAYER, WAIT_ACTIONS, DO_ACIONS, GET_CARDS, NEXT_PLAYER };

    //State machine when the card COM_BP_EV is played
    public enum EventCompState { NONE, PLAYED, SELECTED_BP, GIVE_BP, FINISH };

    //State machine when the card GOOD_EMPL_EV is played;
    public enum EventGoodEmpState { NONE, PLAYED, SHOW_DP, GET_DP, FINISH};

    //State machine when the card SW_QA_State is played
    public enum EventSwQualityState { NONE, PLAYED, SHOW_DP, SELECT_DP, INTERCHANGE, FINISH };

    PlayingState playingState = PlayingState.NONE;

    State state = State.INTRO;

    EventCompState eventCompState = EventCompState.NONE;

    EventGoodEmpState eventGoodEmpState = EventGoodEmpState.NONE;

    EventSwQualityState eventSwQualityState = EventSwQualityState.NONE;

    public int currentPlayer = 0;

    public EVCard.EventType currentEventType;

    public HandController handController;

    public Message message;

    public Message smallMessage;

    public IntroScreen introScreen;

    public CardController currentCard;

    public CardController eventLastBPCard;

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

                eventCompState = EventCompState.NONE;
                eventGoodEmpState = EventGoodEmpState.NONE;
                eventSwQualityState = EventSwQualityState.NONE;

                handController.hand = hands[currentPlayer];
                handController.SetGraphics();

                foreach (CardController cc in handController.cardControllers){
                    cc.transform.localScale = initCardScale;
                    cc.selected = false;
                }

                if(hands[currentPlayer].cards.Count == 0){
                    
                    discardButton.GetComponentInChildren<Text>().text = MyResources.GET_CARDS_BUTTON_TEXT;
                    message.SetTitle(MyResources.YOU_HAVE_NOT_CARDS_TITLE);
                    message.SetText(MyResources.YOU_HAVE_NOT_CARDS_ACTION);
                    message.ShowMessageTime(MyResources.SHOW_MESSAGE_TIME);

                } else 
                {
                    discardButton.GetComponentInChildren<Text>().text = MyResources.DISCARD_BUTTON_TEXT;
                }
                currentEventType = EVCard.EventType.NONE;   
                GetAllCardControllers();
                SetCardControllersListeners();
                selectedCards.Clear();
                ResetDPPanels();
                playButton.interactable = false;


                SetPlayingState(PlayingState.DO_ACIONS);

                break;

            case PlayingState.DO_ACIONS:

                break;

            case PlayingState.GET_CARDS:
                Debug.Log("We are in get cards state");
                if(ThereIsWinner()){
                    
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
                message.ShowMessageTime(1f);
                SetPlayingState(PlayingState.SET_PLAYER);
                break;

        }

    }

    //EVENTS 

    void OnResetClicked(){
        SetState(State.INTRO);
    }

    void OnPlayCardButtonClicked(){

       

        switch(currentEventType){
            
            case EVCard.EventType.SPI_EV:
                ClearOtherPlayersHands();
                SetPlayingState(PlayingState.GET_CARDS);
                DisposeSelectedCards();
                handController.SetGraphics();

                message.SetTitle(MyResources.SPI_MESSAGE_TITLE);
                message.SetText(MyResources.SPI_MESSAGE_TEXT);
                message.ShowMessageTime(MyResources.SHOW_MESSAGE_TIME);

                break;

            case EVCard.EventType.COMP_BP_EV:
                eventCompState = EventCompState.PLAYED;
                HighLightPlayerBPs();
                DisposeSelectedCards();
                handController.SetGraphics();

                message.SetTitle(MyResources.SPI_MESSAGE_TITLE);
                message.SetText(MyResources.SPI_MESSAGE_TEXT);
                message.ShowMessageTime(MyResources.SHOW_MESSAGE_TIME);
                //Highlight all DP
                break;

            case EVCard.EventType.GOOD_EMP_EV:
                eventGoodEmpState = EventGoodEmpState.PLAYED;
                ShowMissingDPsInOthers();              
                DisposeSelectedCards();
                handController.SetGraphics();
                break;

            case EVCard.EventType.SW_QUALITY_EV:
                eventSwQualityState = EventSwQualityState.PLAYED;
                HighLightOtherPlayersDP();
                DisposeSelectedCards();
                handController.SetGraphics();
                break;
            
        }

       
       

    }

    void HighLightOtherPlayersDP(){
        for (int i = 0; i < VSEsGoals.Count; i++){
            if(i != currentPlayer){
                foreach(DPPanelController dpp in VSEsGoals[i].DPPanels){
                    if (!dpp.isInmune && dpp.HasDP()){
                        dpp.HightLight();
                    }
                }
            }
        }
    }

    void HighLightPlayerBPs(){
        foreach (DPPanelController dpp in VSEsGoals[currentPlayer].DPPanels){
            if (dpp.GetBPs().Count > 0){
                dpp.HightLight();
            }
        }
    }

    void ShowMissingDPsInOthers(){
        for (int i = 0; i < VSEsGoals.Count; i++)
        {
            if (i != currentPlayer)
            {
                for (int j = 0; j < VSEsGoals[i].DPPanels.Length; j++){
                    if( VSEsGoals[i].DPPanels[j].HasDP() && !VSEsGoals[currentPlayer].DPPanels[j].HasDP()){
                        VSEsGoals[i].DPPanels[j].HightLight();
                    }
                }
            }
        }
        
    }

    void GetDP(DPPanelController dpp){
        DPCard dPCard = dpp.GetDP();
        switch(dpp.colorType){
            case Card.ColorType.PM:
                foreach (CardController cc in dpp.GetCards()) 
                    cc.transform.parent = VSEsGoals[currentPlayer].DPPanels[0].transform;
                break;
            case Card.ColorType.SI:
                foreach (CardController cc in dpp.GetCards())
                    cc.transform.parent = VSEsGoals[currentPlayer].DPPanels[1].transform;
                break;
            case Card.ColorType.IT:
                foreach (CardController cc in dpp.GetCards())
                    cc.transform.parent = VSEsGoals[currentPlayer].DPPanels[2].transform;
                break;
        }
    }

    void ClearOtherPlayersHands(){
        for (int i = 0; i < hands.Count; i++)
        {
            if (i != currentPlayer)
            {

                foreach (Card card in hands[i].cards)
                {
                    deck.disposedCards.Add(card);

                }
                hands[i].cards.Clear();
            }
        }
    }

    void DisposeSelectedCards(){

        foreach(CardController cc in selectedCards){
            cc.transform.localScale = initCardScale;
            deck.disposedCards.Add((Card)cc.card.Clone());
            hands[currentPlayer].cards.Remove(cc.card);
          
            cc.card = null;
        }
        
      
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
                    
                    if (eventCompState == EventCompState.PLAYED)
                    {
                        //The bpp already played
                        List<CardController> bps = dPPanel.GetBPs();

                        CardController cardController = dPPanel.GetBPs()[0];
                        eventLastBPCard = cardController;
                        BPCard bPCard = cardController.card as BPCard;
                        cardController.HighLight();
                        eventCompState = EventCompState.SELECTED_BP;
                        ResetDPPanels();
                        BPAction(bPCard);
                        return;

                        break;
                    }

                    if (eventCompState == EventCompState.SELECTED_BP)
                    {

                        //Add currentCard to dpp
                        eventLastBPCard.transform.parent = dPPanel.transform;
                        eventLastBPCard.UnHighLight();
                        eventCompState = EventCompState.PLAYED;
                        CheckPanel(dPPanel);
                        ResetDPPanels();
                        HighLightPlayerBPs();


                        return;
                        break;
                    }

                    if(eventGoodEmpState == EventGoodEmpState.PLAYED){
                        
                        GetDP(dPPanel);
                        CheckPanel(dPPanel);
                        eventGoodEmpState = EventGoodEmpState.NONE;
                        SetPlayingState(PlayingState.GET_CARDS);
                        return;
                        break;
                    }

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
        playButton.interactable = false;

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
                EVAction(card);
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


    void EVAction(Card card){
        

        Debug.Log("Card is EVAction");

        EVCard eVCard = (EVCard)card;

        currentEventType = eVCard.eventType;

        Debug.Log("Card is EVAction with Event:" + currentEventType);


        playButton.interactable = true;


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

    bool ThereIsWinner(){
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
                Destroy(cc.gameObject);
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
        playButton.onClick.AddListener(OnPlayCardButtonClicked);

        message.gameObject.SetActive(true);
        smallMessage.gameObject.SetActive(true);
        smallMessage.HideMessage(0f);




        foreach (CardController cc in handController.cardControllers)
        {
            cc.selected = false;
            cc.bgImage.onClick.AddListener(delegate
            {
                SelectCardInHand(cc);

            });
        }

        introScreen.gameObject.SetActive(true);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
                             

}
