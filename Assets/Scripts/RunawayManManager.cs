using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RunawayManManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rigidbody2D;
    float speed;
    float jumpPower = 400;

    float time = 0;
    public static float score;
    public static bool isClear = false;

    public Text scoreText;

    void Start()
    {
        isClear = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float x = Input.GetAxis("Horizontal");

        if(x == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0)
        {
            direction = DIRECTION_TYPE.LEFT;
        }

        //float y = Input.GetAxis("Vertical");
        if (IsGround() && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if(rigidbody2D.position.x <= -8.9f)
        {
            Debug.Log("壁に当たる");
            transform.position = new Vector2(-8.9f, transform.position.y);
        }
        else if(rigidbody2D.position.x >= 8.9f)
        {
            Debug.Log("壁に当たる");
            transform.position = new Vector2(8.9f, transform.position.y);
        }
        score = (int)(8 * time);
        scoreText.text = score.ToString();
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 4;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -8;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }

        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);

    }
    void Jump()
    {
        Debug.Log(time);
        SoundManager.instance.PlaySE(0);
        rigidbody2D.AddForce(Vector2.up * jumpPower);
    }
    bool IsGround()
    {
        Vector3 leftStartPoint = transform.position - Vector3.right * 0.3f;
        Vector3 rightStartPoint = transform.position + Vector3.right * 0.3f;
        Vector3 endPoint = transform.position - Vector3.up * 0.1f;
        Debug.DrawLine(leftStartPoint, endPoint);
        Debug.DrawLine(rightStartPoint, endPoint);

        return Physics2D.Linecast(leftStartPoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartPoint, endPoint, blockLayer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SoundManager.instance.PlaySE(1);
            SceneManager.LoadScene("GameOver");
            score = (int)(8 * time);
            isClear = true;
        }
    }
}
