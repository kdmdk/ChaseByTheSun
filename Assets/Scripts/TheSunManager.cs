using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSunManager : MonoBehaviour
{
    // スピード
    public float speed = 1.0F;

    // 移動ルートを配列に
    public Route[] route;
    private int pointIndex;
    private int routeIndex;

    void Start()
    {
        // 最初の位置に
        transform.position = route[routeIndex].points[0].transform.position;
    }
    
    void Update()
    {
        // 徐々にエネミーのスピードアップ
        speed += Time.deltaTime / 100;

        // ルートのポイント１ーポイント０のベクトルの値
        var v = route[routeIndex].points[pointIndex + 1].transform.position - route[routeIndex].points[pointIndex].transform.position;
        // 移動の処理
        transform.position += v.normalized * speed * Time.deltaTime;
        // ポイントにたどり着いたか
        var pv = transform.position - route[routeIndex].points[pointIndex].transform.position;
        if (pv.magnitude >= v.magnitude)
        {
            pointIndex++;// 次のポイントへ
            if (pointIndex >= route[routeIndex].points.Length - 1)// 最後まで到達した
            {
                pointIndex = 0;
                routeIndex++;
                if (routeIndex == route.Length)
                {
                    routeIndex = 0;// 最初のルートへ
                }
            }
        }
    }
}
