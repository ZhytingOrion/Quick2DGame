using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_GameInit : MonoBehaviour {

    public List<RoleInfo> roleInfos = new List<RoleInfo>();
    public List<StatueInfo> statueInfos = new List<StatueInfo>();

    [HideInInspector]
    public Dictionary<RoleState, RoleInfo> roleInfosDic = new Dictionary<RoleState, RoleInfo>();
    [HideInInspector]
    public Dictionary<RoleState, StatueInfo> statueInfosDic = new Dictionary<RoleState, StatueInfo>();

    // Use this for initialization
    void Awake () {
        Game.Instance.gameState = GameState.Play;  //如果需要加动画则修改
        

        for (int i = 0; i < roleInfos.Count; ++i)
        {
            //读取动画图片
            roleInfos[i].walkAnim = getSpriteAnim(roleInfos[i].walkAnimInfo);
            roleInfos[i].jumpAnim = getSpriteAnim(roleInfos[i].jumpAnimInfo);
            roleInfos[i].stayAnim = getSpriteAnim(roleInfos[i].stayAnimInfo);
            roleInfos[i].otherAnim = getSpriteAnim(roleInfos[i].otherAnimInfo);

            roleInfosDic.Add(roleInfos[i].roleState, roleInfos[i]);
        }
        for (int i = 0; i < statueInfos.Count; ++i)
        {
            statueInfosDic.Add(statueInfos[i].statueState, statueInfos[i]);
        }
    }

    private List<Sprite> getSpriteAnim(spriteAnimInfo info)
    {
        List<Sprite> sprites = new List<Sprite>();
        if (info.formatAssetPath == "" || info.minName > info.maxName) return sprites;
        string assetName = info.formatAssetPath;
        for(int i = info.minName; i<=info.maxName;++i)
        {
            if(i < 10)
            {
                assetName += "0000" + i.ToString();
            }
            else if(i<100)
            {
                assetName += "000" + i.ToString();
            }
            else if(i<1000)
            {
                assetName += "00" + i.ToString();
            }
            else if(i<10000)
            {
                assetName += "0" + i.ToString();
            }
            else
            {
                assetName += i.ToString();
            }
            sprites.Add(Resources.Load<Sprite>(assetName));
            assetName = info.formatAssetPath;
        }
        return sprites;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
