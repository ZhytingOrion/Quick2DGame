using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ButtonBasic : MonoBehaviour {

    public Sprite originTex;
    public Sprite mouseOnTex;
    public GameObject highLight;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = originTex;
        Transform hl = this.transform.Find("highLight");
        if (hl != null)
        {
            highLight = hl.gameObject;
            highLight.SetActive(false);
            
        }
	}

    private void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().sprite = mouseOnTex;
        if (highLight != null) highLight.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().sprite = originTex;
        if (highLight != null) highLight.SetActive(false);
    }
}
