using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public static ObjManager instance
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = FindObjectOfType<ObjManager>();
            }
            return m_inst;
        }
    }   

    private static  ObjManager m_inst;
    private double CoolTime = 2.0;
    private float Timer = 0;
    private int ObjCode;
    private Vector3 Size =new Vector3(1,1,1);
    private int RandomSize;

    public GameObject Start;

    private void Update()
    {
        if (!UIManager.instance.DeadChk()&& UIManager.instance.StartChk())
        {
            Timer += Time.deltaTime;
            if (Timer >= CoolTime)
            {
                ObjCode = Random.Range(0, 3);
                RandomSize = Random.Range(0, 3);
                Size.x = RandomSize;
                Size.y = RandomSize;
                Debug.Log(ObjCode);
                switch (ObjCode)
                {
                    case 0:
                        var NewCube = ObjPool.GetCube();
                        NewCube.transform.position = Start.transform.position;
                        NewCube.transform.localScale += Size;
                        NewCube.transform.position += Vector3.up * Random.Range(0 + RandomSize, 8.03f - RandomSize);
                        break;
                    case 1:
                        var NewCross = ObjPool.GetCross();
                        NewCross.setSpin(Random.Range(0, 2));
                        NewCross.transform.position = Start.transform.position;
                        NewCross.transform.localScale += Size;
                        NewCross.transform.position += Vector3.up * Random.Range(2.5f + RandomSize, 6.3f - RandomSize);
                        break;
                    case 2:
                        var NewGravity = ObjPool.GetGravity();
                        NewGravity.transform.position = Start.transform.position;
                        NewGravity.transform.position += Vector3.up * 3.8f;
                        break;


                }

                Timer = 0;
            }
        }
    }


}
