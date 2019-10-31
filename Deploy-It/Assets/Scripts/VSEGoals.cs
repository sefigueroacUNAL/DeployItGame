using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSEGoals : MonoBehaviour {

    List<DPCard> DPCards;

    public Text VSENameUI;

    public RectTransform mainPanel;

    public RectTransform[] DPPanels;

    public string VSEName;

    public void SetName(string playerName){
        VSEName = playerName;
        VSENameUI.text = VSEName;
    }

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
