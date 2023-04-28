using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private int health;
    private int damageAmount = 10;
    private GameObject targetPlayer;
    private Rigidbody2D rb;

    public float speed = 1.2f;
    public float attackDistance = 4.0f;
    public LayerMask groundLayer;
    public float jumpForce = 5.0f;
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


                // D�tectez les obstacles et d�clenchez le saut si n�cessaire
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, groundLayer);

                if (hit.collider != null && IsGrounded())
                {
                    // Appliquez la force de saut au Rigidbody2D du squelette
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                }

                // D�tectez les trous devant le squelette
                RaycastHit2D holeHit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);

                if (holeHit.collider == null && IsGrounded())
                {
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
            Debug.Log("Aucun script Player n'a �t� trouv� sur le GameObject cible.");
        }
    }

    bool IsGrounded()
    {
        // V�rifiez si le squelette est au sol en utilisant un raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        return hit.collider != null;
    }

    public void GetDamage(int takenDamage)
    {
        health -= takenDamage;
    }
}
