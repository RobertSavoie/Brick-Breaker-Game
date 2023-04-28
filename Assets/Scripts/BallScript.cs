using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public bool inPlay;
    public float speed;
    public Transform paddle;
    public Transform powerUp;
    public Rigidbody2D rb;
    public GameManager gm;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
        }

        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    /// <summary>
    /// Called when ball leaves bottom of play area
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    /// <summary>
    /// Called when the ball hits a brick
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Brick"))
        {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }
            else
            {
                int rand = Random.Range(1, 101);
                if(rand < 10)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(brickScript.explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);

                gm.UpdateScore(brickScript.points);
                gm.UpdateNumberOfBricks();

                Destroy(other.gameObject);
            }
            audioSource.Play();

        }
    }
}
