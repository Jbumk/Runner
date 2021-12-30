using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCube : MonoBehaviour
{
    private Vector3 Origin = new Vector3(1, 1, 1);

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * 5f * Time.deltaTime);

        if (UIManager.instance.DeadChk())
        {
            transform.localScale = Origin;
            ObjPool.ReturnCube(this);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("End"))
        {
            transform.localScale = Origin;
            ObjPool.ReturnCube(this);
        }
       
    }
}
