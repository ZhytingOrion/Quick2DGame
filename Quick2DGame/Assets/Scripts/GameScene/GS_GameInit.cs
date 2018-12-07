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
            roleInfosDic.Add(roleInfos[i].roleState, roleInfos[i]);
        }
        for (int i = 0; i < statueInfos.Count; ++i)
        {
            statueInfosDic.Add(statueInfos[i].statueState, statueInfos[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
