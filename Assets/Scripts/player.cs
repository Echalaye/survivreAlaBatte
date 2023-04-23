using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1f;
    private bool isJumping = false;
    private float saveYpos;
    private float jumpAmount = 10;
    // Update is called once per frame
    void Update()
    {

 
        if (Input.GetKey(KeyCode.A))
            moove(Vector3.left);
        else if (Input.GetKey(KeyCode.D))
            moove(Vector3.right);
        else if (Input.GetKey(KeyCode.Space))
            jump();


    }

    public void moove(Vector3 Direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }


    public void jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);

    }
}
