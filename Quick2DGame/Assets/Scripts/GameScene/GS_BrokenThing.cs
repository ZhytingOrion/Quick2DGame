using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_BrokenThing : MonoBehaviour {

    public Sprite brokenTex;
    public Sprite beforeBrokenTex;
    private bool isBroken = false;
    private float MaxDistance = 0.2f;
    private AudioSource audioSource;
    public AudioClip clipSmash;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = beforeBrokenTex;
        this.audioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "statue")
        {
            //播音效
            this.audioSource.clip = this.clipSmash;
            //if (this.audioSource.isPlaying) this.audioSource.Stop();
            this.audioSource.Play();

            if (Mathf.Abs(other.transform.position.y - this.transform.position.y) <= this.MaxDistance)
            isBroken = true;
            this.GetComponent<SpriteRenderer>().sprite = brokenTex;
	
            List<BoxCollider> collider = new List<BoxCollider>(this.GetComponents<BoxCollider>());
	for(int i = 0; i<collider.Count; ++i)
{
	Destroy(collider[i]);
}
        }
    }
}
