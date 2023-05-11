using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    private bool canAtt = true;
    private bool goodPosR = true;
    private bool goodPosL = false;
    public GameObject axe;
    public GameObject stick;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && canAtt)
        {
            axe.GetComponent<AxeHit>().setKnockLeft(true);
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
            axe.GetComponent<AxeHit>().setKnockLeft(false);
            if (!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosR = true;
                goodPosL = false;
            }

        }


        if (Input.GetMouseButtonDown(0) && canAtt)
        {
            transform.Rotate(new Vector3(0, 0, -90), Space.Self);
            if (goodPosR)
                transform.position = new Vector3(transform.position.x + 0.85f, transform.position.y - 0.7f, 0);
            else
                transform.position = new Vector3(transform.position.x - 0.85f, transform.position.y - 0.7f, 0);

            axe.GetComponent<AxeHit>().setAttOn(true);
            stick.GetComponent<StickHit>().setAttOn(true);

            canAtt = false;
            StartCoroutine(DelayAtt());
        }
    }

    IEnumerator DelayAtt()
    {
        yield return new WaitForSeconds(1.3f);
        transform.Rotate(new Vector3(0, 0, 90), Space.Self);
        if (goodPosR)
            transform.position = new Vector3(transform.position.x - 0.85f, transform.position.y + 0.7f, 0);
        else
            transform.position = new Vector3(transform.position.x + 0.85f, transform.position.y + 0.7f, 0);
        axe.GetComponent<AxeHit>().setAttOn(false);
        stick.GetComponent<StickHit>().setAttOn(false);
        canAtt = true;
    }
}
