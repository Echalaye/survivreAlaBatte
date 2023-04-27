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
    private int health = 100;
    // Update is called once per frame
    void Update()
    {

 
        if (Input.GetKey(KeyCode.A))
            moove(Vector3.left);
        else if (Input.GetKey(KeyCode.D))
            moove(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.Space))
            jump();


    }

    public void Moove(Vector3 Direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }


    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);

    }
    public void GetDamage(int takenDamage)
    {
        health -= takenDamage;
    }
}
