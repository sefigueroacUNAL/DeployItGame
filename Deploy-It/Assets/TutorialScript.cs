using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    // Use this for initialization
    public Sprite[] sprites;

    public Button button;
    public Button closeButton;
    public Image image;

    GameObject receiver;

    int index;

    public void SetIndex(int index){
        this.index = index;
    }

    public int GetIndex(){
        return this.index;
    }


	void Start () {

        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        closeButton.onClick.AddListener(OnCloseButtonClicked);

        image = GetComponent<Image>();

        SetIndex(0);
        SetImage(0);

        receiver = GameObject.FindObjectOfType<MainController>().gameObject;
	}

    void OnCloseButtonClicked(){

        EndTutorial();



    }

    void EndTutorial(){

        gameObject.SetActive(false);

        receiver.SendMessage("OnTutorialEnded");

        SetIndex(0);
        SetImage(0);
        
    }

    void OnButtonClicked(){

        index++;

        if(index >= sprites.Length){

            index = 0;
        }

        SetImage(index);
    }

    void SetImage(int index){

        image.sprite = sprites[index];
        
    }

    void SetSprite(int i){
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
