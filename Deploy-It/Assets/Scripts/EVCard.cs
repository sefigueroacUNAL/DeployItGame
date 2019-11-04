using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVCard : Card {

    public enum EventType {NONE,SW_QUALITY_EV,COMP_BP_EV,GOOD_EMP_EV,SPI_EV}
    public EventType eventType;

    public EVCard(EventType newEventType){
        eventType = newEventType;
        viewcolor = MyResources.GENERIC_COLOR;
        icon = MyResources.EVENT_ICON;
        font = MyResources.EV_TEXT_FONT;
        subfont = MyResources.EV_SUBTEXT_FONT;


        switch(eventType){

            case EventType.SW_QUALITY_EV:
                textInfo = MyResources.EVENT_SW_QUALITY_EVENTS;
                textSubInfo = MyResources.EVENT_SW_QUALITY_EVENTS_SUB;
                break;
            case EventType.COMP_BP_EV:
                textInfo = MyResources.EVENT_COMPETITION_BAD_PRACTICES;
                textSubInfo = MyResources.EVENT_COMPETITION_BAD_PRACTICES_SUB;
                break;
            case EventType.GOOD_EMP_EV:
                textInfo = MyResources.EVENT_ATTRACT_GOOD_EMPLOYEES;
                textSubInfo = MyResources.EVENT_ATTRACT_GOOD_EMPLOYEES_SUB;
                break;
            case EventType.SPI_EV:
                textInfo = MyResources.EVENT_SOFTWARE_PROCESS_IMPROVEMENT;
                textSubInfo = MyResources.EVENT_SOFTWARE_PROCESS_IMPROVEMENT_SUB;
                break;


        }

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
