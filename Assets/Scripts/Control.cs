using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody rigid;
    private float Gravity=25f;
    private float JumpPower = 34f;
    private MeshRenderer Rend;

    private Vector3 PlayerForward=new Vector3(0,90,0);
    private Vector3 PlayerVec;
    private Vector3 RayVec;
    private Vector3 OriginVec = new Vector3(-7, 2, 0);
    public GameObject Explosion;

    private float ExploTimer = 0;
    private double DuraExplo = 2.0;

 


    private void Start()
    {
        rigid = Player.GetComponent<Rigidbody>();
        Rend = Player.GetComponent<MeshRenderer>();
    }


    void Update()
    {
        PlayerForward.x = rigid.velocity.y*-3;
        PlayerForward.x = Mathf.Clamp(PlayerForward.x, -35f, 35f);
        Player.transform.rotation=Quaternion.Euler(PlayerForward);
        PlayerVec.x = -7;
        PlayerVec.z = 0;
        PlayerVec.y = Player.transform.position.y;
        Player.transform.position = PlayerVec;


        if (UIManager.instance.DeadChk())
        {
            ExploTimer += Time.deltaTime;
            if (ExploTimer >= DuraExplo)
            {
                Explosion.SetActive(false);
                ExploTimer = 0;
            }           
        }
          

    }

    private void FixedUpdate()
    {
        if (UIManager.instance.StartChk())
        {
            if (!UIManager.instance.DeadChk())
            {
                if (Input.GetMouseButton(0))
                {
                    rigid.AddForce(Vector3.up * JumpPower);
                }
              
            }

            rigid.AddForce(Vector3.down * Gravity);
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
      
    }

    private void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.CompareTag("Disturb")&&!UIManager.instance.DeadChk())
        {
            RayVec = (col.transform.position - Player.transform.position).normalized;
            if (col.gameObject.name==("Cube(Clone)"))
            {
                if (RayVec.y > -0.5 && RayVec.y < 0.5)
                {
                    UIManager.instance.Dead();                   
                    Explosion.transform.position = Player.transform.position;
                    Explosion.gameObject.SetActive(true);
                    Debug.Log("사망 " + RayVec.y);
                }
            }
            else
            {
                UIManager.instance.Dead();                
                Explosion.transform.position = Player.transform.position;
                Explosion.gameObject.SetActive(true);
                Debug.Log("사망 " + RayVec.y);
            }
        }
    }

    public void Revive()
    {
        ExploTimer = 0;      
        Player.transform.position = OriginVec;
        UIManager.instance.Revive();

    }
}
