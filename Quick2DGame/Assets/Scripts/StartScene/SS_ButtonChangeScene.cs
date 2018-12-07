using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SS_ButtonChangeScene : ButtonBasic {

    private void OnMouseDown()
    {
        SceneManager.LoadScene("GameScene");   //待更改
    }
}
