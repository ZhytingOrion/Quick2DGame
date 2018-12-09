using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Balance : MonoBehaviour {

    private float massLeft;
    private float massRight;
    public GameObject LeftPlane;
    public GameObject RightPlane;
    public List<GameObject> leftStatueList = new List<GameObject>();
    public List<GameObject> rightStatueList = new List<GameObject>();
    public float maxY = 0.0f;
    public float minY = -3.0f;
    public float middleY = -2.0f;
    public float moveRatio = 0.2f;
    private GameObject[] statueList;
    private Dictionary<RoleState, RoleInfo> dic;
    public float testMassLeft;
    public float testMassRight;

	// Use this for initialization
	void Start () {
        
        massLeft = 0;
        massRight = 0;
        dic = GameObject.Find("_Init").GetComponent<GS_GameInit>().roleInfosDic;
        RightPlane = this.transform.Find("RightPlane").gameObject;
        LeftPlane = this.transform.Find("LeftPlane").gameObject;
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

        leftStatueList.Add(LeftPlane);
        leftStatueList.Add(this.transform.Find("LeftLineMask").gameObject);
        rightStatueList.Add(RightPlane);
        rightStatueList.Add(this.transform.Find("RightLineMask").gameObject);
        if(massLeft<massRight)
        {
            if (LeftPlane.transform.position.y <= maxY)
            {
                for (int i = 0; i < leftStatueList.Count; ++i)
                {
                    leftStatueList[i].transform.position += new Vector3(0, (maxY - LeftPlane.transform.position.y)  * moveRatio * Time.deltaTime, 0);
                }
            }
            if(RightPlane.transform.position.y >= minY)
            {
                for (int i = 0; i < rightStatueList.Count; ++i)
                {
                    rightStatueList[i].transform.position += new Vector3(0, (minY - RightPlane.transform.position.y) * moveRatio * Time.deltaTime, 0);
                }
            }
        }
        else if(massLeft > massRight)
        {
            if (LeftPlane.transform.position.y >= minY)
            {
                for (int i = 0; i < leftStatueList.Count; ++i)
                {
                    leftStatueList[i].transform.position += new Vector3(0, (minY - LeftPlane.transform.position.y) * moveRatio * Time.deltaTime, 0);
                }
            }
            if (RightPlane.transform.position.y <= maxY)
            {
                for (int i = 0; i < rightStatueList.Count; ++i)
                {
                    rightStatueList[i].transform.position += new Vector3(0, (maxY - RightPlane.transform.position.y) * moveRatio * Time.deltaTime, 0);
                }
            }
        }
        else
        {
            for (int i = 0; i < leftStatueList.Count; ++i)
            {
                leftStatueList[i].transform.position += new Vector3(0, (middleY - LeftPlane.transform.position.y) * moveRatio * Time.deltaTime, 0);
            }
            for (int i = 0; i < rightStatueList.Count; ++i)
            {
                rightStatueList[i].transform.position += new Vector3(0, (middleY - RightPlane.transform.position.y) * moveRatio * Time.deltaTime, 0);
            }
        }
	}

    private void getMass()
    {
        massLeft = 0;
        massRight = 0;
        leftStatueList.Clear();
        rightStatueList.Clear();
        float RightY = RightPlane.transform.position.y;
        float RightX = RightPlane.transform.position.x;
        float LeftY = LeftPlane.transform.position.y;
        float LeftX = LeftPlane.transform.position.x;
        float RightSize = RightPlane.GetComponent<BoxCollider>().size.x * 0.5f;
        float RightHeight = RightPlane.GetComponent<BoxCollider>().size.y * 0.5f;
        float LeftSize = LeftPlane.GetComponent<BoxCollider>().size.x * 0.5f;
        float LeftHeight = LeftPlane.GetComponent<BoxCollider>().size.y * 0.5f;

        for(int i = 0; i<statueList.Length; ++i)
        {
            RaycastHit hit;
            if (!Physics.Raycast(statueList[i].transform.position, Vector3.down, out hit, 20.0f, LayerMask.GetMask("Balance"))) continue;
            if(Mathf.Abs(statueList[i].transform.position.x - RightX) <= RightSize)
            {
                massRight += dic[statueList[i].GetComponent<GS_Statue>().statueState].roleMass;
            }
            else if (Mathf.Abs(statueList[i].transform.position.x - LeftX) <= LeftSize)
            {
                massLeft += dic[statueList[i].GetComponent<GS_Statue>().statueState].roleMass;
            }
        }
    }
}
