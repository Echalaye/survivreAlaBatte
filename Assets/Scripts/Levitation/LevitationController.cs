using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationController : MonoBehaviour
{

    private List<GameObject> listAllEnemy = new List<GameObject>();
    public GameObject waitIndicator1;
    public GameObject waitIndicator2;
    public GameObject waitIndicator3;
    private double timePowerLevitation = 0;
    private bool canAtt = true;
    private bool goodPosR = true;
    private bool goodPosL = false;

    void Update()
    {
        if (timePowerLevitation >= 2)
        {
            timePowerLevitation = 0;
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            EverybodyFly();
        }

        if (timePowerLevitation > 1.5)
            waitIndicator3.SetActive(true);
        else if (timePowerLevitation > 1)
            waitIndicator2.SetActive(true);
        else if (timePowerLevitation > 0.5)
            waitIndicator1.SetActive(true);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(0, 180, 0);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(0, 180, 0);
                goodPosR = true;
                goodPosL = false;
            }

        }

        if (Input.GetMouseButtonUp(0) && canAtt)
        {
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            EverybodyFly();
        }

        if (Input.GetMouseButton(0) && canAtt)
        {
            timePowerLevitation += Time.deltaTime;
        }
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

    public void EverybodyFly()
    {
        canAtt = false;
        for (int i = 0; i < listAllEnemy.Count; i++)
        {
            listAllEnemy[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            listAllEnemy[i].transform.position = new Vector3(listAllEnemy[i].transform.position.x, transform.position.y + 2f, listAllEnemy[i].transform.position.z);
            if (listAllEnemy[i].CompareTag("Piaf"))
                listAllEnemy[i].GetComponent<PiafController>().SetCanMoove(false);
            else if (listAllEnemy[i].CompareTag("Ship"))
                listAllEnemy[i].GetComponent<ShipController>().SetCanMoove(false);
            else if (listAllEnemy[i].CompareTag("Skeleton"))
                listAllEnemy[i].GetComponent<SkeletonController>().SetCanMoove(false);
            else if (listAllEnemy[i].CompareTag("Zombie"))
                listAllEnemy[i].GetComponent<ZombieController>().SetCanMoove(false);
        }
        StartCoroutine(WaitTilNewAtt(3f));
    }
    public void SetPosLevitation(bool valLeft)
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
