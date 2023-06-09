using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private List<GameObject> listAllEnemy = new List<GameObject>();
    public AudioSource batSound;
    private bool canAtt = true;
    private bool goodPosR = true;
    private bool goodPosL = false;
    private float velocityX = 10;
    private float velocityY = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(180, 0, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(180, 0, 0), Space.Self);
                goodPosR = true;
                goodPosL = false;
            }

        }

        if (Input.GetMouseButtonDown(0) && canAtt)
        {
            batSound.Play();
            GigaHit();

        }

    }

    private void GigaHit()
    {
        if (goodPosL)
        {
            velocityX *= -1;
        }
        for (int i = 0; i < listAllEnemy.Count; i++)
        {
            if (listAllEnemy[i].CompareTag("Piaf") && !listAllEnemy[i].GetComponent<PiafController>().GetCanMoove())
                listAllEnemy[i].GetComponent<PiafController>().GigaBatHitMe(velocityX, velocityY);
            else if (listAllEnemy[i].CompareTag("Ship") && !listAllEnemy[i].GetComponent<ShipController>().GetCanMoove())
                listAllEnemy[i].GetComponent<ShipController>().GigaBatHitMe(velocityX, velocityY);
            else if (listAllEnemy[i].CompareTag("Skeleton") && !listAllEnemy[i].GetComponent<SkeletonController>().GetCanMoove())
                listAllEnemy[i].GetComponent<SkeletonController>().GigaBatHitMe(velocityX, velocityY);
            else if (listAllEnemy[i].CompareTag("Zombie") && !listAllEnemy[i].GetComponent<ZombieController>().GetCanMoove())
                listAllEnemy[i].GetComponent<ZombieController>().GigaBatHitMe(velocityX, velocityY);
        }
        canAtt = false;
        velocityX = 10;
        listAllEnemy.Clear();
        StartCoroutine(CanAttAgain(1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ground") && !collision.CompareTag("Untagged"))
        {
            listAllEnemy.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (listAllEnemy.Contains(other.gameObject))
        {
            listAllEnemy.Remove(other.gameObject);
        }
    }

    public void SetPosBat(bool valLeft)
    {
        canAtt = true;
        if (valLeft)
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(180, 0, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
    }
    IEnumerator CanAttAgain(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAtt= true;
    }
}
