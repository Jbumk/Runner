using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public GameObject Ground;
    public GameObject[] Point;
    public float Speed=3f;

    private void Update()
    {
        Ground.transform.position = Vector3.MoveTowards(Ground.transform.position,Point[1].transform.position,Speed*Time.deltaTime);

        if (Ground.transform.position == Point[Point.Length-1].transform.position)
        {
            Ground.transform.position = Point[0].transform.position;
        }
    }
}
