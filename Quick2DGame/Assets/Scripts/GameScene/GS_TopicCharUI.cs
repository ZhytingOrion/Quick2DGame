using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_TopicCharUI : MonoBehaviour {

    public List<Sprite> topicCharLeft = new List<Sprite>();
    public List<Sprite> topicCharRight = new List<Sprite>();
    public List<Sprite> JumpLine = new List<Sprite>();
    public List<Sprite> CandyCount = new List<Sprite>();
    public Sprite P1;
    public Sprite P2;
    public Sprite CandyIcon;
    public bool isP1 = false;

    private GameObject RolePic;
    private GameObject RoleType;
    private GameObject CandyCountPic;
    private GameObject JumpLinePic;
    private RoleInfo Player;

	// Use this for initialization
	void Start () {
        RolePic = this.transform.Find("RolePic").gameObject;
        JumpLinePic = this.transform.Find("JumpLine").gameObject;
        CandyCountPic = this.transform.Find("CandyCount").gameObject;
        this.transform.Find("P1P2").GetComponent<SpriteRenderer>().sprite = isP1 ? P1 : P2;
        this.transform.Find("CandyIcon").GetComponent<SpriteRenderer>().sprite = CandyIcon;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject player = isP1 ? GameObject.Find("Player1").gameObject : GameObject.Find("Player2").gameObject;
        if (player != null) Player = player.GetComponent<PlayerBasic>().roleInfo;

        RolePic.GetComponent<SpriteRenderer>().sprite = isP1 ? topicCharLeft[(int)Player.roleState] : topicCharRight[(int)Player.roleState];
        JumpLinePic.GetComponent<SpriteRenderer>().sprite = JumpLine[(int)(Player.moveSpeedX/0.6f)];
        CandyCountPic.GetComponent<SpriteRenderer>().sprite = isP1 ? CandyCount[Game.Instance.Role1CandyCount] : CandyCount[Game.Instance.Role2CandyCount];

    }
}
