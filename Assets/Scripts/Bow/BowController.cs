using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrow;
    public GameObject waitIndicator1;
    public GameObject waitIndicator2;
    public GameObject waitIndicator3;

    private Vector2 mousePos;
    private Vector2 direction;
    private Vector2 bowPos;
    private double timeUntilRelease = 0;
    private bool canAtt = true;
    private bool goodPosR = true;
    private bool goodPosL = false;
    private bool useBow = false;
    private float arrowPower = 0;



    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bowPos = transform.position;
        direction = mousePos - bowPos;



        if (timeUntilRelease >= 2)
        {
            useBow = false;
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            Shoot();
        }

        if (timeUntilRelease > 1.5)
            waitIndicator3.SetActive(true);
        else if (timeUntilRelease > 1)
            waitIndicator2.SetActive(true);
        else if (timeUntilRelease > 0.5)
            waitIndicator1.SetActive(true);

        if (Input.GetKeyDown(KeyCode.A) && canAtt)
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && canAtt)
        {
            if (!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosR = true;
                goodPosL = false;
            }

        }

        if (Input.GetMouseButtonUp(0) && useBow)
        {
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            useBow= false;
            Shoot();
        }

        if (Input.GetMouseButton(0) && canAtt)
        {
            timeUntilRelease += Time.deltaTime;
            useBow= true;
        }
    }

    public void Shoot()
    {
        GameObject spawnedArrow = Instantiate(arrow, direction, transform.rotation);
        spawnedArrow.transform.position = transform.position;
        if (timeUntilRelease >= 1.5)
            arrowPower = 20f;
        else if (timeUntilRelease >= 1)
            arrowPower = 15f;
        else if (timeUntilRelease >= 0.5)
            arrowPower = 10f;
        else
            arrowPower = 5f;
        canAtt = false;
        timeUntilRelease= 0;
        spawnedArrow.GetComponent<ArrowController>().SetValueArrow(arrowPower, goodPosL, false);
        StartCoroutine(WaitTilNewAtt(1.5f));
    }

    IEnumerator WaitTilNewAtt(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAtt = true;
    }
}
