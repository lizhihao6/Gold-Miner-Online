using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    public Transform HookPosition;
    GameObject Parent = null;


    // Use this for initialization
    void Start () {

        Parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = HookPosition.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string _tag = collision.tag;
        if (_tag == "Wall")
        {
            Parent.GetComponent<RopeAndHook>().GameState = state.recover;
        }
        else if (_tag == "Stone" || _tag == "Gold")
        {
            Parent.GetComponent<RopeAndHook>().speed = Variable.speedDeafult/ collision.gameObject.transform.localScale.y;
            Debug.Log(Parent.GetComponent<RopeAndHook>().speed);
            Debug.Log(collision.gameObject.transform.localScale.y);
            Parent.GetComponent<RopeAndHook>().GameState = state.recover;
        }
        else if (_tag == "Stone_Plus" || _tag == "Gold_Plus")
        {
            Parent.GetComponent<RopeAndHook>().speed = Variable.speedDeafult /4 ;
            Parent.GetComponent<RopeAndHook>().GameState = state.recover;
        }
    }
}