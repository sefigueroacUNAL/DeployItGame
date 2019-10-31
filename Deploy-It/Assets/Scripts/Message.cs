using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Message : MonoBehaviour, IPointerClickHandler {

    public string title;
    public string infoText;

    public Image background;
    public Text titleUI;
    public Text infoTextUI;

    bool showed = true;

    public bool autoUpdate = false;

    public float transitionTime = 0.3f;




    public void SetTitle(string title){
        this.title = title;
        titleUI.text = title;

    }

    public void SetText(string text){
        this.infoText = text;
        infoTextUI.text = text;
    }

	// Use this for initialization
	void Start () {
        HideMessage(0f);

	}
	
	// Update is called once per frame
	void Update () {
        if( autoUpdate){
            titleUI.text = title;
            infoTextUI.text = infoText;
        }		
	}

    public void ShowMessage(float time){

        background.raycastTarget = true;
        titleUI.raycastTarget = true;
        infoTextUI.raycastTarget = true;

        background.CrossFadeAlpha(1f, time, false);
        titleUI.CrossFadeAlpha(1f, time, false);
        infoTextUI.CrossFadeAlpha(1f, time, false);

     
        showed = true;
        Debug.Log("Showed Message");

    }
   

    public void HideMessage(float time){

      
        background.raycastTarget = false;
        titleUI.raycastTarget = false;
        infoTextUI.raycastTarget = false;

        background.CrossFadeAlpha(0f, time, false);
        titleUI.CrossFadeAlpha(0f, time, false);
        infoTextUI.CrossFadeAlpha(0f, time, false);

        showed = false;
        Debug.Log("Hided Message");

    }

    public void ShowMessage()
    {
        ShowMessage(transitionTime);
    }

    public void HideMessage(){
        HideMessage(transitionTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Clicked");
        if (showed)
            HideMessage(transitionTime);
        //throw new System.NotImplementedException();
    }
}
