using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ore : MonoBehaviour {

    int value = 0;
    bool isMove = false;

    GameObject GoldSum = null;
    GameObject Hook = null;
    Vector3 Bias= new Vector3(0,0,0);
    
    // Use this for initialization
    void Start () {
        GoldSum = GameObject.FindWithTag("GoldSum");

        switch (tag) {
            case "Gold_Plus":
                value = 300;
                break;
            case "Stone_Plus":
                value = 25;
                break;
            case "Gold":
                value = 100*(int)transform.localScale.y;
                break;
            case "Stone":
                value = 10 * (int)transform.localScale.y;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isMove) {
            this.transform.position= Hook.transform.position+Bias;
        }

        if (Hook != null && Hook.GetComponentInParent<RopeAndHook>().GameState == state.rotate)
        {
            GoldSum.GetComponent<Text>().text = (int.Parse(GoldSum.GetComponent<Text>().text) + value).ToString();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Hook")
        {
            Hook = collider.gameObject;
            Bias = this.transform.position - Hook.transform.position;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger=false;
            isMove = true;
        }
    }
}
