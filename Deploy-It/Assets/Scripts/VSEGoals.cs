using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSEGoals : MonoBehaviour {

    //List<DPCard> DPCards;



    public Text VSENameUI;

    public RectTransform mainPanel;

    public DPPanelController[] DPPanels;

    public string VSEName;

    public void SetName(string playerName){
        VSEName = playerName;
        VSENameUI.text = VSEName;
    }

    public bool HasDP(int i){
        return DPPanels[i].HasDP();
    }

    public void HighLight(int i){
        
        DPPanels[i].HightLight();

    }

    public void UnHighLight(int i)
    {
        DPPanels[i].UnHightLight();
    }

	// Use this for initialization
	void Start () {
     
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
