using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    [Header("Player's")]
    public GameObject Player;
    private Vector3 PlayerVec = new Vector3(-7, 2, 0);
    private Renderer Rend;
    public GameObject Explo;

    [Header("Sub Menu")]
    //서브메뉴
    public GameObject DeadScene;
    public GameObject Menu;
    public GameObject Btn_Resume;
    public GameObject Menu_Set;

   [Header("Main Menu")]
    //메인메뉴
    public GameObject MainFrame;
    public GameObject Btn_Menu;
    public GameObject Menu_Main;
    public TextMeshPro Clock;
    public TextMeshPro MenuClock;
    public TextMeshPro StartClock;
    private int MenuCode = 0;

    [Header("Rank Menu")]
    //랭킹메뉴 관련
    public GameObject Menu_Rank;
    public TextMeshPro[] Rank;
    public TextMeshPro NowRank;


    //시작전 카운트
    private float StartTimer = 0; 
    private bool isStart = false;
    private bool isDead = true;
    private float Timer = 0;

    [Header("Sounds")]
    //소리관련
    public AudioSource BGM;
    public AudioSource DeadSound;
    public Slider Slider_BGM;
    public Slider Slider_Efect;

    private void Awake()
    {       
        Rend = Player.GetComponent<Renderer>();
        Slider_BGM.value =  RankManager.instance.rankData.Volum_BGM;
        Slider_Efect.value =RankManager.instance.rankData.Volum_Effect ;
    }


    private void Update()
    {
        if (StartTimer > 0)
        {
            StartTimer -= Time.deltaTime;
            StartClock.text = Mathf.Ceil(StartTimer)+"";
        }
        else
        {
            StartClock.gameObject.SetActive(false);
            isStart = true;
        }

        if (!isDead && isStart)
        {
            Timer += Time.deltaTime;
            Clock.text = string.Format("{0:N2}", Timer) + " 초";
            //Clock.text = Mathf.Floor(Timer*100)*0.01f + " Sec";
        }
        
        BGM.volume = Slider_BGM.value;
        DeadSound.volume = Slider_Efect.value;

    }

    //메뉴 관련ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
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

    public void Home()
    {
        BGM.Stop();
        DeadSound.Stop();
        Time.timeScale = 1f;
        isStart = false;
        isDead = true;
        MainFrame.SetActive(false);
        Clock.gameObject.SetActive(false);
        StartClock.gameObject.SetActive(false);
        Btn_Menu.SetActive(false);
        Menu_Rank.SetActive(false);
        Menu_Set.SetActive(false);
        Menu.SetActive(false);
        Menu_Main.SetActive(true);
        RankManager.instance.rankData.Volum_BGM = Slider_BGM.value;
        RankManager.instance.rankData.Volum_Effect = Slider_Efect.value;
        RankManager.instance.SaveRankData();
    }

    public void Setting()
    {
        if (Menu.activeSelf == true)
        {
            Menu.SetActive(false);
            MenuCode = 1;

        }
        else
        {
            Menu_Main.SetActive(false);
            MenuCode = 0;
        }
        Clock.gameObject.SetActive(false);
        Btn_Menu.SetActive(false);
        Menu_Set.SetActive(true);
       
    }
    public void Back()
    {
        Menu_Set.SetActive(false);
        switch (MenuCode)
        {
            case 0:
                Menu_Main.SetActive(true);
                break;
            case 1:
                Menu.SetActive(true);
                Clock.gameObject.SetActive(true);
                Btn_Menu.SetActive(true);
                break;
        }

        RankManager.instance.rankData.Volum_BGM = Slider_BGM.value;
        RankManager.instance.rankData.Volum_Effect = Slider_Efect.value;
        RankManager.instance.SaveRankData();


    }

    //메뉴창 눌렀을시 랭킹
    public void Menu_RankOn()
    {
        Menu_Rank.SetActive(true);
        Menu_Main.SetActive(false);
        Array.Sort(RankManager.instance.rankData.Rank);
        Array.Reverse(RankManager.instance.rankData.Rank);
        for(int i=0; i < RankManager.instance.rankData.Rank.Length; i++)
        {
            Rank[i].text = string.Format("{0:N2}", RankManager.instance.rankData.Rank[i])  + " 초";
        }
        
    }
    

    //메뉴관련 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    
    //기능관련

    public void GameStart()
    {
        Explo.SetActive(false);
        Rend.enabled = true;
        Player.transform.position = PlayerVec;
        BGM.Play();
        StartClock.gameObject.SetActive(true);
        StartTimer = 3.0f;
        Clock.text = "";
        isStart = false;
        isDead = false;
        Time.timeScale = 1f;
        Timer = 0;
        Menu_Main.SetActive(false);
        Clock.gameObject.SetActive(true);
        Btn_Menu.SetActive(true);
        MainFrame.SetActive(true);
    }


    //죽음 관련
    public void Dead()
    {        
        Rend.enabled = false;
        DeadSound.Play();
        BGM.Stop();
        Btn_Menu.SetActive(false);
        MenuClock.text = Clock.text;
        Btn_Resume.SetActive(false);
        Menu.SetActive(true);        
        isDead = true;
        Record();
       
    }
    
    public void Revive()
    {
        Explo.SetActive(false);
        Rend.enabled = true;
        Player.transform.position = PlayerVec;
        BGM.Play();
        StartClock.gameObject.SetActive(true);
        StartTimer = 3.0f;
        Clock.text = "";
        isStart = false;
        isDead = false;
        Time.timeScale = 1f;
        Timer = 0;
        Btn_Menu.SetActive(true);       
        Menu.SetActive(false);       
        DeadScene.SetActive(false);
    }

    //사망시 랭킹 기록
    public void Record()
    {
        RankManager.instance.LoadRankData();     
        Array.Sort(RankManager.instance.rankData.Rank);
        Array.Reverse(RankManager.instance.rankData.Rank);
        if (RankManager.instance.rankData.Rank[RankManager.instance.rankData.Rank.Length - 1] < Timer)
        {
            RankManager.instance.rankData.Rank[RankManager.instance.rankData.Rank.Length - 1] = Timer;
            Array.Sort(RankManager.instance.rankData.Rank);
            Array.Reverse(RankManager.instance.rankData.Rank);

            for (int i = 0; i < RankManager.instance.rankData.Rank.Length; i++)
            {
                Rank[i].text = RankManager.instance.rankData.Rank[i] + " 초";
                if (RankManager.instance.rankData.Rank[i] == Timer)
                {                  
                    NowRank.text = (i+1) + " 위";
                }
            }
        }
        else
        {
            NowRank.text = "Rank Out";
        }

        RankManager.instance.SaveRankData();
    }




    public bool DeadChk()
    {
        return isDead;
    }


    //스타트 관련
    public bool StartChk()
    {
        return isStart;
    }
}
