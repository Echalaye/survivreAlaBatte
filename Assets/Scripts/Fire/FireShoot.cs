using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShoot : MonoBehaviour
{
    public Rigidbody2D rb;
    private float powerShoot = 10f;
    private bool alreadyShoot = false;
    private bool goLeft = false;
    public GameObject fireBallHitObject;
    private int nbBounce = 0;
    private int maxBounce = 0;
    private float velocityX = 0;
    private float velocityY = 0;
    private bool newBounce = true;

    // Update is called once per frame
    void Update()
    {
        if (!alreadyShoot)
            Shoot();
    }


    public void SetValueFireBall(bool valueDir, int valMaxBounce, float valVelocityX, float valVelocityY)
    {
        goLeft = valueDir;
        maxBounce = valMaxBounce;
        velocityX = valVelocityX;
        velocityY = valVelocityY;
        fireBallHitObject.GetComponent<FireHit>().SetKnockLeft(goLeft);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Ground"))
            {
                if (newBounce)
                {
                    if (nbBounce > maxBounce)
                        Destroy(gameObject);
                    nbBounce += 1;
                    Bounce();
                }
            }
        }
    }

    public void Bounce()
    {
        if (goLeft)
            rb.velocity = new Vector2(-velocityX, velocityY);
        else
            rb.velocity = new Vector2(velocityX,velocityY);
        newBounce = false;
        StartCoroutine(AllowNewBounce());
    }

    public void Shoot()
    {
        if (goLeft)
            rb.AddForce(Vector2.left * powerShoot, ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.right * powerShoot, ForceMode2D.Impulse);
        alreadyShoot = true;
    }

    IEnumerator AllowNewBounce()
    {
        yield return new WaitForSeconds(0.5f);
        newBounce = true;
    }

}
