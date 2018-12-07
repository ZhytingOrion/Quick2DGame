using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Statue : MonoBehaviour {
    [HideInInspector]
    public StatueInfo statueInfo;

    public RoleState statueState;

    private Vector3 pos;

    // Use this for initialization
    void Start () {
        statueInit(this.statueState);
        pos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //禁止旋转
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //禁止动
        Vector3 currentPos = this.transform.position;
        currentPos.x = pos.x;
        currentPos.z = pos.z;
        this.transform.position = currentPos;
	}

    public void statueInit(RoleState state)
    {
        this.statueState = state;
        statueInfo = GameObject.Find("_Init").GetComponent<GS_GameInit>().statueInfosDic[statueState];
        this.transform.Find("statueSprite").GetComponent<SpriteRenderer>().sprite = statueInfo.statueTex;
        this.GetComponent<BoxCollider>().size = new Vector3(statueInfo.statueSize.x, statueInfo.statueSize.y, 10.0f);
        //this.GetComponent<Rigidbody2D>().mass = statueInfo.statueMass;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Debug.Log("找到雕塑了!");
            //让灵体激活该雕像
            collision.gameObject.GetComponent<PlayerBasic>().GetIntoStatue(this.statueState);

            //如果有动画：加动画不要虚（幽灵动画加幽灵身上）

            //销毁雕像
            Destroy(this.gameObject);
        }
    }

}
