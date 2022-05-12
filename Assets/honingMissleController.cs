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
        base.FixedUpdate();
    }

    protected override void moveTowardsDestination()
    {
        if (destination != null)
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        else
            base.moveTowardsDestination();
    }
}
