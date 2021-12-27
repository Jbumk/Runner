using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    //방해 오브젝트들
    public GameObject Pre_Cube;
    Queue<ObjCube> CubePool = new Queue<ObjCube>();

    private void Awake()
    {
        instance = this;
        Initialize(10);
    }

    private void Initialize(int count)
    {
        for(int i=0; i < count; i++)
        {
            CubePool.Enqueue(CreateNewObj());
        }
    }
    
    private ObjCube CreateNewObj()
    {
        var NewObj = Instantiate(Pre_Cube).GetComponent<ObjCube>();
        NewObj.transform.SetParent(transform);
        NewObj.gameObject.SetActive(false);

        return NewObj;
    }

    public static ObjCube GetCube()
    {
        if (instance.CubePool.Count > 0)
        {
            var obj = instance.CubePool.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewObj();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnCube(ObjCube obj)
    {
        obj.transform.SetParent(instance.transform);
        obj.gameObject.SetActive(false);
        instance.CubePool.Enqueue(obj);
    }
    
 

}
