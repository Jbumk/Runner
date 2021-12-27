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
    private float Timer = 2;

    public GameObject Start;

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= CoolTime)
        {
           var NewObj = ObjPool.GetCube();
            NewObj.transform.position = Start.transform.position;
            NewObj.transform.position += Vector3.up * Random.Range(0, 8.03f);
            Timer = 0;
        }
    }


}
