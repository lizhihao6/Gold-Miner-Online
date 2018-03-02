using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningArea : MonoBehaviour {
    public int[] OreSum;//Rock,Rock_Plus,Gold,Gold_Plus
    public GameObject RockPerfab;
    public GameObject RockPlusPerfab;
    public GameObject GoldPerfab;
    public GameObject GoldPlusPerfab;
    public GameObject MineArea;
    // Use this for initialization

    private void OreSumInit() {
        OreSum = new int[4];
        for (int i = 0; i < 4; i++)
        {
            OreSum[i] = 2;
        }
    }
    private void Awake()
    {
        List<GameObject> OrePerfab = new List<GameObject>();
        OrePerfab.Add(RockPerfab);
        OrePerfab.Add(RockPlusPerfab);
        OrePerfab.Add(GoldPerfab);
        OrePerfab.Add(GoldPlusPerfab);

        //删去init
        OreSumInit();
     
        for (int index = 0; index < 4; index++)
        {
            for (int sum = 0; sum < OreSum[index]; sum++)
            {
                Vector3 pos;
                while (!CheckSpace(OrePerfab[index], pos = new Vector3(Random.Range(-860f, 860f), Random.Range(-300f, 300f), 0)))
                { }
                GameObject OreObject = Instantiate(OrePerfab[index]);
                OreObject.transform.SetParent(MineArea.transform);
                OreObject.transform.localPosition = pos;
                //OreObject.transform.localPosition = new Vector3(OreObject.transform.localPosition.x, OreObject.transform.localPosition.y,-10);
                //生成普通的时候随机放大1-X倍
                if (index == 0 ||index== 2) {
                    float k = Random.Range(1.0f, 1.6f);
                    OreObject.transform.localScale = new Vector3(k, k, k);
                }
                //生成Plus的时候大小不变
                else if(index == 1|| index == 3){
                    OreObject.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    bool CheckSpace(GameObject gameObject, Vector3 pos3)
    {
        Vector2 pos = pos3;
        RectTransform rt = (RectTransform)gameObject.transform;
        float radii = rt.rect.width / 2;

        //隔30取样一次，取样向量分割成三个取样点
        for (int angle = 0; angle < 360; angle += 30)
        {
            for (int multiple = 1; multiple < 4; multiple++)
            {
                float x = (pos.x + radii * Mathf.Cos(angle)) * multiple / 3;
                float y = (pos.y + radii * Mathf.Sin(angle)) * multiple / 3;
                Vector2 checkPos = new Vector2(x, y);
                RaycastHit2D hit = Physics2D.Linecast(transform.TransformPoint(checkPos), transform.TransformPoint(checkPos), 1 << LayerMask.NameToLayer("Ore"));
                if (hit.collider != null) {
                    return false;
                }
            }
        }
        return true;
    }
}