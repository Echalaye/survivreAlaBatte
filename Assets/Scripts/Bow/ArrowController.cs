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
    Vector2 direction;
    float angle;
    private bool hasHit = false;

    // Update is called once per frame
    void Update()
    {

        if (!hasHit)
            TrackRotation();

        if (!alreadyShoot)
            Shoot();
    }

    public void TrackRotation()
    {
        direction = rb.velocity;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetValueArrow(float valuePower, bool valueDir)
    {
        powerShoot = valuePower;
        goLeft = valueDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            hasHit= true;
            rb.velocity= Vector3.zero;
            rb.isKinematic= true;
            StartCoroutine(DestroyArrow());
        }
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

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
