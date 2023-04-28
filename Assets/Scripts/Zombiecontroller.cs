using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiecontroller : MonoBehaviour
{
    private int health;
    public float speed = 1f;
    private int damageAmount = 15;
    private GameObject targetPlayer;

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
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);
        }
    }

    public void Hit()
    {
        Player playerScript = targetPlayer.GetComponent<Player>();
        
        if (playerScript != null)
        {
            playerScript.GetDamage(damageAmount);
        }
        else
        {
            Debug.Log("Aucun script Player n'a été trouvé sur le GameObject cible.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hit();
        }
    }
}

