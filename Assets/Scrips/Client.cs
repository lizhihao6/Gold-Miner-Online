using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Client : MonoBehaviour
{
    public GameObject LoadCavas;
    public GameObject Ball;
    public GameObject StartBackGround;
    public GameObject InputText;

    float rota = 1.6f;
    bool isBallRota = false;

    string HeartBeatMsg = "Hello";
    string otherNameMsg = "otherName";
    string HookMsg = "Hook";
    string ExitMsg = "exit";

    string SendMsg = "";
    string RecvMsg = "";

    const string ip = "127.0.0.1";
    const int port = 9999;
    IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private void Start()
    {
        LoadCavas.SetActive(false);
       
    }

    private void Update()
    {
        //Loading的皮卡丘动画
        if (isBallRota && Ball != null)
        {
            Ball.transform.Rotate(Vector3.forward, rota);
        }
        //处理收到的消息
        if (RecvMsg != ""){
            MsgHandle();
        }
        //用SendMsg保存要发送的消息
        SendMsg = GetSendMsg();
    }

    void LoadAnimate(bool Start){
        if (Start)
        {
            LoadCavas.SetActive(true);
            isBallRota = true;
            StartBackGround.GetComponent<Image>().sprite = null;
            StartBackGround.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            InputText.GetComponent<InputField>().text = "Matching....";
        }
        else {
            if (LoadCavas != null) {
                LoadCavas.SetActive(false);
                isBallRota = false;
                StartBackGround.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button");
                StartBackGround.GetComponent<Image>().color = Color.white;
                InputText.GetComponent<InputField>().text = PlayerPrefs.GetString("UserName");
            }
        }
    }

    public void Link() {

        LoadAnimate(true);

        socket.Connect(ipe);
        name = PlayerPrefs.GetString("UserName");
        byte[] nameB = Encoding.ASCII.GetBytes(name);
        socket.Send(nameB);
        string recvStr = "";
        byte[] recvBytes = new byte[1024];
        int bytes;
        bytes =socket.Receive(recvBytes, recvBytes.Length, 0);//从服务器端接受返回信息
        recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
        Debug.Log("client get message"+recvStr);
        Thread thread = new Thread(Communicate);
        thread.Start();
        
    }

    private void OnApplicationQuit()
    {
        if (socket.Connected) {
            socket.Send(Encoding.ASCII.GetBytes(ExitMsg));
            byte[] recvBytes = new byte[1024];
            int bytes;
            bytes = socket.Receive(recvBytes, recvBytes.Length, 0);
            socket.Close();
        }
    }

    private void Communicate(){
        while (socket.Connected) {          
            byte[] heartBeatStrB = Encoding.ASCII.GetBytes(SendMsg);
            socket.Send(heartBeatStrB);
            string recvStr = "";
            byte[] recvBytes = new byte[1024];
            int bytes;
            bytes = socket.Receive(recvBytes, recvBytes.Length, 0);//从服务器端接受返回信息
            recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
            RecvMsg = recvStr;
            Thread.Sleep(100);
        }
    }

    void MsgHandle()
    {
        //先重置收到的消息
        string msg = RecvMsg;
        RecvMsg = "";
        if (msg.StartsWith(otherNameMsg))
        {
            LoadAnimate(false);
            Variable.OtherName =  msg.Remove(otherNameMsg.Length);

        }
        else if (msg == HookMsg)
        {
            Variable.isOtherHookDown = true;
        }
        else if (msg == "Left")
        {
            isBallRota = false;
            Variable.isSelfLeft = true;
            SceneManager.LoadScene("Game");
        }
        else if (msg == "Right")
        {
            isBallRota = false;
            Variable.isSelfLeft = false;
            SceneManager.LoadScene("Game");
        } 
        else if (msg == "exit")
        {
            SceneManager.LoadScene("Main");
            LoadAnimate(false);
            InputText.GetComponent<InputField>().text = "Mathing Failed QAQ";

        }
    }

    string GetSendMsg() {
        string msg = HeartBeatMsg;
        if (Variable.isExit)
        {
            msg = ExitMsg;
            Variable.isExit= false;
        }
        else {
            if (Variable.isSelfHookDown) {
                msg = HookMsg;
                Variable.isSelfHookDown = false;
            }
        }
        return msg;
    }
}

