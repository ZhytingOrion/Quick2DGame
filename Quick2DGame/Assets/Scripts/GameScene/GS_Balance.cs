using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Balance : MonoBehaviour {

    private float massLeft;
    private float massRight;
    public GameObject LeftPlane;
    public GameObject RightPlane;
    public float maxY = 0.0f;
    public float minY = -3.0f;
    private GameObject[] statueList;
    private Dictionary<RoleState, RoleInfo> dic;

	// Use this for initialization
	void Start () {
        massLeft = 0;
        massRight = 0;
        dic = GameObject.Find("_Init").GetComponent<GS_GameInit>().roleInfosDic;
    }
	
	// Update is called once per frame
	void Update () {
        statueList = GameObject.FindGameObjectsWithTag("statue");
        getMass();
        if(massLeft < massRight)
        {
            LeftPlane.transform.position += new Vector3(0, (minY - LeftPlane.transform.position.y) * Time.deltaTime, 0);
            RightPlane.transform.position += new Vector3(0, (maxY - LeftPlane.transform.position.y) * Time.deltaTime, 0);
        }
        else if(massLeft > massRight)
        {
            LeftPlane.transform.position += new Vector3(0, (maxY - LeftPlane.transform.position.y) * Time.deltaTime, 0);
            RightPlane.transform.position += new Vector3(0, (minY - LeftPlane.transform.position.y) * Time.deltaTime, 0);
        }
	}

    private void getMass()
    {
        massLeft = 0;
        massRight = 0;
        float RightY = RightPlane.transform.position.y;
        float LeftY = LeftPlane.transform.position.y;
        float RightSize = RightPlane.GetComponent<BoxCollider>().size.x * 0.5f;
        float LeftSize = LeftPlane.GetComponent<BoxCollider>().size.x * 0.5f;

        for(int i = 0; i<statueList.Length; ++i)
        {
            if(Mathf.Abs(statueList[i].transform.position.y - RightY) <= RightSize)
            {
                massRight += dic[statueList[i].GetComponent<GS_Statue>().statueState].roleMass;
            }
            else if (Mathf.Abs(statueList[i].transform.position.y - LeftY) <= LeftSize)
            {
                massLeft += dic[statueList[i].GetComponent<GS_Statue>().statueState].roleMass;
            }
        }
    }
}
