using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private bool goodPosR = true;
    private bool goodPosL = false;
    private bool canAtt = true;
    private double timeSpecial = 0;
    private int nbFireBallToSpawn = 0;
    private int nbOfBounce = 0;
    public GameObject waitIndicator1;
    public GameObject waitIndicator2;
    public GameObject waitIndicator3;
    public GameObject fireBall;

    // Update is called once per frame
    void Update()
    {
        if (timeSpecial >= 2)
        {
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            ShootFireBall();
        }

        if (timeSpecial > 1.5)
            waitIndicator3.SetActive(true);
        else if (timeSpecial > 1)
            waitIndicator2.SetActive(true);
        else if (timeSpecial > 0.5)
            waitIndicator1.SetActive(true);

        if (Input.GetKeyDown(KeyCode.A) && canAtt)
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && canAtt)
        {
            if (!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1.6f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosR = true;
                goodPosL = false;
            }

        }

        if (Input.GetMouseButtonUp(0) && canAtt)
        {
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            ShootFireBall();
        }
        if (Input.GetMouseButton(0) && canAtt)
        {
            timeSpecial += Time.deltaTime;
        }
    }
    public void ShootFireBall()
    {
        if(timeSpecial >= 1.5)
        {
            nbFireBallToSpawn = 3;
            nbOfBounce = 8;
        }else if(timeSpecial >= 1)
        {
            nbFireBallToSpawn = 2;
            nbOfBounce = 6;
        }
        else
        {
            nbFireBallToSpawn = 1;
            nbOfBounce = 4;
        }

        for(int i = 0; i < nbFireBallToSpawn; i++)
        {
            GameObject fireBallSpawned = Instantiate(fireBall);
            fireBallSpawned.transform.position = transform.position;
            fireBallSpawned.GetComponent<FireShoot>().SetValueFireBall(goodPosL, nbOfBounce, 5f + (float)i, 5f + (float)i);
        }

        canAtt = false;
        timeSpecial = 0;
        StartCoroutine(DelayShootFireBall(2f));
    }
    public void SetPosFire(bool valLeft)
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
    IEnumerator DelayShootFireBall(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAtt= true;
    }
}
