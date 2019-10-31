using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand{

    string playerName;
    public List<Card> cards;

    public Hand(string playerName){
        this.playerName = playerName;
        cards = new List<Card>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
