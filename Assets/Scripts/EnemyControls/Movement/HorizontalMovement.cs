using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MovementTypeInterface
{
    public Vector2 direction;

    private void Start()
    {
        direction = Vector2.right;
    }

    private void Update()
    {
        Move();

        if (mainController.SeePlayerDir != Vector2.zero && new Vector2(mainController.SeePlayerDir.x, 0).normalized != direction)
        {
            Vector2 flatDir = new Vector2(mainController.SeePlayerDir.x, 0).normalized;

            direction = flatDir;
            flip();
        }
    }

    public override void setDir()
    {
            if (direction == Vector2.left)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
            }
    }

    public void flip()
    {
        if (direction == Vector2.left)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            mainController.isFacingLeft = true;
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            mainController.isFacingLeft = false;
        }
    }

    public override void Move()
    {
        mainController.rb.position += direction * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            setDir();
        }
    }
}
