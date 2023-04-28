using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PaddleScript : MonoBehaviour
{
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public BallScript ball;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(horizontal * speed * Time.deltaTime * Vector2.right);

        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("ExtraLife"))
        {
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("IncBallSpeed"))
        {
            ball.IncreaseSpeed(1.05f, other);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("DecBallSpeed"))
        {
            ball.DecreaseSpeed(1.05f, other);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("IncPaddleWidth"))
        {
            GrowPaddle(other);
        }
        if (other.CompareTag("DecPaddleWidth"))
        {
            ShrinkPaddle(other);
        }
        if (other.CompareTag("IncPaddleSpeed"))
        {
            IncreaseSpeed(1, other);
        }
        if (other.CompareTag("DecPaddleSpeed"))
        {
            DecreaseSpeed(1, other);
        }
    }

    public void GrowPaddle(Collider2D other)
    {
        Transform paddle = transform.GetComponent<Transform>();
        Vector2 incWidth = new(paddle.localScale.x + 2, paddle.localScale.y);
        paddle.localScale = incWidth;
        rightScreenEdge -= 0.14f;
        leftScreenEdge += 0.14f;
        Destroy(other.gameObject);
    }

    public void ShrinkPaddle(Collider2D other)
    {
        Transform paddle = transform.GetComponent<Transform>();
        Vector2 decWidth = new(paddle.localScale.x - 2, paddle.localScale.y);
        paddle.localScale = decWidth;
        rightScreenEdge += 0.14f;
        leftScreenEdge -= 0.14f;
        Destroy(other.gameObject);
    }

    public void IncreaseSpeed(float speedAdjustment, Collider2D other)
    {
        if (speed < 25)
        {
            speed += speedAdjustment;
            if (speed > 25)
            {
                speed = 25;
            }
        }
        Destroy(other.gameObject);
    }

    public void DecreaseSpeed(float speedAdjustment, Collider2D other)
    {
        if (speed > 5)
        {
            speed -= speedAdjustment;
            if (speed < 5)
            {
                speed = 5;
            }
        }
        Destroy(other.gameObject);
    }
}
