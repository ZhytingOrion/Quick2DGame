using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Statue : MonoBehaviour {
    [HideInInspector]
    public StatueInfo statueInfo;

    public RoleState statueState;

    private Vector3 pos;
    private float acc = -Consts.Instance.Gravity;
    private AudioSource audioSource;
    public AudioClip clipImpact;
    private float height;
    private bool isOnGround = false;
    public GameObject player = null;

    // Use this for initialization
    void Start () {
        statueInit(this.statueState);
        pos = this.transform.position;
        this.audioSource = this.GetComponent<AudioSource>();
        height = this.transform.position.y;
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
        if (IsGrounded())
        {
            if (this.GetComponent<Rigidbody>() != null)
            {
                Destroy(this.GetComponent<Rigidbody>());
            }
        }

    }

    public void statueInit(RoleState state)
    {
        this.statueState = state;
        statueInfo = GameObject.Find("_Init").GetComponent<GS_GameInit>().statueInfosDic[statueState];
        this.transform.Find("statueSprite").GetComponent<SpriteRenderer>().sprite = statueInfo.statueTex;
        this.GetComponent<BoxCollider>().size = new Vector3(statueInfo.statueSize.x, statueInfo.statueSize.y, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(IsGrounded() && Game.Instance.gameState == GameState.Play && height-this.transform.position.y >1.0f)
        {
            //播音效
            this.audioSource.clip = this.clipImpact;
            this.audioSource.Play();
        }
    }

    private bool IsGrounded()
    {
        Vector3 position = transform.position;
        Vector3 direction = Vector3.down;
        if (this.GetComponent<BoxCollider>() == null) return false;
        float distance = this.GetComponent<BoxCollider>().size.y / 2;

        if (Physics.Raycast(position, direction, distance, -1, QueryTriggerInteraction.Ignore ))
        {
	this.isOnGround = true;
            return true;
        }

        return false;
    }
}
