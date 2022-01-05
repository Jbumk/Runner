using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    //방해 오브젝트들
    public GameObject Pre_Cube;
    Queue<ObjCube> CubePool = new Queue<ObjCube>();

    public GameObject Pre_Cross;
    Queue<ObjCross> CrossPool = new Queue<ObjCross>();

    public GameObject Pre_Gravity;
    Queue<ObjGravity> GravityPool = new Queue<ObjGravity>();

    private void Awake()
    {
        instance = this;
        Initialize(10);
    }

    private void Initialize(int count)
    {
        for(int i=0; i < count; i++)
        {
            CubePool.Enqueue(CreateNewCube());
            CrossPool.Enqueue(CreateNewCross());
            GravityPool.Enqueue(CreateNewGravity());
        }
    }

    //큐브 생성 구간ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    private ObjCube CreateNewCube()
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
            var newObj = instance.CreateNewCube();
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

    //큐브 생성 구간 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    

    //Cross 생성 구간 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    private ObjCross CreateNewCross()
    {
        var newObj = Instantiate(Pre_Cross).GetComponent<ObjCross>();
        newObj.transform.SetParent(transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public static ObjCross GetCross()
    {
        if (instance.CrossPool.Count > 0)
        {
            var Obj = instance.CrossPool.Dequeue();
            Obj.transform.SetParent(null);
            Obj.gameObject.SetActive(true);

            return Obj;
        }
        else
        {
            var Obj = instance.CreateNewCross();
            Obj.transform.SetParent(null);
            Obj.gameObject.SetActive(true);

            return Obj;
        }
    }

    public static void ReturnCross(ObjCross obj)
    {
        obj.transform.SetParent(instance.transform);
        obj.gameObject.SetActive(false);
        instance.CrossPool.Enqueue(obj);
    }
 
    //Cross 생성 구간 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    //GraviryGate 생성 구간 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    private ObjGravity CreateNewGravity()
    {
        var newObj = Instantiate(Pre_Gravity).GetComponent<ObjGravity>();
        newObj.transform.SetParent(transform);
        newObj.gameObject.SetActive(false);

        return newObj;
    }

    public static ObjGravity GetGravity()
    {
        if (instance.GravityPool.Count > 0)
        {
            var obj = instance.GravityPool.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            var newObj = instance.CreateNewGravity();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);

            return newObj;
        }
    }

    public static void ReturnGravity(ObjGravity obj)
    {
        obj.transform.SetParent(instance.transform);
        obj.gameObject.SetActive(false);
        instance.GravityPool.Enqueue(obj);
    }

}
