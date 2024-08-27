using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}