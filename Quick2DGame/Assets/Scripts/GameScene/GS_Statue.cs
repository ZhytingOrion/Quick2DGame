using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Statue : MonoBehaviour {
    [HideInInspector]
    public StatueInfo statueInfo;

    public RoleState statueState;

    private Vector3 pos;
    private float acc = -Consts.Instance.Gravity;
    private float V0;

    // Use this for initialization
    void Start () {
        statueInit(this.statueState);
        pos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Game.Instance.gameState != GameState.Play) return;

        //禁止旋转
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //禁止动
        Vector3 currentPos = this.transform.position;
        currentPos.x = pos.x;
        currentPos.z = pos.z;
        this.transform.position = currentPos;

        /*if (!IsGrounded())
        {
            this.transform.Translate(new Vector3(0, 0.5f * acc * Time.deltaTime,0));
        }*/
        
    }

    public void statueInit(RoleState state)
    {
        this.statueState = state;
        statueInfo = GameObject.Find("_Init").GetComponent<GS_GameInit>().statueInfosDic[statueState];
        this.transform.Find("statueSprite").GetComponent<SpriteRenderer>().sprite = statueInfo.statueTex;
        this.GetComponent<BoxCollider>().size = new Vector3(statueInfo.statueSize.x, statueInfo.statueSize.y, 0.2f);
        //this.GetComponent<Rigidbody2D>().mass = statueInfo.statueMass;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Debug.Log("找到雕塑了!");
            //让灵体激活该雕像
            collision.gameObject.GetComponent<PlayerBasic>().GetIntoStatue(this.statueState, this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGrounded())
        {
            if (this.GetComponent<Rigidbody>() != null)
            {
                Destroy(this.GetComponent<Rigidbody>());
            }
        }
    }

    private bool IsGrounded()
    {
        Vector3 position = transform.position;
        Vector3 direction = Vector3.down;
        float distance = this.GetComponent<BoxCollider>().size.y / 2;

        if (Physics.Raycast(position, direction, distance))
        {
            return true;
        }

        return false;
    }
}
