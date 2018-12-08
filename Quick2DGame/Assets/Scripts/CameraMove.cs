using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    private float betweenX;
    public float maxPlayerSpace = 19.0f;
    public float maxSpaceX = 8.0f;
    public float speedXRatio = 0.2f;
    public float LeftX = 0.0f;
    public float rightX = 20.0f;


	// Use this for initialization
	void Start () {
        if (player1 == null) player1 = GameObject.Find("Player1");
        if (player2 == null) player2 = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player1 == null) player1 = GameObject.Find("Player1");
        if (player2 == null) player2 = GameObject.Find("Player2");
        this.betweenX = (player1.transform.position.x + player2.transform.position.x) / 2;
        if (Mathf.Abs(this.transform.position.x - this.betweenX) >= maxSpaceX) this.transform.position += new Vector3((this.betweenX - this.transform.position.x)*speedXRatio * Time.deltaTime, 0, 0);
        float farX = Mathf.Max(Mathf.Abs(player1.transform.position.x - this.transform.position.x), Mathf.Abs(player2.transform.position.x - this.transform.position.x));
        if (farX >= maxPlayerSpace / 2)
        {
            this.GetComponent<Camera>().orthographicSize = farX / 1.5f;
            //this.transform.position = new Vector3(this.betweenX, this.transform.position.y, this.transform.position.z);
            this.transform.position += new Vector3((this.betweenX - this.transform.position.x)*speedXRatio * Time.deltaTime, 0, 0);
        }
        else
        {
            this.GetComponent<Camera>().orthographicSize = 5.0f;
        }
        if (this.transform.position.x < LeftX) this.transform.position = new Vector3(LeftX, this.transform.position.y, this.transform.position.z);
        if (this.transform.position.x > rightX) this.transform.position = new Vector3(rightX, this.transform.position.y, this.transform.position.z);
    }
}
