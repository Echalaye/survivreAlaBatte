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
            Moove(Vector3.left);
        else if (Input.GetKey(KeyCode.D))
<<<<<<< HEAD
            moove(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.Space))
            jump();
=======
            Moove(Vector3.right);
        
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            Jump();
>>>>>>> 181d6a0c3f1f3a95a8fa28aba2e6d6bd0585a173


    }

    public void Moove(Vector3 Direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }


    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        isJumping = true;
        StartCoroutine(canJump());
    }
    public void GetDamage(int takenDamage)
    {
        health -= takenDamage;
    }
    IEnumerator canJump()
    {
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }
}
