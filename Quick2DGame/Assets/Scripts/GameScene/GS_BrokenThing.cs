using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_BrokenThing : MonoBehaviour {

    public Sprite brokenTex;
    public Sprite beforeBrokenTex;
    private bool isBroken = false;
    private float MaxDistance = 0.2f;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = beforeBrokenTex;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "statue")
        {
            if(Mathf.Abs(other.transform.position.y - this.transform.position.y) <= this.MaxDistance)
            isBroken = true;
            this.GetComponent<SpriteRenderer>().sprite = brokenTex;
            Destroy(this.GetComponent<BoxCollider>());
        }
    }
}
