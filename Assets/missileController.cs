using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileController : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    bool isStatic = false;

    [SerializeField]
    protected LayerMask whatToCollideWith;

    [SerializeField]
    protected int damage;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
            moveTowardsDestination();
    }

    protected virtual void FixedUpdate()
    {
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position - transform.forward * 20, transform.lossyScale, transform.forward, out hit, transform.rotation, 20f, whatToCollideWith);

        if (isHit)
        {
            hit.transform.GetComponent<IDamagable>().getHit(damage);
            Destroy(gameObject);
        }
    }

    protected virtual void moveTowardsDestination()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 100);
        //Gizmos.DrawWireCube(transform.position, boxColliderSizes);
    }
}
