using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    GameObject enemyLaser, point1, point2;

    [SerializeField]
    float laserDamage;

    [SerializeField]
    float attackDelay = 3;

    [SerializeField]
    float attackDelayTimer = 0;

    GameObject player;

    [SerializeField]
    float distanceToActivate;

    Sequence t;

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
            attackDelayTimer = attackDelay;
        }
        else attackDelayTimer -= Time.deltaTime;

        
        transform.Rotate(0, 0, 10, Space.Self);
    }

    bool isPlayerClose()
    {
        return Vector3.Distance(transform.position, player.transform.position) < distanceToActivate;
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(1);
        Instantiate(enemyLaser, point1.transform.position, transform.rotation);
        Instantiate(enemyLaser, point2.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(enemyLaser, point1.transform.position, transform.rotation);
        Instantiate(enemyLaser, point2.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(enemyLaser, point1.transform.position, transform.rotation);
        Instantiate(enemyLaser, point2.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
    }

    void lookAtRandom()
    {
        Vector3 randomRot = new Vector3(Random.Range(0, -90), Random.Range(0, 360), transform.rotation.z);
        transform.DOLocalRotate(randomRot, 0.5f).SetRelative();
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform.gameObject);
    }
}
