using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_PauseCancel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        this.transform.parent.parent.GetComponent<GS_PauseButton>().RecoverGame();
        this.transform.parent.gameObject.SetActive(false);
    }
}
