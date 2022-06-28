using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class honingMissleController : missileController
{
    public Transform destination;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        moveTowardsDestination();
    }

    protected override void FixedUpdate()
    {
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position - transform.forward * 20, transform.lossyScale, transform.forward, out hit, transform.rotation, 20f, whatToCollideWith);

        if (isHit)
        {
            hit.transform.GetComponent<IDamagable>().getHit(damage);
            hit.transform.GetComponent<enemyController>().hideHoning();
            Destroy(gameObject);
        }
    }

    protected override void moveTowardsDestination()
    {
        if (destination != null)
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        else
            base.moveTowardsDestination();
    }
}
