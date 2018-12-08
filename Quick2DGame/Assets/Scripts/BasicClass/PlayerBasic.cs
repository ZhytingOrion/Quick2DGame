using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoleState
{
    Mouse,
    Robbit,
    Dog,
    Soul,
}

public class PlayerBasic : MonoBehaviour {

    //private Vector3 position;
    private float width;
    private float height;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode jumpKey = KeyCode.LeftShift;
    public KeyCode changeIntoSoul = KeyCode.Y;
    public RoleState roleState;

    [HideInInspector]
    public RoleInfo roleInfo = new RoleInfo();

    private bool isJump = false;
    private bool isLeft;
    private int jumpTime;
    private GameObject gameInit;
    private Dictionary<RoleState, RoleInfo> roleInfos = new Dictionary<RoleState, RoleInfo>();
    private GameObject body;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        //position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Use this for initialization
    void Start () {
        jumpTime = roleInfo.jumpTime;
        //this.GetComponent<BoxCollider2D>().size = roleInfo.roleSize;
        this.GetComponent<BoxCollider>().size = roleInfo.roleSize;
        gameInit = GameObject.Find("_Init");
        roleInfos = gameInit.GetComponent<GS_GameInit>().roleInfosDic;

        this.roleInfo = roleInfos[this.roleState];
        this.isLeft = this.roleInfo.isLeft;
        body = this.transform.Find("body").gameObject;
        body.GetComponent<SpriteRenderer>().sprite = this.roleInfo.roleTex;
        deleteAllCollider();
	}
	
