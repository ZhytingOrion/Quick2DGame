using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_PauseButton : MonoBehaviour {

    public Sprite pauseSprite;
    public Sprite playSprite;
    private bool isPause = false;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = pauseSprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecoverGame()
    {
        this.GetComponent<SpriteRenderer>().sprite = pauseSprite;
        isPause = false;
        Game.Instance.gameState = GameState.Play;
    }

    private void OnMouseDown()
    {
        if(isPause)
        {
            this.GetComponent<SpriteRenderer>().sprite = pauseSprite;
            isPause = false;
            Game.Instance.gameState = GameState.Play;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = playSprite;
            isPause = true;
            Game.Instance.gameState = GameState.Pause;
            this.transform.Find("PauseScene").gameObject.SetActive(true);
        }
    }
}
