using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts {

    private Consts _instance;
    public Consts Instance
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
}
