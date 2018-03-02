using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum state
{
    rotate,
    elongate,
    recover
}
public class RopeAndHook : MonoBehaviour
{
    public GameObject RotatePosition;
    public GameObject Rope;
    public Text Timer;
    public Text Goal;
    public Text GoldSum;

    public float rota = 0.07f;
    public float speed = Variable.speed;
    const float RopeMin = 0.3333333f;
    public state GameState = state.rotate;

    bool isOther = false;

    void Start()
    {
        bool isHookLeft = GetComponent<IsLeft>().Left;
        isOther = (Variable.isSelfLeft && !isHookLeft) || (!Variable.isSelfLeft && isHookLeft);
    }

    // Update is called once per frame
    void Update()
    {
        bool Hook = false;
        if (isOther)
        {
            if (Variable.isOtherHookDown)
            {
                Hook = true;
                Variable.isOtherHookDown = false;
            }
        }
        else
        {
            Hook = isInput();
        }

        if (Hook && GameState == state.rotate)
        {
            Variable.isSelfHookDown = true;
            GameState = state.elongate;
        }

        if (GameState == state.elongate)
        {
            Elongate();
        }

        else if (GameState == state.recover)
        {
            Recover();
        }

        else if (GameState == state.rotate)
        {
            Rotate();
        }

    }

    void Rotate()
    {
        this.transform.RotateAround(RotatePosition.transform.position, Vector3.forward, rota);
        if (this.transform.eulerAngles.z > 60 && this.transform.eulerAngles.z < 180 || this.transform.eulerAngles.z > 180 && this.transform.eulerAngles.z < 300)
        {
            rota = -rota;
        }
    }

    void Elongate()
    {
        Rope.transform.localScale = Rope.transform.localScale + new Vector3(0, speed, 0);
    }

    void Recover()
    {
        if (Rope.transform.localScale.y > RopeMin)
        {
            Rope.transform.localScale = Rope.transform.localScale - new Vector3(0, speed, 0);
        }
        else
        {
            speed = Variable.speed;
            GameState = state.rotate;
        }
    }

    bool isInput()
    {
#if UNITY_ANDROID
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 pos = Input.touches[0].position;
            if (pos.y < Screen.height * 3 / 4)
            {
                return true;
            }
        }
#endif

#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;
            if (pos.y < Screen.height * 3 / 4)//想了想还是每次都调用Screen吧，说不定有哪个zz玩着玩着该分辨率了
            {
                return true;
            }
        }
#endif
        return false;
    }

}