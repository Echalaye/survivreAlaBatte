using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Rigidbody2D rb;
    private int health = 80;
    public float speed = 1f;
    private int damageAmount = 15;
    private GameObject targetPlayer;
    private float knockback = 1.5f;
    private bool knockLeft = true;
    private bool canMoove = true;
    private bool goodPosL = false;
    private bool goodPosR = true;
    private bool canAtt = true;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");

        if (targetPlayer == null)
        {
            Debug.Log("Aucun GameObject avec le tag 'Player' n'a �t� trouv�.");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(canMoove)
        {
            if (targetPlayer != null)
            {
                float step = speed * Time.deltaTime;

                if(transform.position.x < targetPlayer.transform.position.x)
                {
                    knockLeft = false;
                    if (!goodPosR)
                    {
                        transform.Rotate(new Vector3(0,180,0), Space.Self);
                        goodPosL = false;
                        goodPosR = true;
                    }
                }
                else
                {
                    knockLeft = true;
                    if (!goodPosL)
                    {
                        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                        goodPosL = true;
                        goodPosR = false;
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);
            }
        }
    }

    public void Hit()
    {
        canAtt = false;
        Player playerScript = targetPlayer.GetComponent<Player>();
        
        if (!knockLeft)
            knockback *= -1;
        playerScript.GetDamage(damageAmount, knockback);
        knockback = 1.5f;
        StartCoroutine(WaitUntilNextAtt(1f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAtt)
        {
            Hit();
        }
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
        StartCoroutine(CanMooveAgain());
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
    IEnumerator CanMooveAgain()
    {
        yield return new WaitForSeconds(2f);
        canMoove = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator WaitUntilNextAtt(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAtt= true;
    }
}

