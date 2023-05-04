using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private int health;
    private int damageAmount = 0;
    private GameObject targetPlayer;
    private Rigidbody2D rb;
    private bool canJump = true;

    public float speed = 1.8f;
    public float attackDistance = 4.0f;
    public LayerMask groundLayer;
    public float jumpForce = 5f;
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        
        if (targetPlayer == null)
        {
            Debug.Log("Aucun GameObject avec le tag 'Player' n'a été trouvé.");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);


            if (distanceToPlayer <= attackDistance)
            { 
                Hit();
            }
            else
            {
                float step = speed * Time.deltaTime;
                Debug.DrawRay(transform.position, Vector3.right * 0.5f, Color.green);

                if (canJump)
                {
                    // Détectez les obstacles et déclenchez le saut si nécessaire
                    RaycastHit2D hitR = Physics2D.Raycast(transform.position + Vector3.right, Vector3.right, 0.5f);
                    RaycastHit2D hitL = Physics2D.Raycast(transform.position + Vector3.left, Vector3.left, 0.5f);
                    if (hitR.collider != null || hitL.collider != null)
                    {
                        Debug.Log("AAAAAAAAAAAAAAh");
                        // Appliquez la force de saut au Rigidbody2D du squelette
                        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                        canJump = false;
                        //StartCoroutine(JumpAgain());
                    }

                    // Détectez les trous devant le squelette
                    RaycastHit2D holeHitR = Physics2D.Raycast(transform.position + Vector3.right * 0.80f, Vector3.down * 1f, groundLayer);
                    RaycastHit2D holeHitL = Physics2D.Raycast(transform.position + Vector3.left * 0.80f, Vector3.down * 1f, groundLayer);
                    Debug.DrawRay(transform.position + Vector3.right * 0.80f, Vector3.down * 1f, Color.red);


                    if ((holeHitR.collider == null || holeHitL.collider == null))
                    {
                        Debug.Log("TROOOOOOOOOOOOOOOOOU");
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

    public void Hit()
    {
        Player playerScript = targetPlayer.GetComponent<Player>();

        if (playerScript != null)
        {
            Debug.Log("HIT!");
            playerScript.GetDamage(damageAmount, 0.5f);   
        }
        else
        {
            Debug.Log("Aucun script Player n'a été trouvé sur le GameObject cible.");
        }
    }

    void IsGrounded()
    {
        // Vérifiez si le squelette est au sol en utilisant un raycast
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
}
