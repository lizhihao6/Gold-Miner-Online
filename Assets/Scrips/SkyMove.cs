using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMove : MonoBehaviour {
    public GameObject SkyImage;
    public 
    bool x_plus = false;
    bool y_plus = false;
    const float x_max = 400f;
    const float x_min = -x_max;
    const float y_max = 150f;
    const float y_min = -y_max;
    //Y from -150 to 150 X from -400 to 400
    //400 = 42.76  150 = 16.037
    void Start () {
        SkyImage.transform.localPosition = new Vector3(x_max, y_max, 0);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 SkyPosition = SkyImage.transform.localPosition;
        float x = SkyPosition.x;
        float y = SkyPosition.y;
        if (x >= x_max)
        {
            x_plus = false;
        }
        else if (x <= x_min)
        {
            x_plus = true;
        }
        if (y >= y_max)
        {
            y_plus = false;
        }
        else if (y <= y_min)
        {
            y_plus = true;
        }
        if (x_plus)
            x += 0.3f;
        else if (!x_plus)
            x -= 0.3f;
        if (y_plus)
            y += 0.06f;
        else if (!y_plus)
            y -= 0.06f;
        SkyImage.transform.localPosition = new Vector3(x, y, 0);
    }
}