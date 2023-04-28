using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PiafController : MonoBehaviour
{
    private int damage = 10;
    private bool hasAttack = false;
    private float posY;
    public float speed = 1.5f;
    private int randNumber;
    private bool goLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        GetPlaceToMoove();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAttack)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector3.down, 1000000f);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Zombie") || hit.collider.CompareTag("Skeleton"))
                {
                    posY = transform.position.y;
                    transform.position = new Vector3(transform.position.x, hit.transform.position.y, transform.position.z);
                }
            }
        }

        if(goLeft) {

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
        switch(randNumber)
        {
            case 0:
                goLeft = true;
                break;
            case 1:
                goLeft= false;
                break;
        }
        StartCoroutine(ChoosNewPlace());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
        }else if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<Zombiecontroller>().GetDamage(damage);
        }else if (collision.gameObject.CompareTag("Skeleton")){
            collision.gameObject.GetComponent<SkeletonController>().GetDamage(damage);
        }
        hasAttack = true;
        StartCoroutine(SetHasAttack());
    }

    IEnumerator SetHasAttack()
    {
        yield return new WaitForSeconds(2f);
        hasAttack = false;
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }

    IEnumerator ChoosNewPlace()
    {
        yield return new WaitForSeconds(10f);
        GetPlaceToMoove();
    }
}
