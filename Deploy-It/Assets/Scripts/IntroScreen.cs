using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour {


    public Button StartButton;
    public GameObject receiver;
    public Message message;

    public Toggle[] toogles;
    public InputField[] inputFields;

    List<string> players;

	// Use this for initialization
	void Start () {
        players = new List<string>();

        foreach(Toggle t in toogles){
            t.isOn = false;
        }

        StartButton.onClick.AddListener(OnButtonStart);

	}

    public void OnButtonStart(){

        if(checkPlayers() > 1){
            receiver.SendMessage("OnStarted", players);
        }else{
            message.SetTitle(MyResources.NO_PLAYERS_SELECTED);
            message.SetText(MyResources.NO_PLAYERS_SELECTED_INFO);
            message.ShowMessage();
        }
    }
	
    int checkPlayers(){
        players.Clear();
        int count = 0;
        for (int i = 0; i < toogles.Length; i++){
            Toggle t = toogles[i];
            string vse = inputFields[i].text;
            if (t.isOn && vse != "") 
            {
                count++;
                players.Add(inputFields[i].text);
            }
        }
        return count;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
