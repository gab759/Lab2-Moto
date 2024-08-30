using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 3f;
    public float direction;
    public LayerMask layerMask;
    public bool suelo;
    public bool doubleJump;
    public float jumpForce = 4.5f;
    private bool jumpVerificacion; 
    Rigidbody2D _compRigidbody2D;
    public int playerColor;
    public int life = 10;
    public Slider barLife;
    public GameObject player;
    private bool canChangeColor = true;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _compRigidbody2D.velocity = new Vector2(direction * speed, _compRigidbody2D.velocity.y);

        Check();

        if (jumpVerificacion)
        {
            Jump();
            jumpVerificacion = false; 
        }
    }

    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpVerificacion = true;
        }
    }
    public void DecreaseLife(int amount)
    {
        life -= amount;
        if (life <= 0)
        {
            SceneManager.LoadScene("Loss");
        }
    }
    private void Jump()
    {
        if (suelo || doubleJump)
        {
            if (!suelo)
            {
                doubleJump = false; 
            }
            _compRigidbody2D.velocity = new Vector2(_compRigidbody2D.velocity.x, jumpForce);
        }
    }

    private void Check()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask))
        {
            suelo = true;
            doubleJump = true;
        }
        else
        {
            suelo = false;
        }
    }
    public void Red()
    {
        if (canChangeColor)
        {
            player.GetComponent<SpriteRenderer>().color = Color.red;
            playerColor = 1;
        }
    }

    public void Blue()
    {
        if (canChangeColor)
        {
            player.GetComponent<SpriteRenderer>().color = Color.blue;
            playerColor = 2;
        }
    }

    public void Yellow()
    {
        if (canChangeColor)
        {
            player.GetComponent<SpriteRenderer>().color = Color.yellow;
            playerColor = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            SceneManager.LoadScene("Level2");
        }
        if (collision.CompareTag("Win"))
        {
            SceneManager.LoadScene("Win");
        }
        switch (collision.tag)
        {
            case "Red":
                if (playerColor != 1)
                {
                    DecreaseLife(2);
                    Debug.Log("vida restada: " + life);
                    barLife.value = life;
                }
                break;

            case "Blue":
                if (playerColor != 2)
                {
                    DecreaseLife(2);
                    Debug.Log("vida restada: " + life);
                    barLife.value = life;
                }
                break;

            case "Yellow":
                if (playerColor != 3)
                {
                    DecreaseLife(2);
                    Debug.Log("vida restada: " + life);
                    barLife.value = life;
                }
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Red") || collision.CompareTag("Blue") || collision.CompareTag("Yellow"))
        {
            canChangeColor = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Red") || collision.CompareTag("Blue") || collision.CompareTag("Yellow"))
        {
            canChangeColor = true;
        }
    }
}