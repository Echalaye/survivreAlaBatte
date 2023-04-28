using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private int randNumber;
    private bool goLeft = false;
    public float speed = 1.5f;
    private int health = 30;
    // Update is called once per frame
    void Update()
    {

        if (goLeft)
        {

            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
        }
    }
    public void GetPlaceToMoove()
    {
        randNumber = Random.Range(0, 2);
        switch (randNumber)
        {
            case 0:
                goLeft = true;
                break;
            case 1:
                goLeft = false;
                break;
        }
        StartCoroutine(ChoosNewPlace());
    }

    public void GetDamage(int takenDamage)
    {
        health -= takenDamage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ChoosNewPlace()
    {
        yield return new WaitForSeconds(10f);
        GetPlaceToMoove();
    }
}
