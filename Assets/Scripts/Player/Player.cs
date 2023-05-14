using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject buttonReload;
    public TextMeshProUGUI lifeText;

    public float speed = 1f;
    private bool isJumping = false;
    private float jumpAmount = 7;
    private int health = 100;
    private bool goLeft = false;

    private void Start()
    {
        lifeText.text = health.ToString();
    }
    void Update()
    {

        
        if (Input.GetKey(KeyCode.A))
        {
            goLeft = true;
            Moove(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            goLeft = false;
            Moove(Vector3.right);
        }
        
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
        lifeText.text = health.ToString();
        transform.position = new Vector3(transform.position.x - knockBack, transform.position.y, transform.position.z); 
    }

    public bool GetGoLeft()
    {
        return goLeft;
    }

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("FullScene");
    }

    IEnumerator CanJump()
    {
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }
}
