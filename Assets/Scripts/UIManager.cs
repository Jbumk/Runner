using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = FindObjectOfType<UIManager>();
            }
            return m_inst;
        }
    }

    private static UIManager m_inst;

    public GameObject DeadScene;

    private float Timer = 0;
    private double CoolTime = 2.0;

    private void Update()
    {
        if (Control.isDead)
        {
            Timer += Time.deltaTime;
            if (Timer >= CoolTime)
            {
                DeadScene.SetActive(true);
            }
        }
    }
}
