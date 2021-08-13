using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime * speed, 0);
        if (transform.position.x <= -20f)
        {
            transform.position = new Vector3(20f, 0);
        }
    }
}