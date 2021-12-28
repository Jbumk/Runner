using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCube : MonoBehaviour
{
  
    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * 5f * Time.deltaTime);

        if (UIManager.instance.DeadChk())
        {
            ObjPool.ReturnCube(this);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("End"))
        {
            ObjPool.ReturnCube(this);
        }
       
    }
}
