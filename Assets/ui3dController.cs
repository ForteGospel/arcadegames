using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ui3dController : MonoBehaviour
{
    [SerializeField]
    playerStatsSO playerStats;
    [SerializeField]
    GameObject[] bombs;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject bomb in bombs)
            bomb.transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        showBombs();
    }

    void showBombs()
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            if (playerStats.Bombs > i) bombs[i].SetActive(true);
            else bombs[i].SetActive(false);
        }
    }
}
