using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiController : MonoBehaviour
{
    [SerializeField]
    playerStatsSO playerStats;

    [SerializeField]
    Image healthBar, boostBar;

    [SerializeField]
    TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateBars();
        updateScore();
    }

    void updateBars()
    {
        healthBar.fillAmount = playerStats.HealthPoints / playerStats.startingHealth;
        boostBar.fillAmount = playerStats.Boost / playerStats.startingBoost;
    }

    void updateScore()
    {
        string zeros = "";
        for (int i = 0; i < 3 - playerStats.Score.ToString().Length; i++)
            zeros = zeros + "0";
        scoreText.text = zeros + playerStats.Score.ToString();
    }
}
