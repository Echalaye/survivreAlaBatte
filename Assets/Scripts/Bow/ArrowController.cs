using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    private float powerShoot = 0f;
    private bool alreadyShoot = false;
    private bool goLeft = false;
    public GameObject arrowHitObject;

    // Update is called once per frame
    void Update()
    {
        if (!alreadyShoot)
            Shoot();
    }

    public void SetValueArrow(float valuePower, bool valueDir)
    {
        powerShoot = valuePower;
        goLeft = valueDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            Destroy(gameObject);
    }

    public void Shoot()
    {
        arrowHitObject.GetComponent<ArrowHit>().SetKnockLeft(goLeft);
        if(goLeft)
            rb.AddForce(Vector2.left * powerShoot, ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.right * powerShoot, ForceMode2D.Impulse);
        alreadyShoot = true;
    }

}
