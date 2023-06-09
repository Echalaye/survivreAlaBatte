using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PiafController : MonoBehaviour
{
    public Rigidbody2D rb;
    private int damage = 10;
    private bool hasAttack = false;
    private float posY;
    public float speed = 1.5f;
    private int randNumber;
    private bool goLeft = false;
    private float lerpSpeed;
    private float health = 50;
    private float knockback = 0.5f;
    private bool canMoove = true;

    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;
        GetPlaceToMoove();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoove)
        {
            if (!hasAttack)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector3.down, 1000000f);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Zombie") || hit.collider.CompareTag("Skeleton"))
                    {
                        transform.position = new Vector3(transform.position.x, hit.transform.position.y, transform.position.z);
                    
                    }
                }
            }

            if(goLeft) {

                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, posY, transform.position.z), 1 * Time.deltaTime);
        }
    }

    public void GetPlaceToMoove()
    {
        randNumber = Random.Range(0, 2);
        switch(randNumber)
        {
            case 0:
                goLeft = true;
                break;
            case 1:
                goLeft= false;
                break;
        }
        StartCoroutine(ChoosNewPlace());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(transform.position.x > collision.gameObject.transform.position.x)
            {
                knockback = -knockback;
            }
            collision.gameObject.GetComponent<Player>().GetDamage(damage, knockback);
            knockback = 0.5f;
        }else if (collision.gameObject.CompareTag("Zombie"))
        {
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                knockback = -knockback;
            }
            collision.gameObject.GetComponent<ZombieController>().GetDamage(damage, 0.5f);
            knockback = 0.5f;
        }
        else if (collision.gameObject.CompareTag("Skeleton")){
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                knockback = -knockback;
            }
            collision.gameObject.GetComponent<SkeletonController>().GetDamage(damage, 0.5f);
            knockback = 0.5f;
        }
        hasAttack = true;
        StartCoroutine(SetHasAttack());
    }

    public void GetDamage(int takenDamage, float knockBack)
    {
        health -= takenDamage;
        if (health <= 0)
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
    public bool GetCanMoove()
    {
        return canMoove;
    }

    public void GigaBatHitMe(float velX, float velY)
    {
        rb.velocity = new Vector2(velX, velY);
        StartCoroutine(KillingByTheBat());
    }
    IEnumerator KillingByTheBat()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator canMooveAgain()
    {
        yield return new WaitForSeconds(2f);
        canMoove = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    IEnumerator SetHasAttack()
    {
        yield return new WaitForSeconds(2f);
        hasAttack = false;
    }

    IEnumerator ChoosNewPlace()
    {
        yield return new WaitForSeconds(10f);
        GetPlaceToMoove();
    }
}
