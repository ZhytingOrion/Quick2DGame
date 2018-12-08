using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoleInfo
{
    public RoleState roleState;
    public Sprite roleTex;
    public Vector2 roleSize;
    public float moveSpeedX;
    public float moveSpeedY;
    public float jumpForce;
    public int jumpTime;
    public float roleMass;
    public bool isLeft;
    public Animation runAnim;
    public Animation jumpAnim;
    public Animation hurtAnim;
}

[System.Serializable]
public class StatueInfo
{
    public RoleState statueState;
    public Sprite statueTex;
    public Vector2 statueSize;
    public float statueMass;
}

[System.Serializable]
public enum AnimState
{
    Stop,
    Walk,
    Stay,
    Jump,
    Other,
}

public class Datas{

}
