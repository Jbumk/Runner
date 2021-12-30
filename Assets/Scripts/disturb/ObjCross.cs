using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCross : MonoBehaviour
{
    private Vector3 Dir = new Vector3(-1, 0, 0);
    private Vector3 Origin = new Vector3(1, 1, 1);
    private float Speed = 40f;
    private void FixedUpdate()
    {
        transform.position+=(Dir * 5f * Time.deltaTime);
        transform.Rotate(Vector3.forward * Speed * Time.deltaTime,Space.Self);

        if (UIManager.instance.DeadChk())
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
}
