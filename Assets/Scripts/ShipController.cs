using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private int randNumber;
    private bool goLeft = false;
    public float speed = 1.5f;
    private int health = 30;
    private bool canMoove = true;
    private void Start()
    {
        GetPlaceToMoove();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoove)
        {
            if (goLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
            }
        }
    }
    public void GetPlaceToMoove()
    {
        randNumber = Random.Range(0, 2);
        switch (randNumber)
        {
            case 0:
                goLeft = true;
                break;
            case 1:
                goLeft = false;
                break;
        }
        StartCoroutine(ChoosNewPlace());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
        }

    }
    public void GetDamage(int takenDamage, float knockBack)
    {
        health -= takenDamage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - knockBack, transform.position.y, transform.position.z);
        }
    }

    public void SetCanMoove(bool value)
    {
        canMoove = value;
        StartCoroutine(canMooveAgain());
    }

    IEnumerator canMooveAgain()
    {
        yield return new WaitForSeconds(2f);
        canMoove = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator ChoosNewPlace()
    {
        yield return new WaitForSeconds(10f);
        GetPlaceToMoove();
    }
}
