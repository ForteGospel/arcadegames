using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    GameObject enemyLaser;
    
    [SerializeField]
    GameObject[] attackPoints;

    [SerializeField]
    float laserDamage;

    [SerializeField]
    Vector2 delay = new Vector2(3, 6);

    [SerializeField]
    float attackDelayTimer = 0;

    [SerializeField]
    int numOfAttacks = 1;

    GameObject player;

    [SerializeField]
    float distanceToActivate;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<planeController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackDelayTimer <= 0)
        {
            if (isPlayerClose()) transform.DOLookAt(player.transform.position, 1f);
            else lookAtRandom();

            StartCoroutine(attack());
            attackDelayTimer = Random.Range(delay.x, delay.y);
        }
        else attackDelayTimer -= Time.deltaTime;

        
        //transform.Rotate(0, 0, 10, Space.Self);
    }

    bool isPlayerClose()
    {
        return Vector3.Distance(transform.position, player.transform.position) < distanceToActivate;
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < numOfAttacks; i++)
        {
            instantiateAttack();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void lookAtRandom()
    {
        Vector3 randomRot = new Vector3(Random.Range(0, -90), Random.Range(0, 360), transform.rotation.z);
        transform.DOLocalRotate(randomRot, 0.5f).SetRelative();
    }

    private void instantiateAttack()
    {
        if (attackPoints.Length == 0)
            Instantiate(enemyLaser, transform.position, transform.rotation);
        foreach (GameObject point in attackPoints)
            Instantiate(enemyLaser, point.transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform.gameObject);
    }
}
