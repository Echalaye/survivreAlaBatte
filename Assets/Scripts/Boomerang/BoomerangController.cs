using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoomerangController : MonoBehaviour
{
    private bool canAtt = true;
    private bool goodPosR = true;
    private bool goodPosL = false;
    private bool boomerangShoot = false;
    private Vector3 posToGo;
    private float speed = 2f;
    private float distanceX = 3f;
    public GameObject boomerangHitObject;
    private GameObject player;
    private bool goBack = false;
    private bool rotate = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (rotate)
        {
            if(!goBack)
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 1f));
            else
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 1f));
        }


        if (goBack)
        {
            boomerangShoot = false;
            transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
            if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= 0.6)
            {
                goBack = false;
                rotate = false;
                if (goodPosL)
                {
                    transform.position = new Vector3(player.transform.position.x - 0.6f, player.transform.position.y, player.transform.position.z);
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, player.transform.position.z);
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                StartCoroutine(WaitUntilNextAtt(0.8f));
            }
        }

        if (boomerangShoot)
        {
            transform.position = Vector3.Lerp(transform.position, posToGo, speed * Time.deltaTime);
            if(Mathf.Abs(Vector3.Distance(transform.position, posToGo)) <= 0.6)
            {
                boomerangHitObject.GetComponent<BoomerangHit>().SetBoomerangIsShoot(false);
                StartCoroutine(WaitUntilGoBack(1.5f));
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1.2f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!goodPosR)
            {
                transform.position = new Vector3(transform.position.x + 1.2f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosR = true;
                goodPosL = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && canAtt)
        {
            ShootBoomerang();
            rotate = true;
        }
    }

    public void SetGoBack(bool val)
    {
        goBack = val;
    }

    public void SetBoomerangShoot(bool val)
    {
        boomerangShoot = val;
    }

    public void ShootBoomerang()
    {
        if (goodPosL)
            distanceX *= -1;
        posToGo = new Vector3(transform.position.x + distanceX, transform.position.y, transform.position.z);
        canAtt = false;
        boomerangShoot = true;
        boomerangHitObject.GetComponent<BoomerangHit>().SetBoomerangIsShoot(true);
        distanceX = 3f;
    }
    public void SetPosBoomerang(bool valLeft)
    {
        canAtt = true;
        if (valLeft)
        {
            if (!goodPosL)
            {
                transform.position = new Vector3(transform.position.x - 1.2f, transform.position.y, transform.position.z);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                goodPosL = true;
                goodPosR = false;
            }
        }
    }
    IEnumerator WaitUntilNextAtt(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAtt = true;
    }

    IEnumerator WaitUntilGoBack(float delay)
    {
        yield return new WaitForSeconds(delay);
        goBack = true;
    }
}
