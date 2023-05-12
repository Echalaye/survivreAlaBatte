using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject buttonReload;

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
            Moove(Vector3.right);
        
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            Jump();


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
        }

    }
    public void Moove(Vector3 Direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }


    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        isJumping = true;
        StartCoroutine(CanJump());
    }
    public void GetDamage(int takenDamage, float knockBack)
    {
        health -= takenDamage;
        if (health <= 0)
        {
            buttonReload.SetActive(true);
            Time.timeScale = 0;
        }

        transform.position = new Vector3(transform.position.x - knockBack, transform.position.y, transform.position.z); 
    }

    public void Reload()
    {
        SceneManager.LoadScene("FullScene");
    }

    IEnumerator CanJump()
    {
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }
}
