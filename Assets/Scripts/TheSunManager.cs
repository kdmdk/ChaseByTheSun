using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSunManager : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    
    //public Transform[] Markers;

    // スピード
    public float speed = 1.0F;

    public Route[] route;
    private int pointIndex;
    private int routeIndex;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        transform.position = route[routeIndex].points[0].transform.position;
    }
    
    void Update()
    {
        speed += Time.deltaTime / 100;
        var v = route[routeIndex].points[pointIndex + 1].transform.position - route[routeIndex].points[pointIndex].transform.position;
        transform.position += v.normalized * speed * Time.deltaTime;

        var pv = transform.position - route[routeIndex].points[pointIndex].transform.position;
        if (pv.magnitude >= v.magnitude)
        {
            pointIndex++;
            if (pointIndex >= route[routeIndex].points.Length - 1)//最後まで到達した
            {
                pointIndex = 0;
                routeIndex++;
                if (routeIndex == route.Length)
                {
                    routeIndex = 0;
                }
            }
        }
    }
}
