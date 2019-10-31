using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    public static void WaitForSeconds(MonoBehaviour context,float time, System.Action action)
    {
        context.StartCoroutine(WaitForSecondsCoroutine(time, action));
        //Helper function to wait;

    }
    public static IEnumerator WaitForSecondsCoroutine(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
