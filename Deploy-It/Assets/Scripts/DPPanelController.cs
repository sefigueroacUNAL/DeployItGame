using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPPanelController : MonoBehaviour, IPointerClickHandler{

    public UnityEvent DPClick;

    Image image;

    public bool isHighLight;

    public void OnPointerClick(PointerEventData eventData)
    {
        DPClick.Invoke();
    }

    public bool HasDP(){
        return transform.childCount > 0;
    }

    public void HightLight(){
        isHighLight = true;
        GetComponent<Image>().CrossFadeColor(MyResources.ACTIONABLE_PANEL_COLOR, 0.3f, false, true);
    }

    public void UnHightLight()
    {
        isHighLight = false;
        GetComponent<Image>().CrossFadeColor(MyResources.NORMAL_PANEL_COLOR, 0.3f, false, true);
    }

    // Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
