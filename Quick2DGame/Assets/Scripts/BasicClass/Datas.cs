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
    public spriteAnimInfo walkAnimInfo;
    public spriteAnimInfo jumpAnimInfo;
    public spriteAnimInfo stayAnimInfo;
    public spriteAnimInfo otherAnimInfo;
    public List<Sprite> walkAnim = new List<Sprite>();
    public List<Sprite> jumpAnim = new List<Sprite>();
    public List<Sprite> stayAnim = new List<Sprite>();
    public List<Sprite> otherAnim = new List<Sprite>();
}

[System.Serializable]
public class spriteAnimInfo
{
    public string formatAssetPath;
    public int minName;
    public int maxName;
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
