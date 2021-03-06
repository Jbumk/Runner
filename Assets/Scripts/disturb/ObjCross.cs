using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCross : MonoBehaviour
{
    private Vector3 Dir = new Vector3(-1, 0, 0);
    private Vector3 Origin = new Vector3(1, 1, 1);
    private float Speed = 40f;
    private int SpinCode = 0;
    private void FixedUpdate()
    {
        transform.position+=(Dir * 5f * Time.deltaTime);
        if (SpinCode == 0)
        {
            transform.Rotate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(Vector3.back * Speed * Time.deltaTime, Space.Self);
        }

        if (UIManager.instance.DeadChk()||UIManager.instance.ResetChk())
        {
            transform.localScale = Origin;
            ObjPool.ReturnCross(this);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("End"))
        {
            transform.localScale = Origin;
            ObjPool.ReturnCross(this);
        }

    }
    public void setSpin(int code)
    {
        SpinCode = code;
    }
}