	// Update is called once per frame
	void Update () {
        if (Game.Instance.gameState != GameState.Play) return;

        if (this.GetComponent<Collider>()== null)
        {
            addColliderComponent();
        }

        //禁止旋转
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //键盘
        /*if(Input.GetKeyDown(KeyCode.W))
        {
            this.transform.position += new Vector3(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.position += new Vector3(0, 1, 0);
        }*/
        if (Input.GetKey(leftKey))
        {
            this.transform.position += new Vector3(-1, 0, 0) * roleInfo.moveSpeedX * Time.deltaTime;
            switch(this.roleState)
            {
                case RoleState.Robbit:
                    if (!this.isJump)
                    {
                        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, roleInfo.jumpForce, 0));
                        this.isJump = true;
                    }
                    break;
                default:
                    break;
            }
            if (!this.isLeft)
            {
                this.body.transform.Rotate(new Vector3(0, 1, 0), 180);
                this.isLeft = true;
            }
        }
        if (Input.GetKey(rightKey))
        {
            this.transform.position += new Vector3(1, 0, 0) * roleInfo.moveSpeedX * Time.deltaTime;
            switch (this.roleState)
            {
                case RoleState.Robbit:
                    if (!this.isJump)
                    {
                        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, roleInfo.jumpForce, 0));
                        this.isJump = true;
                    }
                    break;
                default:
                    break;
            }
            if (this.isLeft)
            {
                this.body.transform.Rotate(new Vector3(0, 1, 0), 180);
                this.isLeft = false;
            }
                    
        }
        if (Input.GetKey(upKey))
        {
            switch(this.roleInfo.roleState)
            {
                case RoleState.Soul:
                    this.transform.position += new Vector3(0, 1, 0) * roleInfo.moveSpeedY * Time.deltaTime;
                    break;
                default:
                    break;
            }            
        }
        if (Input.GetKey(downKey))
        {
            switch (this.roleInfo.roleState)
            {
                case RoleState.Soul:
                    this.transform.position += new Vector3(0, -1, 0) * roleInfo.moveSpeedY * Time.deltaTime;
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(jumpKey) && !isJump)
        {
            switch (this.roleInfo.roleState)
            {
                case RoleState.Soul:
                    break;
                default:
                    //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, roleInfo.jumpForce));
                    this.GetComponent<Rigidbody>().AddForce(new Vector3(0, roleInfo.jumpForce,0));
                    jumpTime -= 1;
                    if (jumpTime == 0) isJump = true;
                    break;
            }
        }

        if(Input.GetKeyDown(changeIntoSoul) && roleInfo.roleState !=RoleState.Soul)
        {
            GetIntoSoul();
        }

        /*
        //鼠标
        if(Input.GetMouseButtonDown(0))  //左键
        {

        }
        if(Input.GetMouseButtonDown(1))  //右键
        {

        }
        if(Input.GetMouseButtonDown(2))  //中键
        {

        }

        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                //position = new Vector3(-pos.x, pos.y, 0.0f);

                // Position the cube.
                //transform.position = position;
            }

            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began)
                {
                    // Halve the size of the cube.
                    //transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    // Restore the regular size of the cube.
                    //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }*/
    }

    public void GetIntoSoul()
    {
        //换图
        this.body.GetComponent<SpriteRenderer>().sprite = roleInfos[RoleState.Soul].roleTex;
        this.isLeft = roleInfos[RoleState.Soul].isLeft;

        //播动画

        //丢雕像
        GameObject statue = Instantiate((GameObject)Resources.Load("Prefabs/Statue"));
        statue.GetComponent<GS_Statue>().statueInit(this.roleState);
        statue.transform.position = this.transform.position ;

        //切换状态       
        GameObject player = Instantiate((GameObject)Resources.Load("Prefabs/" + this.gameObject.name));
        player.name = this.gameObject.name;
        player.transform.position = this.transform.position + new Vector3(0, this.roleInfo.roleSize.y + 0.5f, 0);
        Destroy(this.gameObject);
        //this.roleState = RoleState.Soul;
        //this.roleInfo = roleInfos[RoleState.Soul];
        //addColliderComponent();
    }

    public void GetIntoStatue(RoleState newRolestate)
    {
        this.roleState = newRolestate;

        //换图
        this.body.GetComponent<SpriteRenderer>().sprite = roleInfos[newRolestate].roleTex;

        //播动画

        //销毁雕像（可放在雕像碰撞体也可以放在这里）

        //切换状态
        this.roleInfo = roleInfos[newRolestate];
        deleteAllCollider();
    }

    private void addColliderComponent()   //根据角色添加对应碰撞体
    {
        Debug.Log("进入加刚体" + this.roleState );

        //if (this.GetComponent<Rigidbody2D>() == null)
        //{
        //    Debug.Log("角色状态：" + this.roleState);
        //    this.gameObject.AddComponent<Rigidbody2D>();
        //    Debug.Log("加上刚体" + this.roleState);
        //}
        if (this.gameObject.GetComponent<Rigidbody>() == null)
        {
            Debug.Log("角色状态：" + this.roleState);
            this.gameObject.AddComponent<Rigidbody>();
            Debug.Log("加上刚体" + this.roleState);
        }
        else
        {
            Debug.Log("有刚体" + this.roleState + Time.time);
        }

        if(this.gameObject.GetComponent<BoxCollider>() == null)
        {
            this.gameObject.AddComponent<BoxCollider>();
        }

        switch (this.roleState)
        {
            case RoleState.Mouse:
                //this.GetComponent<Rigidbody2D>().gravityScale = 1;   //可能还需要修改质量
                //this.gameObject.GetComponent<Rigidbody2D>().mass = this.roleInfo.roleMass;
                //this.gameObject.AddComponent<BoxCollider2D>();
                //this.GetComponent<BoxCollider2D>().size = this.roleInfo.roleSize;
                this.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().mass = this.roleInfo.roleMass;
                this.GetComponent<BoxCollider>().size = new Vector3(this.roleInfo.roleSize.x, this.roleInfo.roleSize.y, 0.1f);
                break;
            case RoleState.Robbit:
                //this.GetComponent<Rigidbody2D>().gravityScale = 1;
                //this.gameObject.GetComponent<Rigidbody2D>().mass = this.roleInfo.roleMass;
                //this.gameObject.AddComponent<BoxCollider2D>();
                //this.GetComponent<BoxCollider2D>().size = this.roleInfo.roleSize;
                this.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().mass = this.roleInfo.roleMass;
                this.GetComponent<BoxCollider>().size = new Vector3(this.roleInfo.roleSize.x, this.roleInfo.roleSize.y, 0.1f);
                break;
            case RoleState.Dog:
                //this.GetComponent<Rigidbody2D>().gravityScale = 1;
                //this.gameObject.GetComponent<Rigidbody2D>().mass = this.roleInfo.roleMass;
                //this.gameObject.AddComponent<BoxCollider2D>();
                //this.GetComponent<BoxCollider2D>().size = this.roleInfo.roleSize;
                this.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().mass = this.roleInfo.roleMass;
                this.GetComponent<BoxCollider>().size = new Vector3(this.roleInfo.roleSize.x, this.roleInfo.roleSize.y, 0.1f);
                break;
            case RoleState.Soul:
                this.GetComponent<Rigidbody>().useGravity = false;
                this.gameObject.GetComponent<Rigidbody>().mass = this.roleInfo.roleMass;
                this.GetComponent<BoxCollider>().isTrigger = true;
                //Destroy(this.GetComponent<Rigidbody2D>());
                //this.GetComponent<Rigidbody2D>().gravityScale = 0;
                //this.gameObject.GetComponent<Rigidbody2D>().mass = this.roleInfo.roleMass;
                //this.gameObject.AddComponent<BoxCollider2D>();
                //this.GetComponent<BoxCollider2D>().isTrigger = true;
                break;
        }
    }

    private void deleteAllCollider()
    {
        //if (this.GetComponent<Rigidbody2D>() != null) Destroy(this.GetComponent<Rigidbody2D>());
        //Component[] colliders = this.GetComponents<Collider2D>();
        //for(int i = 0; i<colliders.Length; ++i)
        //{
        //    Destroy(colliders[i]);
        //}
        //if (this.GetComponent<Rigidbody>() != null) Destroy(this.GetComponent<Rigidbody>());
        Component[] colliders = this.GetComponents<Collider>();
        for(int i = 0; i<colliders.Length; ++i)
        {
            Destroy(colliders[i]);
        }
        Debug.Log("销毁刚体：" + this.roleState + Time.time);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("进入碰撞体!");
    //    //if(collision.gameObject.tag == "floor")
    //    {
    //        isJump = false;
    //        jumpTime = this.roleInfo.jumpTime;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("进入碰撞体!");
        //if(collision.gameObject.tag == "floor")
        {
            isJump = false;
            jumpTime = this.roleInfo.jumpTime;
        }
    }
}


