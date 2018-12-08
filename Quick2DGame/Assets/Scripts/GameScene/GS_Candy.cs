using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Candy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            //糖果数量++
            switch(other.gameObject.name)
            {
                case "Player1":
                    Game.Instance.Role1CandyCount++;
                    break;
                case "Player2":
                    Game.Instance.Role2CandyCount++;
                    break;
                default:
                    break;
            }
            //失去糖果
            Destroy(this.gameObject);
        }
    }
}
