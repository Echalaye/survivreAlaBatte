using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public GameObject arrow;
    private int health;
    private GameObject targetPlayer;
    private Rigidbody2D rb;
    private bool canJump = true;
    private bool canMoove = true;
    private bool alreadyAtt = false;
    private bool goodPosL = false;
    private bool goodPosR = true;

    public float speed = 1.8f;
    public float attackDistance = 4.0f;
    public LayerMask groundLayer;
    public float jumpForce = 5f;
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        
        if (targetPlayer == null)
        {
            Debug.Log("Aucun GameObject avec le tag 'Player' n'a �t� trouv�.");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMoove)
        {
            if (transform.position.x > targetPlayer.transform.position.x && !goodPosL)
            {
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
            else if(transform.position.x < targetPlayer.transform.position.x && !goodPosR)
            {
                transform.Rotate(new Vector3(0,180,0), Space.Self);
                goodPosL = false;
                goodPosR = true;
            }

            if (targetPlayer != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);


                if (distanceToPlayer <= attackDistance)
                { 
                    if(!alreadyAtt)
                        Hit();
                }
                else
                {
                    float step = speed * Time.deltaTime;
                    Debug.DrawRay(transform.position, Vector3.right * 0.5f, Color.green);

                    if (canJump)
                    {
                        // D�tectez les obstacles et d�clenchez le saut si n�cessaire
                        RaycastHit2D hitR = Physics2D.Raycast(transform.position + Vector3.right, Vector3.right, 0.5f);
                        RaycastHit2D hitL = Physics2D.Raycast(transform.position + Vector3.left, Vector3.left, 0.5f);
                        if (hitR.collider != null || hitL.collider != null)
                        {
                            // Appliquez la force de saut au Rigidbody2D du squelette
                            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                            canJump = false;
                            //StartCoroutine(JumpAgain());
                        }

                        // D�tectez les trous devant le squelette
                        RaycastHit2D holeHitR = Physics2D.Raycast(transform.position + Vector3.right * 0.80f, Vector3.down * 1f, groundLayer);
                        RaycastHit2D holeHitL = Physics2D.Raycast(transform.position + Vector3.left * 0.80f, Vector3.down * 1f, groundLayer);
                        Debug.DrawRay(transform.position + Vector3.right * 0.80f, Vector3.down * 1f, Color.red);


                        if ((holeHitR.collider == null || holeHitL.collider == null))
                        {
                            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                            canJump = false;
                        }
                    }
                        // Avance vers le joueur 
                        transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);
                }
                if (!canJump)
                {
                    Invoke("IsGrounded", 5);
                }

            }
        }
    }
    public void Hit()
    {
        alreadyAtt = true;
        if (goodPosL)
        {
            GameObject spawnedArrow = Instantiate(arrow, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), transform.rotation);
            spawnedArrow.GetComponent<ArrowController>().SetValueArrow(15, goodPosL, true);
        }
        else
        {
            GameObject spawnedArrow = Instantiate(arrow, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), transform.rotation);
            spawnedArrow.GetComponent<ArrowController>().SetValueArrow(15, goodPosL, true);
        }
        
        StartCoroutine(CanAttAgain(1.5f));
    }

    void IsGrounded()
    {
        // V�rifiez si le squelette est au sol en utilisant un raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.6f, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.blue);
        canJump = hit.collider != null;
        /*return hit.collider != null;*/
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
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator CanAttAgain(float delay)
    {
        yield return new WaitForSeconds(delay);
        alreadyAtt = false;
    }

}
