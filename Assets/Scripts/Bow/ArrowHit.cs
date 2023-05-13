using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    private int damage = 15;
    private float knockback = 1.5f;
    private bool knockLeft = false;
    private bool alreadyHit = false;
    private bool skeletonShoot = false;
    public void SetKnockLeft(bool val)
    {
        knockLeft= val;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!knockLeft)
            knockback *= -1;

        if (!alreadyHit)
        {
            if (skeletonShoot)
            {
                if (collision.CompareTag("Player"))
                    collision.GetComponent<Player>().GetDamage(10, knockback);
            }
            else
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
        }
        alreadyHit= true;
    }

    public void SetSkeletonShoot(bool val)
    {
        skeletonShoot= val;
    }
}
