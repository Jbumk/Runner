using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGravity : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * 5f * Time.deltaTime);
        if (UIManager.instance.DeadChk() || UIManager.instance.ResetChk())
        {
            ObjPool.ReturnGravity(this);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("End"))
        {
            ObjPool.ReturnGravity(this);
        }
    }
}
