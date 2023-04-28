using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiafController : MonoBehaviour
{
    private int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector3.down, 1000000f);
        Debug.DrawRay(transform.position, Vector2.down * 1000000);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Zombie"))
            {
                Debug.Log("player");
                transform.position = new Vector3(transform.position.x, hit.transform.position.y, transform.position.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
        }else if (collision.gameObject.CompareTag("Zombie"))
        {

        }
    }
}
