using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private int health;
    private int damageAmount = 10;
    private GameObject targetPlayer;
    public float speed = 1.2f;
    public float attackDistance = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");

        if (targetPlayer == null)
        {
            Debug.Log("Aucun GameObject avec le tag 'Player' n'a été trouvé.");
        }

    }

    // Update is called once per frame
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
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);

            }

        }
    }

    public void Hit()
    {
        player playerScript = targetPlayer.GetComponent<player>();

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

}
