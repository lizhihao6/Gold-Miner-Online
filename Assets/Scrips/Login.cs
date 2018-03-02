using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public GameObject ButtonBackGround;
    public Button StartButton;
    public Text UserName;
    public InputField UserNameBackGround;

    public void Start()
    {
        StartButton.GetComponent<Button>().enabled = false;
        if (PlayerPrefs.GetString("UserName")!= "") {
            Debug.Log(PlayerPrefs.GetString("UserName"));
            ChangeButton();
        }
    }

    public void _Login() {
        PlayerPrefs.SetString("UserName", UserName.text);
        ChangeButton();
    }

    void ChangeButton()
    {
        ButtonBackGround.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button");
        ButtonBackGround.GetComponent<Image>().color = Color.white;
        StartButton.GetComponent<Button>().enabled = true;
        UserNameBackGround.GetComponent<Image>().sprite = null;
        UserNameBackGround.GetComponent<Image>().color = new Color(0,0,0,0);
        UserNameBackGround.GetComponent<InputField>().readOnly = true;
        UserNameBackGround.GetComponent<InputField>().text = PlayerPrefs.GetString("UserName");
    }

}
