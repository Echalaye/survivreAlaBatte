using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private int damage = 15;
    private float knockback = 1f;
    private List<GameObject> listAllEnemy = new List<GameObject>();
    private bool canAtt = true;
    private bool knockLeft = true;
    private bool goodPosR = true;
    private bool goodPosL = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            knockLeft = true;
            if(!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            knockLeft = false;
            if(!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                goodPosR = true;
                goodPosL = false;
            }

        }


        if (Input.GetMouseButtonDown(0) && canAtt)
        {
            NormalHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ground"))
        {
            listAllEnemy.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(listAllEnemy.Contains(other.gameObject))
        {
            listAllEnemy.Remove(other.gameObject);
        }
    }

    public void NormalHit()
    {
        if (!knockLeft)
        {
            knockback *= -1;
        }
        for(int i = 0; i < listAllEnemy.Count; i++)
        {
            if (listAllEnemy[i].CompareTag("Piaf"))
                listAllEnemy[i].GetComponent<PiafController>().GetDamage(damage, knockback);
            else if (listAllEnemy[i].CompareTag("Ship"))
                listAllEnemy[i].GetComponent<ShipController>().GetDamage(damage, knockback);
            else if (listAllEnemy[i].CompareTag("Skeleton"))
                listAllEnemy[i].GetComponent<SkeletonController>().GetDamage(damage, knockback);
            else if (listAllEnemy[i].CompareTag("Zombie"))
                listAllEnemy[i].GetComponent<Zombiecontroller>().GetDamage(damage, knockback);
            
        }
        listAllEnemy.Clear();
        canAtt = false;
        StartCoroutine(WaitTilNewAtt());
    }

    IEnumerator WaitTilNewAtt()
    {
        yield return new WaitForSeconds(0.5f);
        canAtt = true;
    }
}