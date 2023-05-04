using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private int health;
    private int damageAmount = 0;
    private GameObject targetPlayer;
    private Rigidbody2D rb;
    private bool jump = false;

    public float speed = 1.2f;
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

                while (IsGrounded())
                {
                    // Détectez les obstacles et déclenchez le saut si nécessaire
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right, Vector3.right, 0.5f);
                    
                    if (hit.collider != null && IsGrounded())
                    {
                        Debug.Log("AAAAAAAAAAAAAAh");
                        // Appliquez la force de saut au Rigidbody2D du squelette
                        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                        jump = true;
                    }

                    // Détectez les trous devant le squelette
                    RaycastHit2D holeHit = Physics2D.Raycast(transform.position + Vector3.right * 0.80f, Vector3.down * 1f, groundLayer);
                    Debug.DrawRay(transform.position + Vector3.right * 0.80f, Vector3.down * 1f, Color.red);


                    if (holeHit.collider == null && IsGrounded())
                    {
                        Debug.Log("TROOOOOOOOOOOOOOOOOU");
                        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                        jump = true;
                    }
                }
                    // Avance vers le joueur 
                    transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);
            }

        }
    }

    public void Hit()
    {
        Player playerScript = targetPlayer.GetComponent<Player>();

        if (playerScript != null)
        {
            Debug.Log("HIT!");
            playerScript.GetDamage(damageAmount);   
        }
        else
        {
            Debug.Log("Aucun script Player n'a été trouvé sur le GameObject cible.");
        }
    }

    bool IsGrounded()
    {
        // Vérifiez si le squelette est au sol en utilisant un raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector3.down, 0.1f, groundLayer);

        return hit.collider != null;
    }

    public void GetDamage(int takenDamage)
    {
        health -= takenDamage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
