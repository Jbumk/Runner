using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //서브메뉴
    public GameObject DeadScene;
    public GameObject Menu;
    public GameObject Btn_Resume;

    //메인메뉴
    public GameObject MainFrame;
    public GameObject Btn_Menu;
    public GameObject Menu_Main;

    public TextMeshPro Clock;
    public TextMeshPro MenuClock;
    
    

    private bool isDead = false;

    private float Timer = 0;
    private double CoolTime = 2.0;

    private void Update()
    {
        if (isDead)
        {
          
        }
        else
        {
            Timer += Time.deltaTime;
            Clock.text = string.Format("{0:N2}",Timer) + " Sec";
            //Clock.text = Mathf.Floor(Timer*100)*0.01f + " Sec";
        }

    }

    //메뉴 관련
    public void Quit()
    {
        Application.Quit();
    }
    
    public void OnMenu()
    {
        MenuClock.text = " ";
        Time.timeScale = 0f;
        Btn_Resume.SetActive(true);
        Menu.SetActive(true);        
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Btn_Resume.SetActive(false);
        Menu.SetActive(false);
    }

    public void GameStart()
    {
        Menu_Main.SetActive(false);
        Clock.gameObject.SetActive(true);
        Btn_Menu.SetActive(true);
        MainFrame.SetActive(true);
    }


    //죽음 관련
    public void Dead()
    {
        MenuClock.text = Clock.text;
        Btn_Resume.SetActive(false);
        Menu.SetActive(true);        
        isDead = true;
    }
    
    public void Revive()
    {
        Time.timeScale = 1f;
        Menu.SetActive(false);
        isDead = false;
        Timer = 0;
        DeadScene.SetActive(false);
    }
    public bool DeadChk()
    {
        return isDead;
    }


}
