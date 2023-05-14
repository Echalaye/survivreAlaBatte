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
    private bool useSpecial = false;
    private double timeSpecial = 0;
    public GameObject waitIndicator1;
    public GameObject waitIndicator2;
    public GameObject waitIndicator3;

    private void Update()
    {
        if(timeSpecial >= 2)
        {
            useSpecial = false;
            timeSpecial = 0;
            waitIndicator3.SetActive(false); 
            waitIndicator2.SetActive(false); 
            waitIndicator1.SetActive(false);
            SpecialHit();
        }

        if (timeSpecial > 1.5)
            waitIndicator3.SetActive(true);
        else if(timeSpecial > 1)
            waitIndicator2.SetActive(true);
        else if(timeSpecial > 0.5)
            waitIndicator1.SetActive(true);

        if (Input.GetKeyDown(KeyCode.A))
        {
            knockLeft = true;
            if(!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0,180,0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            knockLeft = false;
            if(!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosR = true;
                goodPosL = false;
            }

        }

        if(Input.GetMouseButtonUp(1) && useSpecial)
        {
            if(timeSpecial < 1.5)
            {
                timeSpecial = 0;
                NormalHit();
            }
            else
            {
                waitIndicator3.SetActive(false);
                waitIndicator2.SetActive(false);
                waitIndicator1.SetActive(false);
                SpecialHit();
            }
            useSpecial= false;
        }

        if(Input.GetMouseButton(1) && canAtt)
        {
            useSpecial= true;
            timeSpecial += Time.deltaTime;
        }



        if (Input.GetMouseButtonDown(0) && canAtt && !useSpecial)
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
                listAllEnemy[i].GetComponent<ZombieController>().GetDamage(damage, knockback);
            
        }
        knockback = 1f;
        listAllEnemy.Clear();
        canAtt = false;
        StartCoroutine(WaitTilNewAtt(0.5f));
    }

    public void SpecialHit()
    {
        knockback *= 2 ;
        damage *= 2;
        if (!knockLeft)
        {
            knockback *= -1;
        }
        for (int i = 0; i < listAllEnemy.Count; i++)
        {
            if (listAllEnemy[i].CompareTag("Piaf"))
                listAllEnemy[i].GetComponent<PiafController>().GetDamage(damage, knockback);
            else if (listAllEnemy[i].CompareTag("Ship"))
                listAllEnemy[i].GetComponent<ShipController>().GetDamage(damage, knockback);
            else if (listAllEnemy[i].CompareTag("Skeleton"))
                listAllEnemy[i].GetComponent<SkeletonController>().GetDamage(damage, knockback);
            else if (listAllEnemy[i].CompareTag("Zombie"))
                listAllEnemy[i].GetComponent<ZombieController>().GetDamage(damage, knockback);

        }
        knockback = 1f;
        damage = 15;
        listAllEnemy.Clear();
        canAtt = false;
        StartCoroutine(WaitTilNewAtt(1f));
    }
    public void SetPosSword(bool valLeft)
    {
        canAtt = true;
        if (valLeft)
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
    }
    IEnumerator WaitTilNewAtt(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAtt = true;
    }
}