using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitScreen : MonoBehaviour
{

    private float standard_width = 1920.0f;
    private float standard_height = 1080.0f;
    private float device_width;
    private float device_height;
    private float fitScale;

    // Use this for initialization
    void Start()
    {
        device_width = Screen.width;
        device_height = Screen.height;
        fitScale = device_height / standard_height;

        foreach (Transform child in this.transform)
        {
            Vector2 anchorPoint = child.GetComponent<RectTransform>().anchoredPosition;
            child.GetComponent<RectTransform>().anchoredPosition = fitScale * anchorPoint;
            child.GetComponent<RectTransform>().localScale = new Vector2(fitScale, fitScale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
