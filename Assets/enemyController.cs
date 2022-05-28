using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class enemyController : MonoBehaviour
{
    [SerializeField]
    GameObject honingScope;

    [SerializeField]
    int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        honingScope.transform.DOScale(8f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetId(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        honingScope.transform.LookAt(Camera.main.transform);
        if (health <= 0)
            Destroy(gameObject);
    }

    public void targetted()
    {
        honingScope.SetActive(true);
        honingScope.transform.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform.gameObject);
    }

    public void getHit(int damage)
    {
        health -= damage;
    }
}