using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts {

    private static Consts _instance;
    public static Consts Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Consts();
            }
            return _instance;
        }
    }

    public float Gravity = 9.8f;
    public float FrameSpeed = 0.083f;
}
