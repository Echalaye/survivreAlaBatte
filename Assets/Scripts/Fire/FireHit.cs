using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHit : MonoBehaviour
{
    private int damage = 30;
    private float knockback = 0.5f;
    private bool knockLeft = false;
    private bool alreadyHit = false;
    public void SetKnockLeft(bool val)
    {
        knockLeft = val;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!knockLeft)
            knockback *= -1;

        if (!alreadyHit)
        {
            if (collision.CompareTag("Piaf"))
                collision.GetComponent<PiafController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Ship"))
                collision.GetComponent<ShipController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Skeleton"))
                collision.GetComponent<SkeletonController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Zombie"))
                collision.GetComponent<ZombieController>().GetDamage(damage, knockback);
        }
        alreadyHit = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        alreadyHit= false;
    }

}
