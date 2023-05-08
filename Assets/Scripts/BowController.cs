using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class BowController : MonoBehaviour
{
    public GameObject arrow;
    public GameObject waitIndicator1;
    public GameObject waitIndicator2;
    public GameObject waitIndicator3;
    private double timeUntilRelease = 0;
    private bool canAtt = true;
    private bool goodPosR = true;
    private bool goodPosL = false;
    private bool useBow = false;

    // Update is called once per frame
    void Update()
    {

        if (timeUntilRelease >= 2)
        {
            useBow = false;
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            Shoot();
        }
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

        if (Input.GetMouseButtonUp(1) && useBow)
        {
            waitIndicator3.SetActive(false);
            waitIndicator2.SetActive(false);
            waitIndicator1.SetActive(false);
            useBow= false;
            Shoot();
        }

        if (Input.GetMouseButton(1) && canAtt)
        {
            timeUntilRelease += Time.deltaTime;
            useBow= true;
        }
    }

    public void Shoot()
    {
        GameObject spawnedArrow = Instantiate(arrow);
        spawnedArrow.transform.position = transform.position;

    }
}
