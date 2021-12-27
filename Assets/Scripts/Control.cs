using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody rigid;
    private float Gravity=25f;
    private float JumpPower = 4.5f;

    private Vector3 PlayerForward=new Vector3(0,90,0);
    private Vector3 PlayerVec;
    private Vector3 RayVec;
    public static bool isDead = false;
    public GameObject Explosion;


    private void Start()
    {
        rigid = Player.GetComponent<Rigidbody>();
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

        if (Input.GetMouseButton(0))
        {
            rigid.AddForce(Vector3.up * JumpPower);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Gravity *= -1;
            JumpPower *= -1;
        }
          
    }

    private void FixedUpdate()
    {
        
        rigid.AddForce(Vector3.down * Gravity);
      
    }

    private void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.CompareTag("Disturb"))
        {
            RayVec = (col.transform.position - Player.transform.position).normalized;
            if(RayVec.y>-0.5 && RayVec.y < 0.5)
            {
                isDead = true;                
                Player.gameObject.SetActive(false);
                Explosion.transform.position = Player.transform.position;
                Explosion.gameObject.SetActive(true);
                Debug.Log("사망 " + RayVec.y);                
            }
        }
    }
}
