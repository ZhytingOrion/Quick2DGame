using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_SpriteAnim : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer image;
    [SerializeField]
    private List<Sprite> animWalkSprites = new List<Sprite>();
    private List<Sprite> animStaySprites = new List<Sprite>();
    private List<Sprite> animJumpSprites = new List<Sprite>();
    private AnimState animState = AnimState.Stay;
    private AnimState nextState = AnimState.Stop;
    private bool nextIsBack;
    private bool nextIsFlip;
    private bool nextIsLoop;
    private List<Sprite> animNextSprites;
    private Coroutine nowAnimCoroutine;
    
    private void Start()
    {
        PlayAnimation(AnimState.Stay, false, false, true);
    }

    private List<Sprite> getListSprite(AnimState state)
    {
        switch (state)
        {
            case AnimState.Stay:
                return animStaySprites;
            case AnimState.Walk:
                return animWalkSprites;
            case AnimState.Jump:
                return animJumpSprites;
            default:
                return null;
        }
    }

    public void PlayAnimation(AnimState state, bool isBack, bool isFlip, bool isLoop)
    {
        if (image == null) image = this.GetComponent<SpriteRenderer>();
        if (nowAnimCoroutine != null)
        {           
            animNextSprites = getListSprite(state);
            if(animNextSprites!=null && animNextSprites.Count > 0)
            {
                nextIsBack = isBack;
                nextIsFlip = isFlip;
                nextIsLoop = isLoop;
                nextState = state;
            }
        }
        else
        {
            nowAnimCoroutine = StartCoroutine(PlayAnimationForwardIEnum(getListSprite(state), isBack, isFlip, isLoop));
            animState = state;
        }
    }

    private IEnumerator PlayAnimationForwardIEnum(List<Sprite> animationSprites, bool isBack, bool isFlip, bool isLoop)
    {
        int index = 0;//可以用来控制起始播放的动画帧索引
        if (animationSprites != null) {
            if (isBack) index = animationSprites.Count - 1;
            gameObject.SetActive(true);
            while (true)
            {
                //当我们需要在整个动画播放完之后  重复播放后面的部分 就可以展现我们纯代码播放的自由性
                if (index > animationSprites.Count - 1)
                {
                    if (!isLoop || nextState != AnimState.Stop) break;
                    index = 0;
                }
                if (index < 0)
                {
                    if (!isLoop || nextState != AnimState.Stop) break;
                    index = animationSprites.Count - 1;
                }
                image.sprite = animationSprites[index];
                if (!isBack) index++;
                else index--;
                yield return new WaitForSeconds(0.04f);//等待间隔  控制动画播放速度
            }
            if (nextState != AnimState.Stop)
            {
                nowAnimCoroutine = StartCoroutine(PlayAnimationForwardIEnum(getListSprite(nextState), isBack, isFlip, isLoop));
            }
        }
        else
        {
            animState = AnimState.Stop;
            nowAnimCoroutine = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
